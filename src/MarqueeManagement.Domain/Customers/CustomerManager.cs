using System.Numerics;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MarqueeManagement.Customers;

public class CustomerManager : DomainService
{
    private readonly ICustomerRepository _customerRepository;
    public CustomerManager(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<Customer> CreateAsync(
           string name,
           string phone,
           string email,
           string address
        )
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(phone, nameof(phone));
        Check.NotNullOrWhiteSpace(email, nameof(email));
        Check.NotNullOrWhiteSpace(address, nameof(address));

        var existingByPhone = await _customerRepository.FindByPhoneAsync(phone);
        if (existingByPhone != null)
        {
             throw new CustomerPhoneAlreadyExistsException(phone);
        }
        var existingByEmail = await _customerRepository.FindByEmailAsync(email);
        if (existingByEmail != null)
        {
            throw new CustomerEmailAlreadyExists(email);
        }
        return new Customer(
            GuidGenerator.Create(),
            name,
            phone,
            email,
            address
            );

    }
    public async Task UpdateAsync(
        Customer customer,
        string name,
        string phone,
        string email,
        string address
        )
    {
        Check.NotNull(customer, nameof(customer));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(phone, nameof(phone));
        Check.NotNullOrWhiteSpace(email, nameof(email));
        Check.NotNullOrWhiteSpace(address, nameof(address));

        var existingByPhone = await _customerRepository.FindByPhoneAsync(phone);
        if (existingByPhone != null && existingByPhone.Id != customer.Id )
        {
            throw new CustomerPhoneAlreadyExistsException(phone);
        }
        var existingByEmail = await _customerRepository.FindByEmailAsync(email);
        if (existingByEmail != null && existingByEmail.Id != customer.Id )
        {
            throw new CustomerEmailAlreadyExists(email);
        }
        customer.ChangeDetails(name, phone, email, address);

    }
}
