import type { CreateCustomerAttachmentDto, CustomerAttachmentDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CustomerAttachmentService {
  apiName = 'Default';
  

  create = (input: CreateCustomerAttachmentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CustomerAttachmentDto>({
      method: 'POST',
      url: '/api/app/customer-attachment',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/customer-attachment/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CustomerAttachmentDto>({
      method: 'GET',
      url: `/api/app/customer-attachment/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CustomerAttachmentDto>>({
      method: 'GET',
      url: '/api/app/customer-attachment',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
