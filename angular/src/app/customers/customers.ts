import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto, ConfigStateService } from '@abp/ng.core';
import { CustomerService, CustomerDto, GetCustomerListDto } from '../proxy/customers';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { CardModule, ConfirmationService, Confirmation, ToasterService, ThemeSharedModule } from '@abp/ng.theme.shared';
import { PageModule } from '@abp/ng.components/page';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-customers',
  templateUrl: './customers.html',
  styleUrls: ['./customers.scss'],
  host: { class: 'app-dark-page' },
  imports: [
    CommonModule, FormsModule, ReactiveFormsModule,
    CardModule, PageModule,
    NgbDropdownModule, ThemeSharedModule, NgxDatatableModule,
  ],
  providers: [ListService],
})
export class Customers implements OnInit {
  customers = { items: [], totalCount: 0 } as PagedResultDto<CustomerDto>;
  isModalOpen = false;
  showFilter = false;
  form!: FormGroup;
  selectedCustomer = {} as CustomerDto;
  filters = {} as GetCustomerListDto;
  selectedFile?: File;
  previewUrl: string | null = null;

  constructor(
    public readonly list: ListService,
    private customerService: CustomerService,
    private toaster: ToasterService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private http: HttpClient,
    private configState: ConfigStateService,
  ) {}

  private get apiUrl(): string {
    return this.configState.getDeep('environment.apis.default.url');
  }

  ngOnInit(): void {
    const streamCreator = (query: any) =>
      this.customerService.getList({ ...query, ...this.filters });
    this.list.hookToQuery(streamCreator).subscribe(res => {
      this.customers = res;
    });
  }

  buildForm(): void {
    this.form = this.fb.group({
      name:    [this.selectedCustomer.name    || '', Validators.required],
      phone:   [this.selectedCustomer.phone   || '', Validators.required],
      email:   [this.selectedCustomer.email   || '', Validators.required],
      address: [this.selectedCustomer.address || '', Validators.required],
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (!file) return;
    this.selectedFile = file;
    if (file.type.startsWith('image/')) {
      const reader = new FileReader();
      reader.onload = (e: any) => { this.previewUrl = e.target.result; };
      reader.readAsDataURL(file);
    } else {
      this.previewUrl = null;
    }
  }

  clearFile(fileInput: HTMLInputElement): void {
    this.selectedFile = undefined;
    this.previewUrl = null;
    fileInput.value = '';
  }

  async uploadFile(): Promise<string | null> {
    if (!this.selectedFile) return null;
    const formData = new FormData();
    formData.append('file', this.selectedFile, this.selectedFile.name);
    const response = await firstValueFrom(
      this.http.post<string>(`${this.apiUrl}/api/app/file-attachment/upload`, formData)
    );
    return response;
  }

  saveCustomerAttachment(customerId: string, fileAttachmentId: string): void {
    this.http.post(`${this.apiUrl}/api/app/customer-attachment`, {
      customerId,
      fileAttachmentId,
    }).subscribe();
  }

  createCustomer(): void {
    this.selectedCustomer = {} as CustomerDto;
    this.selectedFile = undefined;
    this.previewUrl = null;
    this.buildForm();
    this.isModalOpen = true;
  }

  editCustomer(id: string): void {
    this.customerService.get(id).subscribe(res => {
      this.selectedCustomer = res;
      this.selectedFile = undefined;
      this.previewUrl = null;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: string): void {
    this.confirmation.warn('This action cannot be undone.', 'Delete Record')
      .subscribe(status => {
        if (status === Confirmation.Status.confirm) {
          this.customerService.delete(id).subscribe(() => {
            this.list.get();
            this.toaster.success('Record deleted successfully.');
          });
        }
      });
  }

  previewFile(fileId: string): void {
    window.open(`${this.apiUrl}/api/app/file-attachment/download/${fileId}`, '_blank');
  }

  clearFilters(): void {
    this.filters = {} as GetCustomerListDto;
    this.list.get();
    this.form?.reset();
  }

  async save(): Promise<void> {
    if (this.form.invalid) return;
    if (this.selectedCustomer?.id && !this.form.dirty && !this.selectedFile) {
      this.toaster.info('Nothing changed');
      return;
    }
    const data = this.form.value;
    if (this.selectedCustomer?.id) {
      this.customerService.update(this.selectedCustomer.id, data).subscribe(async () => {
        const fileId = await this.uploadFile();
        if (fileId) this.saveCustomerAttachment(this.selectedCustomer.id!, fileId);
        this.afterSave('Updated Successfully');
      });
    } else {
      this.customerService.create(data).subscribe(async customer => {
        const fileId = await this.uploadFile();
        if (fileId) this.saveCustomerAttachment(customer.id!, fileId);
        this.afterSave('Created Successfully');
      });
    }
  }

  private afterSave(msg: string): void {
    this.isModalOpen = false;
    this.form.reset();
    this.selectedFile = undefined;
    this.previewUrl = null;
    this.list.get();
    this.toaster.success(msg);
  }
}