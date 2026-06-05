using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace MarqueeManagement.Customers;

public class CustomerPhoneAlreadyExistsException : BusinessException
{
    public CustomerPhoneAlreadyExistsException(string phone) : base(MarqueeManagementDomainErrorCodes.
        CustomerPhoneExists)
    {
        WithData("phone", phone);
    }

}
