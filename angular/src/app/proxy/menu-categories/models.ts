import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateMenuCategoryDto {
  name: string;
  description?: string;
}

export interface GetMenuCategoryListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
  name?: string;
}

export interface MenuCategoryDto extends EntityDto<string> {
  name?: string;
  description?: string;
}

export interface UpdateMenuCategoryDto {
  name: string;
  description?: string;
}
