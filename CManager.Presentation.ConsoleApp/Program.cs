using CManager.Application.Interfaces;
using CManager.Domain;
using CManager.Application.Services;
using CManager.Infrastructure.Repositories;
using CManager.Presentation.ConsoleApp.Controllers;
using CManager.Application.Helpers;


// 1. Skapa repository
var repository = new CustomerRepository();

var guidFactory = new GuidFactory();


// 2. Skapa service och skicka in repository + guidFactory
var service = new CustomerService(repository, guidFactory);

var controller = new CustomerController(service);
controller.Start();

