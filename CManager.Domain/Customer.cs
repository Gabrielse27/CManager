using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }  // skilljer på 2 kunder med samma namn 
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = " ";

        // Adress
        public string Street { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string City { get; set; } = "";


    }
}
