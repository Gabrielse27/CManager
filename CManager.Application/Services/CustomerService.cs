using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Application.Helpers;
using System.Linq;


namespace CManager.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly GuidFactory _guidFactory;

        //Abstraktion via Dependency Injection. 
        public CustomerService(ICustomerRepository customerRepository, GuidFactory guidFactory)
        {
            _customerRepository = customerRepository;
            _guidFactory = guidFactory;
        }

        // ----
        // CREATE
        // ----
        //  Skapa ny kund med unikt ID via helper-klass (SOLID).
        public Customer CreateCustomer(
            string firstName,
            string lastName,
            string email,
            string phone,
            string street,
            string postalCode,
            string city)
        {
            // Hämta nuvarande lista
            var customers = _customerRepository.LoadCustomers();

            var customer = new Customer
            // Använder din GuidFactory helper
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

            // Lägg till och spara
            customers.Add(customer);
            _customerRepository.SaveCustomers(customers);

            return customer;
        }

        // ----
        // READ
        // ----
        // Servicedelen: Hämta alla kunder. 
        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.LoadCustomers();
        }

        // Servicedelen: Hämta specifik kund. 
        public Customer? GetCustomerById(Guid id)
        {
            var customers = _customerRepository.LoadCustomers();
            // Använder LINQ för att hitta rätt.
            return customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer? GetCustomerByEmail(string email)
        {
            var customers = _customerRepository.LoadCustomers();
            return customers.FirstOrDefault(c => c.Email == email);
        }

        // ----
        // UPDATE
        // ----
        //Uppdatera en specifik kund.
        
        public bool UpdateCustomer(Customer customer)
        {
            var customers = _customerRepository.LoadCustomers();

            // Hitta den gamla versionen av kunden i listan
            var existing = customers.FirstOrDefault(c => c.Id == customer.Id);
            if (existing == null)
                return false; // Kunden fanns inte

            // Uppdatera fälten
            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;
            existing.Street = customer.Street;
            existing.PostalCode = customer.PostalCode;
            existing.City = customer.City;

            // Spara ner den uppdaterade listan
            _customerRepository.SaveCustomers(customers);
            return true;
        }


        // -----
        // DELETE
        // -----
        // Ta bort en specifik kund. 
        
        public bool DeleteCustomer(Guid id)
        {
            var customers = _customerRepository.LoadCustomers();
            var remove = customers.FirstOrDefault(c => c.Id == id);

            if (remove == null)
                return false; // Kunden fanns inte

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

     
        public bool SaveChanges(Customer customer)
        {
            return UpdateCustomer(customer);
        }
    }
}
