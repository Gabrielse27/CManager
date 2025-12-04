using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Application.Services;
using CManager.Infrastructure.Repositories;


// 1. Skapa repository
ICustomerRepository repository = new CustomerRepository();

// 2. Skapa service och skicka in repository
ICustomerService service = new CustomerService(repository);


var c1 = service.CreateCustomer(
    "Gabriel",
    "Seres",
    "gabriel.seres@example.com",
    "070-111 11 11",
    "Testgatan 1",
    "302 00",
    "Halmstad");

var c2 = service.CreateCustomer(
    "Robert",
    "Seres",
    "robert.seres@example.com",
    "070-222 22 22",
    "Fotbollsvägen 10",
    "302 10",
    "Halmstad");

var c3 = service.CreateCustomer(
    "Anna",
    "Andersson",
    "anna.andersson@example.com",
    "070-333 33 33",
    "Bergsgatan 5",
    "411 20",
    "Göteborg");


// 2. Visa alla kunder
Console.WriteLine("---- Alla kunder ----");
foreach (var c in service.GetAllCustomers())
{
    Console.WriteLine($"{c.Id} - {c.FirstName} {c.LastName}");
}

// 3. Hämta specifik kund
Console.WriteLine("\n---- Hämta kund ----");
var found = service.GetCustomerById(c2.Id);
Console.WriteLine(found != null
    ? $"Hittad: {found.FirstName} {found.LastName}"
    : "Hittades inte");

// 4. Ta bort en kund
Console.WriteLine("\n---- Ta bort Robert ----");
service.DeleteCustomer(c2.Id);

// 5. Visa kunder efter borttagning
Console.WriteLine("\n---- Kvarstående kunder ----");
foreach (var c in service.GetAllCustomers())
{
    Console.WriteLine($"{c.Id} - {c.FirstName} {c.LastName}");
}
















/*
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
*/