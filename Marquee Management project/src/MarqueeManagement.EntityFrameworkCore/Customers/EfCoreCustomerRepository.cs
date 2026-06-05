using MarqueeManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MarqueeManagement.Customers;

public class EfCoreCustomerRepository
    : EfCoreRepository<MarqueeManagementDbContext, Customer, Guid>,
      ICustomerRepository
{
    public EfCoreCustomerRepository(
        IDbContextProvider<MarqueeManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
    public async Task<Customer?> FindByPhoneAsync(string phone)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Phone == phone);
    }

    public async Task<Customer?> FindByEmailAsync(string email)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task<List<Customer>> GetAllAsync(
      int skipCount,
      int maxResultCount,
      string sorting,
      string? filter = null,
      string? name = null,
      string? email = null)
    {
        if (string.IsNullOrWhiteSpace(sorting))
        {
            sorting = nameof(Customer.Name);
        }

        var query = await GetQueryableAsync(filter, name, email);

        query = query.OrderBy(sorting)
                     .Skip(skipCount)
                     .Take(maxResultCount);

        return await query.ToListAsync();
    }
    public async Task<long> GetCountAsync(
       string? filter = null,
       string? name = null,
       string? email = null)
    {
        var query = await GetQueryableAsync(filter, name, email);
        return await query.LongCountAsync();
    }
    private async Task<IQueryable<Customer>> GetQueryableAsync(
     string? filter = null,
     string? name = null,
     string? email = null)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet.AsQueryable();

        query = query
            .WhereIf(!filter.IsNullOrWhiteSpace(),
                x => x.Name.Contains(filter!) || x.Phone.Contains(filter) || x.Email.Contains(filter!))
            .WhereIf(!name.IsNullOrWhiteSpace(),
                x => x.Name.Contains(name!))
            .WhereIf(!email.IsNullOrWhiteSpace(),
                x => x.Email.Contains(email!));

        return query;
    }
}