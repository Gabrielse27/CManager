using System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using CManager.Domain;
using CManager.Application.Interfaces;
using System.IO;



namespace CManager.Application.Interfaces
{
    public interface ICustomerRepository
    {
        // Spara lista av kunder till JSON-fil
        void SaveCustomers(List<Customer> customers);

        // Ladda kunder från JSON-fil och returnera listan
        List<Customer> LoadCustomers();

    }
}
