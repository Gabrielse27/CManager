using System;
using System.Collections.Generic;
using System.Text;
using CManager.Application.Interfaces;
using CManager.Domain;


namespace CManager.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers = new();

        public Customer CreateCustomer(string firstName, string lastName)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName
            };

            _customers.Add(customer);
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers;
        }

        public Customer? GetCustomerById(Guid id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public bool DeleteCustomer(Guid id)
        {
            var customer = GetCustomerById(id);
            if (customer == null)
                return false;

            _customers.Remove(customer);
            return true;
        }

    }
}
