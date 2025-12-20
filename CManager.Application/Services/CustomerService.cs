
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CManager.Application.Interfaces;
using CManager.Domain; 
using CManager.Application.Helpers; 

namespace CManager.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer?> GetCustomerAsync(Guid id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    // Slog ihop Create och Update till en enda "SaveCustomerAsync"
    // Detta gör det mycket enklare för ViewModellen att bara anropa "Spara".
    public async Task SaveCustomerAsync(Customer customer)
    {
        // Om kunden saknar ID (Guid.Empty), då är det en NY kund -> Skapa
        if (customer.Id == Guid.Empty)
        {
            // Skapa ID (antingen via Factory eller direkt)
      
            customer.Id = IdGenerator.CreateId();

            await _customerRepository.AddAsync(customer);
        }
        else
        {
            // Om ID finns, då är det en gammal kund -> Uppdatera
            await _customerRepository.UpdateAsync(customer);
        }
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}
