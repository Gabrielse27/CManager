using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Application.Services;
using CManager.Infrastructure.Repositories;
using CManager.Presentation.ConsoleApp.Controllers;
using CManager.Application.Helpers;


// Skapar Datalagret (Repository)
// Vi instansierar objektet som ansvarar för att läsa/skriva till textfilen.
// Detta måste skapas först eftersom Servicen är beroende av det.
var repository = new CustomerRepository();


// Skapar Logiklagret (Service)
// Här sker "Dependency Injection" manuellt via konstruktorn.
// Vi skickar in (injicerar) vårt repository i servicen, så att servicen kan spara data utan att veta exakt HUR det sparas.
var service = new CustomerService(repository);


// Skapar Presentationslagret (Controller/UI)
// Vi skapar kontrollern och injicerar servicen i den.
// Nu har Controllern tillgång till logiken, som i sin tur har tillgång till databasen.
var controller = new CustomerController(service);

// Startar Applikationen
// Nu när alla beroenden är kopplade, kör vi igång huvudloopen (menyn).
controller.Start();
