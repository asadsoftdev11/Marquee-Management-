using AutoMapper;
using MarqueeManagement.BookingMenuOptions;
using MarqueeManagement.Bookings;
using MarqueeManagement.Customers;
using MarqueeManagement.Marquees;
using MarqueeManagement.MenuCategories;
using MarqueeManagement.MenuItems;

namespace MarqueeManagement;

public class MarqueeManagementApplicationAutoMapperProfile : Profile
{
    public MarqueeManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Booking, BookingDto>()
        .ForMember(dest => dest.MarqueeName,
         opt => opt.MapFrom(src => src.Marquee.Name))
        .ForMember(dest => dest.CustomerName,
         opt => opt.MapFrom(src => src.Customer.Name));

        CreateMap<BookingMenuOption, BookingMenuOptionDto>()
                 .ForMember(dest => dest.BookingInfo,
                       opt => opt.MapFrom(src => src.Booking.EventType))
                 .ForMember(dest => dest.MenuItemName,
                       opt => opt.MapFrom(src => src.MenuItem.Name));

        CreateMap<Customer, CustomerDto>();
         CreateMap<Marquee, MarqueeDto>();
         CreateMap<MenuCategory, MenuCategoryDto>();

        CreateMap<MenuItem, MenuItemDto>()
        .ForMember(dest => dest.MenuCategoryName,
        opt => opt.MapFrom(src => src.MenuCategory.Name));

    }
}
