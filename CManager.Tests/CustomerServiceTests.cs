
using CManager.Application.Helpers;
using CManager.Application.Interfaces;
using CManager.Application.Services;
using CManager.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CManager.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public async Task CreateCustomerAsync_Should_Call_Repository_AddAsync()
        {

            // Skapa en "Mock" (falsk version) av Repositoryt
            var mockRepo = new Mock<ICustomerRepository>();

            // Skapa servicen.
            var service = new CustomerService(mockRepo.Object);

            // Skapa kunden som vi ska testa med
            var newCustomer = new Customer
            {
                FirstName = "Gabriel",
                LastName = "Seres",
                Email = "gabriel@example.com",
                Phone = "070101010",
                Street = "Brogatan 1",
                PostalCode = "30258",
                City = "Halmstad"
            };

            // Vi kör den asynkrona metoden
            await service.SaveCustomerAsync(newCustomer);

            // 3. ASSERT (Kontroll)
            // Vi kollar att Repositoryts "AddAsync"-metod blev anropad exakt 1 gång
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
        }
    }
}

