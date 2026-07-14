import type { EntityDto } from '@abp/ng.core';

export interface CreateCustomerAttachmentDto {
  customerId?: string;
  fileAttachmentId?: string;
}

export interface CustomerAttachmentDto extends EntityDto<string> {
  customerId?: string;
  fileAttachmentId?: string;
}
