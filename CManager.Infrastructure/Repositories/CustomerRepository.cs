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




    }   }   
}
