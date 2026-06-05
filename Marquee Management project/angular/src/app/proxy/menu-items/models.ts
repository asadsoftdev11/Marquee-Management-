import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateMenuItemDto {
  name: string;
  description?: string;
  price: number;
  isAvailable: boolean;
  menuCategoryId: string;
  imageUrl?: string;
}

export interface GetMenuItemListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
  name?: string;
  isAvailable?: boolean;
}

export interface MenuItemDto extends EntityDto<string> {
  name?: string;
  description?: string;
  price: number;
  isAvailable: boolean;
  menuCategoryId?: string;
  menuCategoryName?: string;
  imageUrl?: string;
}

export interface UpdateMenuItemDto {
  name: string;
  description?: string;
  price: number;
  isAvailable: boolean;
  menuCategoryId: string;
  imageUrl?: string;
}
