using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.TrackBookings;

public interface ITrackBookingAppService : IApplicationService
{
    Task<TrackBookingResultDto> GetByNameOrPhoneAsync(
        string? name,
        string? phone
    );
}