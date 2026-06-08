using Microsoft.AspNetCore.Authorization;
using MarqueeManagement.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.Marquees;
[RemoteService(IsEnabled = false)]
[Authorize(MarqueeManagementPermissions.Marquees.Default)]
public class MarqueeAppService
    : MarqueeManagementAppService, IMarqueeAppService
{
    private readonly IMarqueeRepository _marqueeRepository;
    private readonly MarqueeManager _marqueeManager;

    public MarqueeAppService(
        IMarqueeRepository marqueeRepository,
        MarqueeManager marqueeManager)
    {
        _marqueeRepository = marqueeRepository;
        _marqueeManager = marqueeManager;
    }
    public async Task<MarqueeDto> GetAsync(Guid id)
    {
        var marquee = await _marqueeRepository.GetAsync(id);
        return ObjectMapper.Map<Marquee, MarqueeDto>(marquee);
    }
    public async Task<PagedResultDto<MarqueeDto>> GetListAsync(GetMarqueeListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Marquee.Name);
        }

        var list = await _marqueeRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter,
            input.Name,
            input.Location
        );

        var totalCount = await _marqueeRepository.GetCountAsync(
            input.Filter,
            input.Name,
            input.Location
        );

        var dtoList = ObjectMapper.Map<List<Marquee>, List<MarqueeDto>>(list);
        return new PagedResultDto<MarqueeDto>(totalCount, dtoList);
    }
    [Authorize(MarqueeManagementPermissions.Marquees.Create)]
    public async Task<MarqueeDto> CreateAsync(CreateMarqueeDto input)
    {
        var marquee = await _marqueeManager.CreateAsync(
            input.Name,
            input.Location,
            input.Description,
            input.Capacity,
            input.PricePerDay
        );

        await _marqueeRepository.InsertAsync(marquee);
        return ObjectMapper.Map<Marquee, MarqueeDto>(marquee);
    }

    [Authorize(MarqueeManagementPermissions.Marquees.Edit)]
    public async Task UpdateAsync(Guid id, UpdateMarqueeDto input)
    {
        var marquee = await _marqueeRepository.GetAsync(id);
        await _marqueeManager.UpdateAsync(
            marquee,
            input.Name,
            input.Location,
            input.Description,
            input.Capacity,
            input.PricePerDay
        );

        await _marqueeRepository.UpdateAsync(marquee);
    }

    [Authorize(MarqueeManagementPermissions.Marquees.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _marqueeRepository.DeleteAsync(id);
    }
}