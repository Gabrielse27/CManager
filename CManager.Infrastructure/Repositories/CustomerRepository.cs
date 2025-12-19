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
        // Vi skapar en instans av din befintliga formatter
        _formatter = new JsonFormatter();

        // Vi sparar filen i AppData så den fungerar på både PC och Mobil
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
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

    public async Task UpdateAsync(Customer customer)
    {
        _customers = await GetAllAsync();

        var existingCustomer = _customers.FirstOrDefault(x => x.Id == customer.Id);
        if (existingCustomer != null)
        {
            // Uppdatera värdena
            var index = _customers.IndexOf(existingCustomer);
            _customers[index] = customer;

            await SaveToFile();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        _customers = await GetAllAsync();

        var customer = _customers.FirstOrDefault(x => x.Id == id);
        if (customer != null)
        {
            _customers.Remove(customer);
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
        // 1. Använd DIN formatter för att göra om listan till text
        var json = _formatter.Serialize(_customers);

        // 2. Skriv texten till filen
        await File.WriteAllTextAsync(_filePath, json);
    }
}

