import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { MenuCategoryService, MenuCategoryDto, GetMenuCategoryListDto } from '../proxy/menu-categories';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToasterService } from '@abp/ng.theme.shared';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CardModule, ModalComponent, ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgbDateAdapter, NgbDateNativeAdapter, NgbDatepickerModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PageModule } from '@abp/ng.components/page';

@Component({
  standalone: true,
  selector: 'app-menu-categories',
  templateUrl: './menu-categories.html',
  styleUrls: ['./menu-categories.scss'],
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
  providers: [ListService]
})
export class MenuCategories implements OnInit {
  menuCategories = { items: [], totalCount: 0 } as PagedResultDto<MenuCategoryDto>;
  isModalOpen = false;
  showFilter = false;
  form!: FormGroup;
  selectedMenuCategory = {} as MenuCategoryDto;
  filters = {} as GetMenuCategoryListDto;

  constructor(
    public readonly list: ListService,
    private menuCategoryService: MenuCategoryService,
    private toaster: ToasterService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    const menuCategoryStreamCreator = (query: any) => this.menuCategoryService.getList({ ...query, ...this.filters });
    this.list.hookToQuery(menuCategoryStreamCreator).subscribe((response) => {
      this.menuCategories = response;
    });
  }

  buildForm(): void {
    this.form = this.fb.group({
      name: [this.selectedMenuCategory.name || '', Validators.required],
      description: [this.selectedMenuCategory.description || '']
    });
  }

  createMenuCategory(): void {
    this.selectedMenuCategory = {} as MenuCategoryDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editMenuCategory(id: string): void {
    this.menuCategoryService.get(id).subscribe((menuCategory) => {
      this.selectedMenuCategory = menuCategory;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: string): void {
     this.confirmation.warn( 'This action cannot be undone.',
    'Delete Record').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.menuCategoryService.delete(id).subscribe(() => {
          this.list.get();
          this.toaster.success('Record deleted successfully.');
        });
      }
    });
  }

  clearFilters(): void {
    this.filters = {} as GetMenuCategoryListDto;
    this.list.get();
    this.form?.reset();
  }

  save(): void {
    if (this.form.invalid) return;

    if (this.selectedMenuCategory?.id && !this.form.dirty) {
      this.toaster.info('Nothing changed');
      return;
    }

    const menuCategoryData = this.form.value;

    if (this.selectedMenuCategory?.id) {
      this.menuCategoryService.update(this.selectedMenuCategory.id, menuCategoryData).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Updated Successfully');
      });
    } else {
      this.menuCategoryService.create(menuCategoryData).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Created Successfully');
      });
    }
  }
}