import type { CreateMarqueeDto, GetMarqueeListDto, MarqueeDto, UpdateMarqueeDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MarqueeService {
  apiName = 'Default';
  

  create = (input: CreateMarqueeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MarqueeDto>({
      method: 'POST',
      url: '/api/app/marquees',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/marquees/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MarqueeDto>({
      method: 'GET',
      url: `/api/app/marquees/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetMarqueeListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MarqueeDto>>({
      method: 'GET',
      url: '/api/app/marquees',
      params: { filter: input.filter, name: input.name, location: input.location, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateMarqueeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/marquees/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
