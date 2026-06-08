using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.BookingMenuOptions;

public interface IBookingMenuOptionAppService : IApplicationService
{
    Task<BookingMenuOptionDto> GetAsync(Guid id);

    Task<PagedResultDto<BookingMenuOptionDto>> GetListAsync(GetBookingMenuOptionListDto input);

    Task<BookingMenuOptionDto> CreateAsync(CreateBookingMenuOptionDto input);

    Task UpdateAsync(Guid id, UpdateBookingMenuOptionDto input);

    Task DeleteAsync(Guid id);


}
