using CManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.Interfaces
{
    public interface ICustomerService
    {
        
        Customer CreateCustomer(string firstName, string lastname, string email, string phoneNumber, string street, string postalCode, string city);

        // Hämta alla kunder //
        List<Customer> GetAllCustomers();

        // Hämta kund via ID //
        Customer? GetCustomerById(Guid id);

        // Hämta kund via e-postadress //
        Customer? GetCustomerByEmail(string email);

        // Ta bort kund via ID
        bool DeleteCustomer(Guid id);

        // Ta bort kund via epostadress //
        bool DeleteCustomerByEmail(string email);

        // Spara lista till fil
        void SaveChanges();

    }
}
