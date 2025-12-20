using CManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace CManager.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomerAsync(Guid id);
        Task SaveCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid id);

    }
}






















/*namespace CManager.Application.Interfaces;

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

    IEnumerable<Customer> GetCustomers();
    Customer GetCustomer(Guid id);
    void SaveCustomer(Customer customer);

    // LÄGG TILL DENNA RAD:
    
}
*/