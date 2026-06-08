using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MarqueeManagement.Customers;

public interface ICustomerRepository : IRepository<Customer, Guid>
{
    Task<Customer?> FindByPhoneAsync(string phone);
    Task<Customer?> FindByEmailAsync(string email);
    Task<List<Customer>> GetAllAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null,
        string? name = null,
        string? email = null
        );
    Task<long> GetCountAsync(
        string? filter = null,
        string? name = null,
        string? email = null
        );
}
