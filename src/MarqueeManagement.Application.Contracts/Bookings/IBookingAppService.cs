using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.Bookings;

public interface IBookingAppService : IApplicationService
{
    Task<BookingDto> GetAsync(Guid id);
    Task<PagedResultDto<BookingDto>> GetListAsync(GetBookingListDto input);
    Task<BookingDto> CreateAsync(CreateBookingDto input);
    Task UpdateAsync(Guid id, UpdateBookingDto input);
    Task DeleteAsync(Guid id);

}
