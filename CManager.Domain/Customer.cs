using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Adress
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }


    }
}
