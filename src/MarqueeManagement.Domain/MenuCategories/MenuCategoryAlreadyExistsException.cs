using Volo.Abp;

namespace MarqueeManagement.MenuCategories;

public class MenuCategoryAlreadyExistsException : BusinessException
{
    public MenuCategoryAlreadyExistsException(string name)
        : base(MarqueeManagementDomainErrorCodes.MenuCategoryAlreadyExists)
    {
        WithData("name", name);
    }
}