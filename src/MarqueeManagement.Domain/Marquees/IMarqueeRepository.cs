using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MarqueeManagement.Marquees;

public interface IMarqueeRepository : IRepository<Marquee, Guid>
{
    Task<Marquee> FindByNameAsync(string name);
    Task<List<Marquee>> GetListAsync(
       int skipCount,
       int maxResultCount,
       string sorting,
       string? filter = null,
       string? name = null,
       string? location = null
       );
    Task<long> GetCountAsync(
        string? filter = null,
        string? name = null,
        string? location = null
        );
}