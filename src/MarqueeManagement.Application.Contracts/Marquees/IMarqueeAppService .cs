using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.Marquees;

public interface IMarqueeAppService : IApplicationService
{
    Task<MarqueeDto> GetAsync(Guid id);
    Task<PagedResultDto<MarqueeDto>> GetListAsync(GetMarqueeListDto input);
    Task<MarqueeDto> CreateAsync(CreateMarqueeDto input);
    Task UpdateAsync(Guid id, UpdateMarqueeDto input);
    Task DeleteAsync(Guid id);
}
