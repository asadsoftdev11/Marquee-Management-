import type { TrackBookingResultDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TrackBookingService {
  apiName = 'Default';
  

  getByNameOrPhone = (name: string, phone: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TrackBookingResultDto>({
      method: 'GET',
      url: '/api/app/track-booking/by-name-or-phone',
      params: { name, phone },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
