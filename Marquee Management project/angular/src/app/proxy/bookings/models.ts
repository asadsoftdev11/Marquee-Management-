import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { BookingStatus } from './booking-status.enum';

export interface BookingDto extends EntityDto<string> {
  eventDate?: string;
  eventType?: string;
  guestCount: number;
  totalAmount: number;
  status?: BookingStatus;
  marqueeId?: string;
  marqueeName?: string;
  customerId?: string;
  customerName?: string;
}

export interface CreateBookingDto {
  eventDate: string;
  eventType: string;
  guestCount: number;
  totalAmount: number;
  status: BookingStatus;
  marqueeId: string;
  customerId: string;
}

export interface GetBookingListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
  eventType?: string;
  status?: BookingStatus;
}

export interface UpdateBookingDto {
  eventDate: string;
  eventType: string;
  guestCount: number;
  totalAmount: number;
  status: BookingStatus;
  marqueeId: string;
  customerId: string;
}
