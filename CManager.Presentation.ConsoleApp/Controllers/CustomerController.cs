using System;
using System.Collections.Generic;
using System.Text;
using CManager.Application.Interfaces;
using CManager.Domain;




namespace CManager.Presentation.ConsoleApp.Controllers
{
    public class CustomerController
    {

        // "readonly" betyder att vi bara får sätta denna en gång (i konstruktorn).
        // Vi använder Interfacet (ICustomerService) för att följa Dependency Injection-principen.
        // "Dependency Injection" används för att frikoppla klasserna från varandra. Istället för att klasserna skapar sina egna beroenden (med 'new'), injiceras de via konstruktorn.
        private readonly ICustomerService _service;

        // Konstruktorn: Denna körs när programmet startar upp.
        // Här tar vi emot (injicerar) den färdiga Servicen så att Controllern kan använda den.
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // Huvudmetoden som startar menysystemet.
        public void Start()
        {
            // while (true) skapar en "evig loop". 
            // Detta gör att programmet inte stängs av efter ett val, utan återvänder till menyn.
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

                // Läser in vad användaren skriver. "!" betyder att vi lovar att det inte är null.
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


        // Metod för att skapa en ny kund.
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


            // Vi skickar objektet till Servicen som sparar det till filen.
            // .GetAwaiter().GetResult() används för att tvinga koden att vänta på att sparningen blir klar 
            // (eftersom detta är en synkron konsol-metod men servicen är asynkron).
            _service.SaveCustomerAsync(newCustomer).GetAwaiter().GetResult();
     

            Console.WriteLine($"\nKund Skapad ");
            Console.ReadKey();

        }

        // -----VISA ALLA KUNDER-----//
        private void ShowAllCustomersDialog()
        {
            Console.Clear();
            Console.WriteLine("-----ALLA KUNDER-----");
            // Hämtar alla kunder från servicen.
            var customers = _service.GetCustomersAsync().GetAwaiter().GetResult();

            // Vi loopar igenom varje kund (c) i listan och skriver ut den på skärmen.
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

            // Hämtar alla kunder
            var customers = _service.GetCustomersAsync().GetAwaiter().GetResult();

            // sedan använder vi LINQ (FirstOrDefault) för att leta upp RÄTT kund baserat på e-posten.
            // x => x.Email == email betyder "Leta efter den kund vars Email matchar det vi skrev in".
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
        // Metod för att ta bort kunden
        private void DeleteCustomerDialog()
        {
            Console.Clear();
            Console.WriteLine("-----Ta Bort Kund-----");
            Console.WriteLine("Skriv Epostadressen: ");
            var email = Console.ReadLine() ?? "";

            // Vi måste först hitta kunden för att få tag på dess ID.
            var customers = _service.GetCustomersAsync().GetAwaiter().GetResult();
            var customerToDelete = customers.FirstOrDefault(x => x.Email == email);


            if (customerToDelete != null) 
            {
                // Om kunden finns, tar vi dess ID och skickar till Servicens Delete-metod.
                _service.DeleteCustomerAsync(customerToDelete.Id).GetAwaiter().GetResult();
                Console.WriteLine("Kunden är Bortagen!");
            }
            else
                Console.WriteLine("Kunden Hittades inte!");
            Console.ReadKey();
        }
       

    }
}