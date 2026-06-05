using MarqueeManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.Customers;
[RemoteService(IsEnabled = false)]

[Authorize(MarqueeManagementPermissions.Customers.Default)]
public class CustomerAppService
    : MarqueeManagementAppService, ICustomerAppService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerManager _customerManager;

    public CustomerAppService(
        ICustomerRepository customerRepository,
        CustomerManager customerManager)
    {
        _customerRepository = customerRepository;
        _customerManager = customerManager;
    }
    public async Task<CustomerDto> GetAsync(Guid id)
    {
        var customer = await _customerRepository.GetAsync(id);
        return ObjectMapper.Map<Customer, CustomerDto>(customer);
    }
    public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Customer.Name);
        }

        var list = await _customerRepository.GetAllAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter,
            input.Name,
            input.Email
        );

        var totalCount = await _customerRepository.GetCountAsync(
            input.Filter,
            input.Name,
            input.Email
        );

        var dtoList = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(list);
        return new PagedResultDto<CustomerDto>(totalCount, dtoList);
    }
    [Authorize(MarqueeManagementPermissions.Customers.Create)]
    public async Task<CustomerDto> CreateAsync(CreateCustomerDto input)
    {
        var customer = await _customerManager.CreateAsync(
            input.Name,
            input.Phone,
            input.Email,
            input.Address
        );

        await _customerRepository.InsertAsync(customer);
        return ObjectMapper.Map<Customer, CustomerDto>(customer);
    }
    [Authorize(MarqueeManagementPermissions.Customers.Edit)]
    public async Task UpdateAsync(Guid id, UpdateCustomerDto input)
    {
        var customer = await _customerRepository.GetAsync(id);

        await _customerManager.UpdateAsync(
            customer,
            input.Name,
            input.Phone,
            input.Email,
            input.Address
        );

        await _customerRepository.UpdateAsync(customer);
    }

    [Authorize(MarqueeManagementPermissions.Customers.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
    }
}