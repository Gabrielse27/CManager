using CManager.Application.Helpers;
using CManager.Application.Interfaces;
using CManager.Application.Services;
using CManager.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net.Http.Headers;



namespace CManager.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        // Test 1 : SPARA NY KUND
        [TestMethod]

        public async Task SaveCustomerAsync_Should_CreateId_And_Call_AddAsync_When_New()

        {
            // ARRANGE
            // Skapar ett "låtsas-repository" (Mock) istället för ett riktigt.
            var mockRepo = new Mock<ICustomerRepository>();

            // Kopplar mocken till servicen så vi slipper riktig databas.
            var service = new CustomerService(mockRepo.Object);

            // Detta lurar servicen att tro att kunden redan finns i databasen (en befintlig kund).
            var newCustomer = new Customer
            {
                FirstName = "Gabriel",
                LastName = "Svensson"
            };

            // ACT
            // Här körs logiken i CustomerService.
            await service.SaveCustomerAsync(newCustomer);

            // ASSERT
           // Vi kontrollerar att IdGenerator i servicen faktiskt kördes.
            Assert.AreNotEqual(Guid.Empty, newCustomer.Id);

            // Vi frågar mocken vad som hände.
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Customer>()), Times.Never);
        }

        //  TEST 2: UPPDATERA KUND
        [TestMethod]
        public async Task SaveCustomerAsync_Should_Call_UpdateAsync_When_Existing()
        {

            // ARRANGE
            var mockRepo = new Mock<ICustomerRepository>();
            var service = new CustomerService(mockRepo.Object);

            var existingCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Anna",
                LastName = "Karlsson"
            };

            // ACT 
            await service.SaveCustomerAsync(existingCustomer);

            // ASSERT

            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Customer>()), Times.Once);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Never);
        }

        // TEST 3: HÄMTA ALLA KUNDER

        [TestMethod]

        public async Task GetCustomersAsync_Should_Return_All_Customers()

        {
            // ARRANGE

            var mockRepo = new Mock<ICustomerRepository>();

            mockRepo.Setup(mockRepo => mockRepo.GetAllAsync())
                .ReturnsAsync(new List<Customer>
                {
                    new Customer { FirstName = "Erik", LastName = "Nilsson"},
                    new Customer { FirstName = "Lisa", LastName = "Andersson"}
                });

            var service = new CustomerService(mockRepo.Object);

            // ACT
            var result = await service.GetCustomersAsync();

            // ASSERT
            // Vi kollar att vi fick tillbaka exakt 2 kunder, precis som vi bestämde i Setup.
            Assert.AreEqual(2, result.Count());

        }

        // TEST 4: TA BORT KUND
        [TestMethod]

        public async Task DeleteCustomer_Should_Call_DeleteAsync_On_Repository()
        {
            // ARRANGE
            // Skapar ett "låtsas-repository" (Mock) istället för ett riktigt.
            var mockRepo = new Mock<ICustomerRepository>();

            // Kopplar mocken till servicen så vi slipper riktig databas.
            var service = new CustomerService(mockRepo.Object);
            var customerid = Guid.NewGuid(); // ID för kunden som ska tas bort


            // ACT 
            await service.DeleteCustomerAsync(customerid);

            // ASSERT
            // Vi verifierar att servicen skickade vidare exakt samma ID till repots Delete-metod.
            mockRepo.Verify(r => r.DeleteAsync(customerid), Times.Once);
        }
    }
}

