
using Volo.Abp;

namespace MarqueeManagement.Marquees;

public class MarqueeAlreadyExistsException : BusinessException
{
    public MarqueeAlreadyExistsException(string name)
        : base(MarqueeManagementDomainErrorCodes.NameAlreadyExists)
    {
        WithData("name", name);
    }
}
