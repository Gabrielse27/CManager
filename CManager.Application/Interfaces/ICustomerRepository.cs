using System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using CManager.Domain;
using CManager.Application.Interfaces;
using System.IO;




namespace CManager.Application.Interfaces;

    // KRAV: Interface Segregation Principle (ISP) - Små specifika interfaces

    // Ett interface BARA för att läsa(Read)
    public interface IReadRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
    }
    // Ett interface BARA för att skriva/ändra (Write)
    public interface IWriteRepository
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid id);
    }


    // Huvud-interface som binder ihop dem.
    // Detta är det interface du injicerar i  Service.
    public interface ICustomerRepository : IReadRepository , IWriteRepository
   {
    //den ärver allt från de två ovanför.
   }
