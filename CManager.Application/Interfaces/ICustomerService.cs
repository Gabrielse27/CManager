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
