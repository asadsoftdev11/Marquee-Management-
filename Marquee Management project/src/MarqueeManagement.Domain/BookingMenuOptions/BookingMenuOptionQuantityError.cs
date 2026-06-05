using Volo.Abp;

namespace MarqueeManagement.BookingMenuOptions;

public class BookingMenuOptionQuantityError : BusinessException
{
    public BookingMenuOptionQuantityError(decimal quantity) :
        base(MarqueeManagementDomainErrorCodes.QuantityError)
{
    WithData("quantity", quantity);
}
}