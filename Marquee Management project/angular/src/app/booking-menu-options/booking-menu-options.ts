import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { BookingMenuOptionService, BookingMenuOptionDto, GetBookingMenuOptionListDto } from '../proxy/booking-menu-options';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbDateAdapter, NgbDateNativeAdapter, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { CardModule, ConfirmationService, Confirmation, ModalComponent, ToasterService, ThemeSharedModule } from '@abp/ng.theme.shared';
import { PageModule } from '@abp/ng.components/page';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { BookingService } from '../proxy/controllers';
import { BookingDto } from '../proxy/bookings';
import { MenuItemService, MenuItemDto } from '../proxy/menu-items';

@Component({
  standalone: true,
  selector: 'app-booking-menu-options',
  templateUrl: './booking-menu-options.html',
  styleUrls: ['./booking-menu-options.scss'],
   host: { class: 'app-dark-page' },
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule, 
    ModalComponent,
    PageModule,
    NgbDropdownModule,
    ThemeSharedModule,
    NgxDatatableModule
  ],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class BookingMenuOptions implements OnInit {
  items = { items: [], totalCount: 0 } as PagedResultDto<BookingMenuOptionDto>;
  bookingsList: BookingDto[] = [];
  menuItemsList: MenuItemDto[] = [];
  isModalOpen = false;
  form!: FormGroup; 
  showFilter = false;
  selected = {} as BookingMenuOptionDto;
  filters = {} as GetBookingMenuOptionListDto;

  constructor(
    public readonly list: ListService,
    private service: BookingMenuOptionService,
    private bookingService: BookingService,
    private menuItemService: MenuItemService,
    private toaster: ToasterService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.bookingService.getList({ maxResultCount: 1000 }).subscribe(res => {
    this.bookingsList = res.items || [];
    });
    this.menuItemService.getList({ maxResultCount: 1000 }).subscribe(res => {
    this.menuItemsList = res.items || [];
    });
    const streamCreator = (query: GetBookingMenuOptionListDto) => this.service.getList({ ...query, ...this.filters });
    this.list.hookToQuery(streamCreator).subscribe((res) => {
      this.items = res;
    });
  }

  buildForm(): void {
    this.form = this.fb.group({
      quantity: [this.selected.quantity ?? null, Validators.required],
      priceAtBookingTime: [this.selected.priceAtBookingTime ?? null, Validators.required],
      bookingId: [this.selected.bookingId ?? null, Validators.required],
      menuItemId: [this.selected.menuItemId ?? null, Validators.required], 
    });
  }

  create(): void {
    this.selected = {} as BookingMenuOptionDto;
    this.form?.reset();
    this.buildForm();
    this.isModalOpen = true;
  }

  edit(id: string): void {
    this.service.get(id).subscribe((res) => {
      this.selected = res;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: string): void {
    this.confirmation.warn('This action cannot be undone.',
    'Delete Record').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.service.delete(id).subscribe(() => {
          this.list.get();
          this.toaster.success('Record deleted successfully.');
        });
      }
    });
  }

  clearFilters(): void {
  this.filters = {} as GetBookingMenuOptionListDto;
  this.list.get();
 }

  save(): void {
    if (this.form.invalid) return;

    const data = this.form.value;

    if (this.selected?.id) {
      this.service.update(this.selected.id, data).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Updated Successfully');
      });
    } else {
      this.service.create(data).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Created Successfully');
      });
    }
  }
}