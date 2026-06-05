import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { MarqueeService, MarqueeDto, GetMarqueeListDto } from '../proxy/marquees';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbDateAdapter, NgbDateNativeAdapter, NgbDatepickerModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { CardModule, ConfirmationService, Confirmation, ModalComponent, ToasterService, ThemeSharedModule } from '@abp/ng.theme.shared';
import { PageModule } from '@abp/ng.components/page';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
 
@Component({
  standalone: true,
  selector: 'app-marquees',
  templateUrl: './marquees.html',
  styleUrls: ['./marquees.scss'],
   host: { class: 'app-dark-page' },
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule, 
    ModalComponent,
    PageModule,
    NgbDropdownModule,
    NgbDatepickerModule,
    ThemeSharedModule,
    NgxDatatableModule
  ],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class Marquees implements OnInit {
  marquees = { items: [], totalCount: 0 } as PagedResultDto<MarqueeDto>;
  isModalOpen = false;
  showFilter = false;
  form!: FormGroup;
  selectedMarquee = {} as MarqueeDto;
  filters = {} as GetMarqueeListDto;

  constructor(
    public readonly list: ListService,
    private marqueeService: MarqueeService,
    private toaster: ToasterService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    const marqueeStreamCreator = (query: any) => this.marqueeService.getList({ ...query, ...this.filters });
    this.list.hookToQuery(marqueeStreamCreator).subscribe((response) => {
      this.marquees = response;
    });
  }

  // BUILD FORM
  buildForm(): void {
    this.form = this.fb.group({
      name: [this.selectedMarquee.name || '', Validators.required],
      location: [this.selectedMarquee.location || '', Validators.required],
      description: [this.selectedMarquee.description || ''],
      capacity: [this.selectedMarquee.capacity || null, Validators.required],
      pricePerDay: [this.selectedMarquee.pricePerDay || null, Validators.required],
    });
  }

  // CREATE
  createMarquee(): void {
    this.selectedMarquee = {} as MarqueeDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  // EDIT
  editMarquee(id: string): void {
    this.marqueeService.get(id).subscribe((marquee) => {
      this.selectedMarquee = marquee;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  // DELETE
  delete(id: string): void {
      this.confirmation.warn( 'This action cannot be undone.',
     'Delete Record').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.marqueeService.delete(id).subscribe(() => {
          this.list.get();
          this.toaster.success('Record deleted successfully.');
        });
      }
    });
  }

  // CLEAR FILTERS
  clearFilters(): void {
    this.filters = {} as GetMarqueeListDto;
    this.list.get();
    this.form?.reset();
  }

  // SAVE
  save(): void {
    if (this.form.invalid) return;

    if (this.selectedMarquee?.id && !this.form.dirty) {
      this.toaster.info('Nothing changed');
      return;
    }

    const marqueeData = this.form.value;

    if (this.selectedMarquee?.id) {
      this.marqueeService.update(this.selectedMarquee.id, marqueeData).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Updated Successfully');
      });
    } else {
      this.marqueeService.create(marqueeData).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Created Successfully');
      });
    }
  }
}