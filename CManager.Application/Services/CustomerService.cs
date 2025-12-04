using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CManager.Application.Interfaces;
using CManager.Domain;


namespace CManager.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;//List<Customer> _customers = new();//
        private readonly List<Customer> _customers;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;

            // Ladda kunder från fil vid start
            _customers = _repository.LoadCustomers();
        }
        public Customer CreateCustomer
           (string firstName,
            string lastName, 
            string email, 
            string phone, 
            string street, 
            string postalcode, 
            string city)  
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phone,
                Street = street,
                PostalCode = postalcode,
                City = city

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

        // <summary>
        /// Hämtar kund via e-postadress.
        /// </summary>
        public Customer? GetCustomerByEmail(string email)
        {
            return _customers.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }


        public bool DeleteCustomer(Guid id)
        {
            var customer = GetCustomerById(id);
            if (customer == null)
                return false;

            _customers.Remove(customer);
            SaveChanges();
            return true;
        }

        public bool DeleteCustomerByEmail(string email)
        {
            var customer = GetCustomerByEmail(email);
            if (customer == null)
                return false;

            _customers.Remove(customer);
            SaveChanges();
            return true;
        }
        /// <summary>
        /// Sparar aktuell lista till JSON-fil via Repository.
        /// </summary>
        public void SaveChanges()
        {
            _repository.SaveCustomers(_customers);
        }



    }
}
