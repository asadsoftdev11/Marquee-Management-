import { CardModule, Confirmation, ConfirmationService, ModalComponent, ThemeSharedModule, ToasterService } from "@abp/ng.theme.shared";
import { BookingDto, GetBookingListDto } from "../proxy/bookings";
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { BookingService } from "../proxy/controllers";
import { ListService, PagedResultDto } from "@abp/ng.core";
import { Component, OnInit } from "@angular/core";
import { PageModule } from "@abp/ng.components/page";
import { CommonModule } from "@angular/common";
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { MarqueeService, MarqueeDto } from '../proxy/marquees';
import { CustomerService, CustomerDto } from '../proxy/customers';

@Component({
  standalone: true,
  selector: 'app-bookings',
  templateUrl: './bookings.html',
  styleUrls: ['./bookings.scss'],
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
  providers: [ListService],
})

export class Bookings implements OnInit {

  bookings = { items: [], totalCount: 0 } as PagedResultDto<BookingDto>;
  marquees: MarqueeDto[] = [];
  customers: CustomerDto[] = [];
  isModalOpen = false;
  showFilter = false;
  form!: FormGroup;
  selectedBooking = {} as BookingDto;
  filters = {} as GetBookingListDto;

  constructor(
    public readonly list: ListService,
    private bookingService: BookingService,
    private marqueeService: MarqueeService, 
    private customerService: CustomerService,
    private toaster: ToasterService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {

      this.marqueeService.getList({ maxResultCount: 1000 }).subscribe(res => {
      this.marquees = res.items || [];
      });
      this.customerService.getList({ maxResultCount: 1000 }).subscribe(res => {
      this.customers = res.items || [];
      });
    // const stream = (query: GetBookingListDto) =>
    //   this.bookingService.getList({ ...query, ...this.filters });
  
   const stream = (query: any) =>  
     this.bookingService.getList({ ...query, ...this.filters });

     this.list.hookToQuery(stream).subscribe((res) => {
      this.bookings = res;
    });
  }

  buildForm(): void {
    this.form = this.fb.group({
      eventDate: [this.selectedBooking.eventDate || null, Validators.required],
      eventType: [this.selectedBooking.eventType || '', Validators.required],
      guestCount: [this.selectedBooking.guestCount || null, Validators.required],
      totalAmount: [this.selectedBooking.totalAmount || null, Validators.required],
      status: [this.selectedBooking.status ?? 0, Validators.required],
      marqueeId: [this.selectedBooking.marqueeId || null, Validators.required],  
    customerId: [this.selectedBooking.customerId || null, Validators.required], 
    });
  }

  createBooking(): void {
    this.selectedBooking = {} as BookingDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editBooking(id: string): void {
    this.bookingService.get(id).subscribe((res) => {
      this.selectedBooking = res;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: string): void {
    this.confirmation.warn( 'This action cannot be undone.',
    'Delete Record')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
            this.bookingService.delete(id).subscribe(() => {
            this.list.get();
            this.toaster.success('Record deleted successfully.');
          });
        }
      });
  }

  clearFilters(): void {
    this.filters = {} as GetBookingListDto;
    this.list.get();
    this.form?.reset();
  }

  save(): void {
    if (this.form.invalid) return;

    if (this.selectedBooking?.id && !this.form.dirty) {
      this.toaster.info('Nothing changed');
      return;
    }

    const data = this.form.value;

    if (this.selectedBooking?.id) {
      this.bookingService.update(this.selectedBooking.id, data)
        .subscribe(() => this.afterSave('Updated'));
    } else {
      this.bookingService.create(data)
        .subscribe(() => this.afterSave('Created'));
    }
  }

  private afterSave(msg: string) {
    this.isModalOpen = false;
    this.form.reset();
    this.selectedBooking = {} as BookingDto;
    this.list.get();
    this.toaster.success(msg);
  }
}