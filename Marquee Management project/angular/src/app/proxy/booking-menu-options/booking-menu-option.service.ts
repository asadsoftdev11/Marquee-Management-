import type { BookingMenuOptionDto, CreateBookingMenuOptionDto, GetBookingMenuOptionListDto, UpdateBookingMenuOptionDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BookingMenuOptionService {
  apiName = 'Default';
  

  create = (input: CreateBookingMenuOptionDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BookingMenuOptionDto>({
      method: 'POST',
      url: '/api/app/booking-menu-options',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/booking-menu-options/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BookingMenuOptionDto>({
      method: 'GET',
      url: `/api/app/booking-menu-options/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetBookingMenuOptionListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<BookingMenuOptionDto>>({
      method: 'GET',
      url: '/api/app/booking-menu-options',
      params: { bookingId: input.bookingId, menuItemId: input.menuItemId, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateBookingMenuOptionDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/booking-menu-options/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
