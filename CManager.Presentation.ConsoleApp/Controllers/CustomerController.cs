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


    }
}