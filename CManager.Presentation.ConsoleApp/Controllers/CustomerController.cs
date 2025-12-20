using System;
using System.Collections.Generic;
using System.Text;
using CManager.Application.Interfaces;
using CManager.Domain;




namespace CManager.Presentation.ConsoleApp.Controllers
{
    public class CustomerController
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----Kundhantering-----");
                Console.WriteLine("1. Skapa kund");
                Console.WriteLine("2. Visa kunder");
                Console.WriteLine("3. Visa specifik kund");
                Console.WriteLine("4. Ta bort kund");
                Console.WriteLine("5. Avsluta");
                Console.Write("Välj ett alternativ: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        CreateCustomerDialog();
                            break;

                    case "2":
                        ShowAllCustomersDialog();
                        break;

                    case "3":
                        ShowSpecificCustomerDialog();
                        break;

                    case "4":
                        DeleteCustomerDialog();
                        break;

                    case "5":
                        return;

                    default:
                        {
                            Console.WriteLine("Fel val, försök igen");
                            Console.ReadKey();
                            break;
                        }
                }
                  
            }

        }


        // ---SKAPA KUND-----//
        private void CreateCustomerDialog()
        {
            

            Console.Clear();
            Console.WriteLine("-----Skapa Kund-----");

            Console.Write("Förnamn: ");
            var first = Console.ReadLine() ?? "";

            Console.Write("Efternamn: ");
            var last = Console.ReadLine() ?? "";

            Console.Write("E-postadress: ");
            var email = Console.ReadLine() ?? "";

            Console.Write("Telefonnummer: ");
            var phone = Console.ReadLine() ?? "";

            Console.Write("Gatuadress: ");
            var street = Console.ReadLine() ?? "";

            Console.Write("Postnummer: ");
            var postal = Console.ReadLine() ?? "";

            Console.Write("Ort: ");
            var city = Console.ReadLine() ?? "";


            var newCustomer = new Customer
            {
                FirstName = first,
                LastName = last,
                Email = email,
                Phone = phone,
                Street = street,
                PostalCode = postal,
                City = city
            };



            _service.SaveCustomerAsync(newCustomer).GetAwaiter().GetResult();
     

            Console.WriteLine($"\nKund Skapad ");
            Console.ReadKey();

        }

        // -----VISA ALLA KUNDER-----//
        private void ShowAllCustomersDialog()
        {
            Console.Clear();
            Console.WriteLine("-----ALLA KUNDER-----");

            var customers = _service.GetCustomersAsync().GetAwaiter().GetResult();

            foreach ( var c in customers)
            {
                Console.WriteLine($"{c.FirstName} {c.LastName} - {c.Email}");
            }

            Console.ReadKey();

        }

        //-----VISA SPECIFIK KUND-----//
        private void ShowSpecificCustomerDialog()

        {
            Console.Clear();
            Console.WriteLine("----Visa Kund----");
            Console.Write("Skriv Kundens e-post: ");
            var email = Console.ReadLine() ?? "";

            var customers = _service.GetCustomersAsync().GetAwaiter().GetResult();
            var customer = customers.FirstOrDefault(x => x.Email == email);

            if ( customer == null)
            {
                Console.WriteLine("Kunden Hittades inte: ");
            }
            else
            {
                Console.WriteLine($"\nName: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine($"Telephone: {customer.Phone}");
                Console.WriteLine($"E-postadres: {customer.Email}");
                Console.WriteLine($"Adress: {customer.Street}, {customer.PostalCode}, {customer.City}");
            }

            Console.ReadKey();

        }

        private void DeleteCustomerDialog()
        {
            Console.Clear();
            Console.WriteLine("-----Ta Bort Kund-----");
            Console.WriteLine("Skriv Epostadressen: ");
            var email = Console.ReadLine() ?? "";

            var customers = _service.GetCustomersAsync().GetAwaiter().GetResult();
            var customerToDelete = customers.FirstOrDefault(x => x.Email == email);

         
            if (customerToDelete != null)
            {

                _service.DeleteCustomerAsync(customerToDelete.Id).GetAwaiter().GetResult();
                Console.WriteLine("Kunden är Bortagen!");
            }
            else
                Console.WriteLine("Kunden Hittades inte!");
            Console.ReadKey();
        }
       

    }
}