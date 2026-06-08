using MarqueeManagement.Bookings;
using MarqueeManagement.MenuItems;
using MarqueeManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.BookingMenuOptions;

[RemoteService(IsEnabled = false)]
[Authorize(MarqueeManagementPermissions.BookingMenuOptions.Default)]
public class BookingMenuOptionAppService : MarqueeManagementAppService, IBookingMenuOptionAppService
{
    private readonly IBookingMenuOptionRepository _bookingMenuOptionRepository;
    private readonly BookingMenuOptionManager _bookingMenuOptionManager;
    private readonly IBookingRepository _bookingRepository;
    private readonly IMenuItemRepository _menuItemRepository;

    public BookingMenuOptionAppService(
       IBookingMenuOptionRepository bookingMenuOptionRepository,
       BookingMenuOptionManager bookingMenuOptionManager,
       IBookingRepository bookingRepository,
       IMenuItemRepository menuItemRepository)
    {
        _bookingMenuOptionRepository = bookingMenuOptionRepository;
        _bookingMenuOptionManager = bookingMenuOptionManager;
        _bookingRepository = bookingRepository;
        _menuItemRepository = menuItemRepository;
    }

    public async Task<BookingMenuOptionDto> GetAsync(Guid id)
    {
        var entity = await _bookingMenuOptionRepository.GetAsync(id);
        return ObjectMapper.Map<BookingMenuOption, BookingMenuOptionDto>(entity);
    }
    public async Task<PagedResultDto<BookingMenuOptionDto>> GetListAsync(GetBookingMenuOptionListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(BookingMenuOption.PriceAtBookingTime);
        }

        var list = await _bookingMenuOptionRepository.GetListAsync(
           input.SkipCount,
           input.MaxResultCount,
           input.Sorting,
           input.BookingId,
           input.MenuItemId,
           input.Filter
           );

        var totalCount = await _bookingMenuOptionRepository.GetCountAsync(
           input.BookingId,
           input.MenuItemId,
           input.Filter
            );

        var dtoList = ObjectMapper.Map<List<BookingMenuOption>, List<BookingMenuOptionDto>>(list);

        return new PagedResultDto<BookingMenuOptionDto>(totalCount, dtoList);
    }

    [Authorize(MarqueeManagementPermissions.BookingMenuOptions.Create)]
    public async Task<BookingMenuOptionDto> CreateAsync(CreateBookingMenuOptionDto input)
    {
        await _bookingRepository.GetAsync(input.BookingId);
        await _menuItemRepository.GetAsync(input.MenuItemId);
        var entity = _bookingMenuOptionManager.Create(
            input.BookingId,
            input.MenuItemId,
            input.Quantity,
            input.PriceAtBookingTime
        );

        await _bookingMenuOptionRepository.InsertAsync(entity);

        return ObjectMapper.Map<BookingMenuOption, BookingMenuOptionDto>(entity);
    }

    [Authorize(MarqueeManagementPermissions.BookingMenuOptions.Edit)]
    public async Task UpdateAsync(Guid id, UpdateBookingMenuOptionDto input)
    {
        var booking = await _bookingRepository.GetAsync(input.BookingId);
        var menuItem = await _menuItemRepository.GetAsync(input.MenuItemId);
        var entity = await _bookingMenuOptionRepository.GetAsync(id);
        _bookingMenuOptionManager.Update(
             entity,
             booking.Id,
             menuItem.Id,
             input.Quantity,
             input.PriceAtBookingTime
            );
        await _bookingMenuOptionRepository.UpdateAsync(entity);
    }

    [Authorize(MarqueeManagementPermissions.BookingMenuOptions.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _bookingMenuOptionRepository.DeleteAsync(id);
    }
}