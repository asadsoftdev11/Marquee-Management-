import type { CreateCustomerDto, CustomerDto, GetCustomerListDto, UpdateCustomerDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  apiName = 'Default';
  

  create = (input: CreateCustomerDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CustomerDto>({
      method: 'POST',
      url: '/api/app/customers',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/customers/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CustomerDto>({
      method: 'GET',
      url: `/api/app/customers/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCustomerListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CustomerDto>>({
      method: 'GET',
      url: '/api/app/customers',
      params: { filter: input.filter, name: input.name, email: input.email, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateCustomerDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/customers/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
