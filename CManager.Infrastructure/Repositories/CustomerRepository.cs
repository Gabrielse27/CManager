using System;
using System.Collections.Generic;
using System.Text.Json;
using CManager.Domain;
using System.IO;
using CManager.Application.Interfaces;
using CManager.Infrastructure.Formatters;




namespace CManager.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly JsonFormatter _formatter;
    private readonly string _filePath;
    private List<Customer> _customers = new();

    public CustomerRepository()
    {
        // Vi skapar en instans av vår hjälpklass som sköter JSON- konverterning. 
        _formatter = new JsonFormatter();

        // Vi använder "Environment.SpecialFolder.LocalApplicationData".
        // På Windows blir det "AppData", men på en Android-telefon
        // blir det en helt annan skyddad mapp som appen får skriva till.
        // Detta gör att koden fungerar på ALLA enheter (Cross-platform).
        // Vi sparar filen i AppData så den fungerar på både PC och Mobil
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);


        // Path.Combine lägger ihop mappen och filnamnet och sätter automatiskt in rätt 
        // snedstreck (\ eller /) beroende på om det är Windows eller Android.
        _filePath = Path.Combine(folder, "customers.json");
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Customer>();
        }

        //  Läs texten från filen (Repositoryt sköter filhanteringen)
        var json = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Customer>();
        }

        // Använd DIN formatter för att göra om text till lista
        _customers = _formatter.Deserialize<List<Customer>>(json) ?? new List<Customer>();

        return _customers;
    }

    public async Task AddAsync(Customer customer)
    {
        // Hämta befintliga först
        _customers = await GetAllAsync();

        _customers.Add(customer);

        await SaveToFile();
    }

    public async Task UpdateAsync (Customer customer)
    {
        // Vi hämtar den senaste listan från filen för att vara säkra på att vi har aktuell data.
        // Detta gör vi för att inte råka skriva över något om filen ändrats nyss.
        _customers = await GetAllAsync();

        // Vi letar i listan: "Finns det någon kund här som har samma ID som den vi vill uppdatera?"
        // (x => x.Id == customer.Id) är sökfiltret.
        var existingCustomer = _customers.FirstOrDefault(x => x.Id == customer.Id);

        // Om vi hittade en matchande kund(den är inte null)...
        if (existingCustomer != null)
        {
            // Vi tar reda på vilket index (vilken plats i kön/listan, t.ex. plats 0, 1 eller 2) den gamla kunden har.
            var index = _customers.IndexOf(existingCustomer);

            // Vi ersätetter den gamla kunden med den uppdaterade kunden på samma plats i listan.
            _customers[index] = customer;

            await SaveToFile();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        // Vi hämtar den allra senaste versionen av listan från filen.
    // Detta gör vi för att vara säkra på att vi inte jobbar med gammal data.
        _customers = await GetAllAsync();


        // Vi söker igenom listan: "Hitta den kund som har samma ID som det vi skickade in".
    // Om ingen hittas blir variabeln 'customer' null (tom).
        var customer = _customers.FirstOrDefault(x => x.Id == id);

        // Om kunden är null (inte finns) hoppar vi över detta block så att appen inte kraschar.
        if (customer != null)
        {
            // Vi tar bort kunden från vår lista i minnet (RAM-minnet)
            _customers.Remove(customer);

            // När listan är uppdaterad sparar vi den tillbaka till filen.
            await SaveToFile();
        }
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        _customers = await GetAllAsync();
        return _customers.FirstOrDefault(c => c.Id == id);
    }

    // Hjälpmetod för att spara
    private async Task SaveToFile()
    {
        // Serialiserar listan till JSON-format med hjälp av min formatter så att den går att spara.
        var json = _formatter.Serialize(_customers);

        // Använder "await" här för att operationen ska ske asynkront – då fryser inte programmet medan datorn jobbar med filen.
        // Sparar textsträngen till filen på hårddisken.
        await File.WriteAllTextAsync(_filePath, json);

    }
}

