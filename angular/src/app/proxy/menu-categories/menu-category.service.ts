import type { CreateMenuCategoryDto, GetMenuCategoryListDto, MenuCategoryDto, UpdateMenuCategoryDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MenuCategoryService {
  apiName = 'Default';
  

  create = (input: CreateMenuCategoryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MenuCategoryDto>({
      method: 'POST',
      url: '/api/app/menu-categories',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/menu-categories/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MenuCategoryDto>({
      method: 'GET',
      url: `/api/app/menu-categories/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetMenuCategoryListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MenuCategoryDto>>({
      method: 'GET',
      url: '/api/app/menu-categories',
      params: { filter: input.filter, name: input.name, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateMenuCategoryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/menu-categories/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
