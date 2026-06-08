using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using MarqueeManagement.Bookings;

namespace MarqueeManagement.Controllers;

[RemoteService(IsEnabled = true)]
[ControllerName("Bookings")]
[Area("app")]
[Route("api/app/bookings")]
public class BookingController : AbpController, IBookingAppService
{
    private readonly IBookingAppService _bookingAppService;

    public BookingController(IBookingAppService bookingAppService)
    {
        _bookingAppService = bookingAppService;
    }

    [HttpGet("{id}")]
    public async Task<BookingDto> GetAsync(Guid id)
    {
        return await _bookingAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<BookingDto>> GetListAsync(GetBookingListDto input)
    {
        return await _bookingAppService.GetListAsync(input);
    }

    [HttpPost]
    public async Task<BookingDto> CreateAsync(CreateBookingDto input)
    {
        return await _bookingAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, UpdateBookingDto input)
    {
        await _bookingAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _bookingAppService.DeleteAsync(id);
    }
}