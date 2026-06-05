using Volo.Abp;

namespace MarqueeManagement.BookingMenuOptions;

public class BookingMenuOptionPriceError : BusinessException
{
    public BookingMenuOptionPriceError(decimal PriceAtBookingTime) :
        base(MarqueeManagementDomainErrorCodes.PriceError)
    {
        WithData("PriceAtBookingTime", PriceAtBookingTime);
    }
}
