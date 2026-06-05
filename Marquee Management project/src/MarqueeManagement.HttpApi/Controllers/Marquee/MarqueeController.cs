using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace MarqueeManagement.Marquees;

[RemoteService(IsEnabled = true)]
[ControllerName("Marquees")]
[Area("app")]
[Route("api/app/marquees")]
public class MarqueeController : AbpController, IMarqueeAppService
{
    private readonly IMarqueeAppService _marqueeAppService;

    public MarqueeController(IMarqueeAppService marqueeAppService)
    {
        _marqueeAppService = marqueeAppService;
    }

    [HttpGet("{id}")]
    public async Task<MarqueeDto> GetAsync(Guid id)
    {
        return await _marqueeAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<MarqueeDto>> GetListAsync(GetMarqueeListDto input)
    {
        return await _marqueeAppService.GetListAsync(input);
    }

    [HttpPost]
    public async Task<MarqueeDto> CreateAsync(CreateMarqueeDto input)
    {
        return await _marqueeAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, UpdateMarqueeDto input)
    {
        await _marqueeAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _marqueeAppService.DeleteAsync(id);
    }
}