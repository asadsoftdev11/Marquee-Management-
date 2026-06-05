using Volo.Abp;

namespace MarqueeManagement.MenuItems;

public class MenuItemAlreadyExistsException : BusinessException
{
    public MenuItemAlreadyExistsException(string name) : base(MarqueeManagementDomainErrorCodes
        .MenuItemAlreadyExists)
    {
        WithData("name", name);
    }    
}
