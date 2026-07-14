import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';

@Injectable({
  providedIn: 'root',
})
export class FileAttachmentService {
  apiName = 'Default';
  

  upload = (file: IFormFile, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/file-attachment/upload',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
