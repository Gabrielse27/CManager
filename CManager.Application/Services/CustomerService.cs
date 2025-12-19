using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Application.Helpers;
using System.Linq;




namespace CManager.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer?> GetCustomerAsync(Guid id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    public async Task CreateCustomerAsync(Customer customer)
    {
        //Använd helper-klass för att skapa ID
        // Om du har en static metod i GuidFactory:
        customer.Id = GuidFactory.Create();

        if (customer.Id == Guid.Empty)
        {
            customer.Id = Guid.NewGuid();
        }

        await _customerRepository.AddAsync(customer);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        // Här anropar vi repositoryt som sparar till filen
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}
