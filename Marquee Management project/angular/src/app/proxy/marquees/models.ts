import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateMarqueeDto {
  name: string;
  location: string;
  description?: string;
  capacity: number;
  pricePerDay: number;
}

export interface GetMarqueeListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
  name?: string;
  location?: string;
}

export interface MarqueeDto extends EntityDto<string> {
  name?: string;
  location?: string;
  description?: string;
  capacity: number;
  pricePerDay: number;
}

export interface UpdateMarqueeDto {
  name: string;
  location: string;
  description?: string;
  capacity: number;
  pricePerDay: number;
}
