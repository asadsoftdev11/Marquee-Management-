import { mapEnumToOptions } from '@abp/ng.core';

export enum BookingStatus {
  Pending = 0,
  Confirmed = 1,
  Cancelled = 2,
  Completed = 3,
}

export const bookingStatusOptions = mapEnumToOptions(BookingStatus);
