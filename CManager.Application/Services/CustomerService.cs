using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Application.Helpers;



namespace CManager.Application.Services
    {
        public class CustomerService : ICustomerService
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly GuidFactory _guidFactory;

            public CustomerService(ICustomerRepository customerRepository, GuidFactory guidFactory)
            {
                _customerRepository = customerRepository;
                _guidFactory = guidFactory;
            }

            // ---------------------------------------------------------
            // CREATE
            // ---------------------------------------------------------
            public Customer CreateCustomer(
                string firstName,
                string lastName,
                string email,
                string phone,
                string street,
                string postalCode,
                string city)
            {
                var customers = _customerRepository.LoadCustomers();

                var customer = new Customer
                {
                    Id = _guidFactory.CreateGuid(),
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    Street = street,
                    PostalCode = postalCode,
                    City = city
                };

                customers.Add(customer);
                _customerRepository.SaveCustomers(customers);

                return customer;
            }

            // ---------------------------------------------------------
            // READ
            // ---------------------------------------------------------
            public List<Customer> GetAllCustomers()
            {
                return _customerRepository.LoadCustomers();
            }

            public Customer? GetCustomerById(Guid id)
            {
                var customers = _customerRepository.LoadCustomers();
                return customers.FirstOrDefault(c => c.Id == id);
            }

            public Customer? GetCustomerByEmail(string email)
            {
                var customers = _customerRepository.LoadCustomers();
                return customers.FirstOrDefault(c => c.Email == email);
            }

            // ---------------------------------------------------------
            // UPDATE
            // ---------------------------------------------------------
            public bool UpdateCustomer(Customer customer)
            {
                var customers = _customerRepository.LoadCustomers();

                var existing = customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existing == null)
                    return false;

                existing.FirstName = customer.FirstName;
                existing.LastName = customer.LastName;
                existing.Email = customer.Email;
                existing.Phone = customer.Phone;
                existing.Street = customer.Street;
                existing.PostalCode = customer.PostalCode;
                existing.City = customer.City;

                _customerRepository.SaveCustomers(customers);
                return true;
            }

            // ---------------------------------------------------------
            // DELETE
            // ---------------------------------------------------------
            public bool DeleteCustomer(Guid id)
            {
                var customers = _customerRepository.LoadCustomers();
                var remove = customers.FirstOrDefault(c => c.Id == id);

                if (remove == null)
                    return false;

                customers.Remove(remove);
                _customerRepository.SaveCustomers(customers);

                return true;
            }

            public bool DeleteCustomerByEmail(string email)
            {
                var customers = _customerRepository.LoadCustomers();
                var remove = customers.FirstOrDefault(c => c.Email == email);

                if (remove == null)
                    return false;

                customers.Remove(remove);
                _customerRepository.SaveCustomers(customers);

                return true;
            }

            // ---------------------------------------------------------
            // SAVE CHANGES (REQUIRES CUSTOMER OBJECT)
            // ---------------------------------------------------------
            public bool SaveChanges(Customer customer)
            {
                return UpdateCustomer(customer);
            }
        }
}



    