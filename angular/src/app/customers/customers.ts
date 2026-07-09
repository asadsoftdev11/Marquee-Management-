import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { CustomerService, CustomerDto, GetCustomerListDto } from '../proxy/customers';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import {
  CardModule,
  ConfirmationService,
  Confirmation,
  ModalComponent,
  ToasterService,
  ThemeSharedModule,
} from '@abp/ng.theme.shared';
import { PageModule } from '@abp/ng.components/page';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

//for file upload
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-customers',
  templateUrl: './customers.html',
  styleUrls: ['./customers.scss'],
  host: {
    class: 'app-dark-page',
  },
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule,
    ModalComponent,
    PageModule,
    NgbDropdownModule,
    ThemeSharedModule,
    NgxDatatableModule,
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
  //for fileupload
  selectedFile?: File;
  uploadedFileId?: string;

  constructor(
    public readonly list: ListService,
    private customerService: CustomerService,
    private toaster: ToasterService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    //for fileupload
    private http: HttpClient,
  ) {}

  ngOnInit(): void {
    const streamCreator = (query: any) =>
      this.customerService.getList({ ...query, ...this.filters });

    this.list.hookToQuery(streamCreator).subscribe(res => {
      this.customers = res;
    });
  }

  buildForm(): void {
    this.form = this.fb.group({
      name: [this.selectedCustomer.name || '', Validators.required],
      phone: [this.selectedCustomer.phone || '', Validators.required],
      email: [this.selectedCustomer.email || '', Validators.required],
      address: [this.selectedCustomer.address || '', Validators.required],
    });
  }

  //For fileuploading
  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }
  async uploadFile(): Promise<string | null> {
    if (!this.selectedFile) {
      return null;
    }
    const formData = new FormData();

    formData.append('file', this.selectedFile, this.selectedFile.name);

    const response = await firstValueFrom(
      this.http.post<any>(
        'https://marqueemanagement.runasp.net/api/app/file-attachment/upload',
        formData,
      ),
    );
    this.uploadedFileId = response.id;
    return response.id;
  }
  saveCustomerAttachment(customerId: string | undefined, fileAttachmentId: string): void {
    this.http
      .post('https://marqueemanagement.runasp.net/api/app/customer-attachment', {
        customerId,
        fileAttachmentId,
      })
      .subscribe();
  }

  createCustomer(): void {
    this.selectedCustomer = {} as CustomerDto;
    this.selectedFile = undefined;
    this.buildForm();
    this.isModalOpen = true;
  }

  editCustomer(id: string): void {
    this.customerService.get(id).subscribe(res => {
      this.selectedCustomer = res;
      this.selectedFile = undefined;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: string): void {
    this.confirmation.warn('This action cannot be undone.', 'Delete Record').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.customerService.delete(id).subscribe(() => {
          this.list.get();
          this.toaster.success('Record deleted successfully.');
        });
      }
    });
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
        // file upload for existing customer
        const fileId = await this.uploadFile();

        if (fileId) {
          if (this.selectedCustomer.id) {
            this.saveCustomerAttachment(this.selectedCustomer.id, fileId);
          }
        }

        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Updated Successfully');
      });
    } else {
      this.customerService.create(data).subscribe(async customer => {
        // upload file after customer creation
        const fileId = await this.uploadFile();

        if (fileId) {
          if (customer.id) {
            this.saveCustomerAttachment(customer.id, fileId);
          }
        }

        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Created Successfully');
      });
    }
  }
}
