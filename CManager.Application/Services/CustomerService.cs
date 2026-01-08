
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CManager.Application.Interfaces;
using CManager.Domain; 
using CManager.Application.Helpers; 

namespace CManager.Application.Services;

// Servicen implementerar interface för att vi ska kunna använda Dependency Injection.
public class CustomerService : ICustomerService
{
    // Vi skapar ett fält för Repositoryt. Servicen "äger" inte datan, den bara lånar Repositoryt.
    private readonly ICustomerRepository _customerRepository;

    // KONSTRUKTORN(Dependency Injection)
    // När appen startar (i MauiProgram.cs) skickas automatiskt ett färdigt Repository in här.
    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    // Hämtar alla kunder genom att ropa på Repositoryt.
    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        return await _customerRepository.GetAllAsync();

    }

    // Hämtar en specifik kund baserat på ID.
    public async Task<Customer?> GetCustomerAsync(Guid id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    // Istället för att ViewModel ska behöva veta om det är en NY eller GAMMAL kund,
    // så sköter vi den logiken här.
    // Slog ihop Create och Update till en enda "SaveCustomerAsync"
    // Detta gör det mycket enklare för ViewModellen att bara anropa "Spara".
    public async Task SaveCustomerAsync(Customer customer)
    {
        // Om ID saknas betyder det att kunden inte finns i systemet än -> SKAPA NY.
        // Om kunden saknar ID (Guid.Empty), då är det en NY kund -> Skapa
        if (customer.Id == Guid.Empty)
        {
            // Skapa ID 
            // Vi använder vår IdGenerator för att skapa ett unikt ID
            //             FACTORY PATTERN:
            // Servicen ropar på fabriken för att få ett nytt ID.
            customer.Id = GuidFactory.CreateId();

            // Vi ber Repositoryt lägga till den nya kunden (Add).
            await _customerRepository.AddAsync(customer);
        }
        else
        {
            // Vi ber Repositoryt uppdatera den befintliga kunden (Update).
            // Om ID finns, då är det en gammal kund -> Uppdatera
            await _customerRepository.UpdateAsync(customer);
        }
    }


    // Tar bort en kund via Repositoryt.
    public async Task DeleteCustomerAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}
