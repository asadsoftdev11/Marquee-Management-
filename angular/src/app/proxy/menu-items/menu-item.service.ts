import type { CreateMenuItemDto, GetMenuItemListDto, MenuItemDto, UpdateMenuItemDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MenuItemService {
  apiName = 'Default';
  

  create = (input: CreateMenuItemDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MenuItemDto>({
      method: 'POST',
      url: '/api/app/menu-items',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/menu-items/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MenuItemDto>({
      method: 'GET',
      url: `/api/app/menu-items/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetMenuItemListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MenuItemDto>>({
      method: 'GET',
      url: '/api/app/menu-items',
      params: { filter: input.filter, name: input.name, isAvailable: input.isAvailable, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateMenuItemDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/menu-items/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
