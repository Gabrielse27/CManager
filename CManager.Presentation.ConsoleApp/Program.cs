using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Application.Services;
using CManager.Infrastructure.Repositories;
using CManager.Presentation.ConsoleApp.Controllers;


// 1. Skapa repository
var repository = new CustomerRepository();

// 2. Skapa service och skicka in repository
var service = new CustomerService(repository);

var controller = new CustomerController(service);
controller.Start();

