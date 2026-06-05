using Asp.Versioning;
using MarqueeManagement.BookingMenuOptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace MarqueeManagement.Controllers.BookingMenuOptions;

[RemoteService(IsEnabled = true)]
[ControllerName("BookingMenuOptions")]
[Area("app")]
[Route("api/app/booking-menu-options")]
public class BookingMenuOptionController : AbpController, IBookingMenuOptionAppService
{
    private readonly IBookingMenuOptionAppService _bookingMenuOptionAppService;

    public BookingMenuOptionController(IBookingMenuOptionAppService bookingMenuOptionAppService)
    {
        _bookingMenuOptionAppService = bookingMenuOptionAppService;
    }

    [HttpGet("{id}")]
    public async Task<BookingMenuOptionDto> GetAsync(Guid id)
    {
        return await _bookingMenuOptionAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<BookingMenuOptionDto>> GetListAsync(GetBookingMenuOptionListDto input)
    {
        return await _bookingMenuOptionAppService.GetListAsync(input);
    }

    [HttpPost]
    public async Task<BookingMenuOptionDto> CreateAsync(CreateBookingMenuOptionDto input)
    {
        return await _bookingMenuOptionAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, UpdateBookingMenuOptionDto input)
    {
        await _bookingMenuOptionAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _bookingMenuOptionAppService.DeleteAsync(id);
    }
}
