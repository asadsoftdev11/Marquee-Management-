using System;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MarqueeManagement.BookingMenuOptions;

public class BookingMenuOptionManager : DomainService
{
    private readonly IBookingMenuOptionRepository _bookingMenuOptionRepository;
    public BookingMenuOptionManager(IBookingMenuOptionRepository bookingMenuOptionRepository)
    {
        _bookingMenuOptionRepository = bookingMenuOptionRepository;
    }

    public BookingMenuOption Create(
        Guid bookingId,
        Guid menuItemId,
        int quantity,
        decimal priceAtBookingTime
        )
    {
        if (quantity <= 0)
        {
            throw new BookingMenuOptionQuantityError(quantity);
        }

        if (priceAtBookingTime <= 0)
        {
            throw new BookingMenuOptionPriceError(priceAtBookingTime);
        }

        return new BookingMenuOption(
            GuidGenerator.Create(),
            bookingId,
            menuItemId,
            quantity,
            priceAtBookingTime);
    }
    public void Update(BookingMenuOption menuOption,
        Guid bookingId,
        Guid menuItemId,
        int quantity, 
        decimal priceAtBookingTime
        )
    {
        Check.NotNull(menuOption, nameof(menuOption));

        if (quantity <= 0)
        {
            throw new BookingMenuOptionQuantityError(quantity);
        }

        if (priceAtBookingTime <= 0)
        {
            throw new BookingMenuOptionPriceError(priceAtBookingTime);
        }

        menuOption.BookingId = bookingId;
        menuOption.MenuItemId = menuItemId;
        menuOption.Quantity = quantity;
        menuOption.PriceAtBookingTime = priceAtBookingTime;

    }

}
