
export interface TrackBookingItemDto {
  bookingId?: string;
  eventType?: string;
  eventDate?: string;
  guestCount: number;
  totalAmount: number;
  status: number;
  marqueeName?: string;
  marqueeLocation?: string;
  marqueePricePerDay: number;
  marqueeCapacity: number;
  hallCharge: number;
  foodTotal: number;
  grandTotal: number;
  menuOptions: TrackBookingMenuItemDto[];
}

export interface TrackBookingMenuItemDto {
  menuItemName?: string;
  quantity: number;
  priceAtBookingTime: number;
  lineTotal: number;
}

export interface TrackBookingResultDto {
  customerName?: string;
  customerPhone?: string;
  customerEmail?: string;
  customerAddress?: string;
  bookings: TrackBookingItemDto[];
}
