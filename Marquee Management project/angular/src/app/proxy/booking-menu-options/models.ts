import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface BookingMenuOptionDto extends EntityDto<string> {
  quantity: number;
  priceAtBookingTime: number;
  bookingId?: string;
  bookingInfo?: string;
  menuItemId?: string;
  menuItemName?: string;
}

export interface CreateBookingMenuOptionDto {
  quantity: number;
  priceAtBookingTime: number;
  bookingId: string;
  menuItemId: string;
}

export interface GetBookingMenuOptionListDto extends PagedAndSortedResultRequestDto {
  bookingId?: string;
  menuItemId?: string;
  filter?: string;
}

export interface UpdateBookingMenuOptionDto {
  quantity: number;
  priceAtBookingTime: number;
  bookingId: string;
  menuItemId: string;
}
