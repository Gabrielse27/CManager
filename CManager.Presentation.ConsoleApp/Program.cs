using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Infrastructure.Repositories;

var repo = new CustomerRepository();

// Exempel: spara kunder
var customers = new List<Customer>
{
    new Customer { FirstName = "Gabriel", LastName = "Seres" },
    new Customer { FirstName = "Robert", LastName = "Seres" },
    new Customer { FirstName = "Anna", LastName = "Andersson" },

};

repo.SaveCustomers(customers);

// Exempel: hämta kunder
var loaded = repo.LoadCustomers();

foreach (var c in loaded)
{
    Console.WriteLine($"{c.FirstName} {c.LastName}");
}
