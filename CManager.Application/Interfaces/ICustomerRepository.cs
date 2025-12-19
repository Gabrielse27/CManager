using System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using CManager.Domain;
using CManager.Application.Interfaces;
using System.IO;




namespace CManager.Application.Interfaces;
public interface ICustomerRepository
{
    // Hämta alla (Async)
    Task<List<Customer>> GetAllAsync();

    // Hämta en specifik (Async)
    Task<Customer?> GetByIdAsync(Guid id);

    // Lägg till (Async)
    Task AddAsync(Customer customer);

    // Uppdatera (Async)
    Task UpdateAsync(Customer customer);

    // Ta bort (Async)
    Task DeleteAsync(Guid id);
}
