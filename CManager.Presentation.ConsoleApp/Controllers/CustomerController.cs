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
                      //  CreateCustomerDialog();
                            break;

                    case "2":
                        //ShowAllCustomersDialog();
                        break;

                    case "3":
                        //ShowSpecificCustomerDialog();
                        break;

                    case "4":
                       // DeleteCustomerDialog();
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
















    }
}