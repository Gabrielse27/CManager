using CManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.Interfaces
{
    public interface ICustomerService
    {
        Customer CreateCustomer(string firstName, string lastname);
        List<Customer> GetAllCustomers();
        Customer? GetCustomerById(Guid id);
        bool DeleteCustomer(Guid id);
    }
}
