import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BookingDto, CreateBookingDto, GetBookingListDto, UpdateBookingDto } from '../bookings/models';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  apiName = 'Default';
  

  create = (input: CreateBookingDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BookingDto>({
      method: 'POST',
      url: '/api/app/bookings',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/bookings/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BookingDto>({
      method: 'GET',
      url: `/api/app/bookings/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetBookingListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<BookingDto>>({
      method: 'GET',
      url: '/api/app/bookings',
      params: { filter: input.filter, eventType: input.eventType, status: input.status, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateBookingDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/bookings/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
