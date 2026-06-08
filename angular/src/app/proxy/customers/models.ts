import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateCustomerDto {
  name: string;
  phone: string;
  email: string;
  address: string;
}

export interface CustomerDto extends EntityDto<string> {
  name?: string;
  phone?: string;
  email?: string;
  address?: string;
}

export interface GetCustomerListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
  name?: string;
  email?: string;
}

export interface UpdateCustomerDto {
  name: string;
  phone: string;
  email: string;
  address: string;
}
