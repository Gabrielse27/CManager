using CManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;



namespace CManager.Application.Interfaces;

public interface ICustomerService
{
    // Hämta alla
    Task<List<Customer>> GetAllCustomersAsync();

    // Hämta en
    Task<Customer?> GetCustomerAsync(Guid id);

    // Skapa
    Task CreateCustomerAsync(Customer customer);

    // Uppdatera (Denna saknades!)
    Task UpdateCustomerAsync(Customer customer);

    // Ta bort
    Task DeleteCustomerAsync(Guid id);
}
