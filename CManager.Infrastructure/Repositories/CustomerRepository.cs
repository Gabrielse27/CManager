using System;
using System.Collections.Generic;
using System.Text.Json;
using CManager.Domain;
using System.IO;
using CManager.Application.Interfaces;
using CManager.Infrastructure.Formatters;


namespace CManager.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _filePath = "customers.json";
        private readonly JsonFormatter _formatter = new JsonFormatter();

        public List<Customer> LoadCustomers()
        {
            if (!File.Exists(_filePath))
                return new List<Customer>();

            string json = File.ReadAllText(_filePath);
            var customers = _formatter.Deserialize<List<Customer>>(json);

            return customers ?? new List<Customer>();
        }

        public void SaveCustomers(List<Customer> customers)
        {
            string json = _formatter.Serialize(customers);
            File.WriteAllText(_filePath, json);
        }

        public void UpdateCustomer(Customer updateCustomer)
        {
            var customers = LoadCustomers();
            var existingCustomer = customers.FirstOrDefault(c => c.Id == updateCustomer.Id);

            if (existingCustomer != null)
            {
                existingCustomer.FirstName = updateCustomer.FirstName;
                existingCustomer.LastName = updateCustomer.LastName;
                existingCustomer.Email = updateCustomer.Email;
                existingCustomer.Phone = updateCustomer.Phone;
                existingCustomer.Street = updateCustomer.Street;
                existingCustomer.PostalCode = updateCustomer.PostalCode;
                existingCustomer.City = updateCustomer.City;

                SaveCustomers(customers);
            }
        }
    
        public void DelteCustomer(Guid id)
        {
            var customers = LoadCustomers();

            var customerToDelete = customers.FirstOrDefault(c => c.Id == id);

            if (customerToDelete != null)
            {
                customers.Remove(customerToDelete);
                SaveCustomers(customers);
            }
        }

    }
}

    
           

