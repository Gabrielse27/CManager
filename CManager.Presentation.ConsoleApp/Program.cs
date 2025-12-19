using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Application.Services;
using CManager.Infrastructure.Repositories;
using CManager.Presentation.ConsoleApp.Controllers;
using CManager.Application.Helpers;


//Skapa repository
var repository = new CustomerRepository();


// Skapa service
// Vi skickar  in repositoryt. Servicen sköter ID-skapandet själv internt.
var service = new CustomerService(repository);

//  Starta kontrollern
var controller = new CustomerController(service);
controller.Start();
