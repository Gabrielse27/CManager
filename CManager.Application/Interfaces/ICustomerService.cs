using CManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.Interfaces
{
    public interface ICustomerService
    {

        Customer CreateCustomer(string firstName, string lastName, string email, string phone, string street, string postalCode, string city);
        List<Customer> GetAllCustomers();

        Customer? GetCustomerById(Guid id);
        Customer? GetCustomerByEmail(string email);
        


        bool UpdateCustomer(Customer customer);

       
      
        bool DeleteCustomer(Guid id);
        bool DeleteCustomerByEmail(string email);

        bool SaveChanges(Customer customer);

    }
}      
