
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CManager.Application.Interfaces;
using CManager.Application.Services;
using CManager.Domain;
using System.Collections.Generic;

namespace CManager.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public void CreateCustomer_ShouldReturnCustomerWithCorrectValues()
        {
            // Arrange – mock repository
            var mockRepo = new Mock<ICustomerRepository>();

            mockRepo.Setup(r => r.LoadCustomers())
                    .Returns(new List<Customer>()); // return tom lista

            var service = new CustomerService(mockRepo.Object);

            // Act – skapa kund
            var customer = service.CreateCustomer(
                "Gabriel",
                "Seres",
                "gabriel@example.com",
                "070101010",
                "Brogatan 1",
                "30258",
                "Halmstad"
            );

            // Assert – kontrollera resultatet
            Assert.IsNotNull(customer);
            Assert.AreEqual("Gabriel", customer.FirstName);
            Assert.AreEqual("Seres", customer.LastName);
            Assert.AreEqual("gabriel@example.com", customer.Email);
        }
    }
}

