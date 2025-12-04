using System;
using System.Collections.Generic;
using System.Text.Json;
using CManager.Domain;
using System.IO;

namespace CManager.Infrastructure.Repositories
{
    public class CustomerRepository 
    {
        private readonly string _filePath = "customers.json";

        public void SaveCustomers(List<Customer> customers)
        {
            var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
            Console.WriteLine("Fil sparad som: " + Path.GetFullPath(_filePath));
        }

        public List<Customer> LoadCustomers()
        {
            if (!File.Exists(_filePath))
                return new List<Customer>(); // Returnera tom lista om fil saknas

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Customer>>(json)!;
        }



    }
}
