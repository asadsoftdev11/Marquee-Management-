using Volo.Abp;

namespace MarqueeManagement.Customers;

public class CustomerEmailAlreadyExists : BusinessException
{
    public CustomerEmailAlreadyExists(string email): base(MarqueeManagementDomainErrorCodes.CustomerEmailExists)
    {
        WithData("email", email);
    }
}
