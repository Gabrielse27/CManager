using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Domain
{
    public class Customer
    {

        // GUID (Globally Unique Identifier) är ett unikt ID som datorn skapar.
        public Guid Id { get; set; }  // skilljer på 2 kunder med samma namn 
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

        // Adress
        // public string Address { get; set; } = "";
        public string Street { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string City { get; set; } = "";


    }
}
