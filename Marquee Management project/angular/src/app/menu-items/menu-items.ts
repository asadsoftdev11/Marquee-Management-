import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { MenuItemService, MenuItemDto, GetMenuItemListDto} from '../proxy/menu-items';
import { MenuCategoryService, MenuCategoryDto} from '../proxy/menu-categories';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToasterService, ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CardModule, ModalComponent, ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PageModule } from '@abp/ng.components/page';

@Component({
  standalone: true,
  selector: 'app-menu-items',
  templateUrl: './menu-items.html',
  styleUrls: ['./menu-items.scss'],
   host: { class: 'app-dark-page' },
  imports: [ CommonModule, FormsModule, ReactiveFormsModule, CardModule, ModalComponent, PageModule, NgbDropdownModule, ThemeSharedModule,
    NgxDatatableModule ],
  providers: [ListService]
})

export class MenuItems implements OnInit {

  menuItems = { items: [], totalCount: 0 } as PagedResultDto<MenuItemDto>;
  menuCategories: MenuCategoryDto[] = [];
  //  getCategoryName(id: string): string {
  //   const category = this.menuCategories.find(c => c.id === id);
  //   return category ? category.name : '';
  // }

  onImgError(event: Event): void {
  (event.target as HTMLImageElement).src =
    'https://placehold.co/44x44/f5f0e8/c9963a?text=🍽';
}


  isModalOpen = false;
  showFilter = false;
  form!: FormGroup;
  selectedMenuItem = {} as MenuItemDto;
  filters = {} as GetMenuItemListDto;

  constructor(
    public readonly list: ListService,
    private menuItemService: MenuItemService,
    private menuCategoryService: MenuCategoryService,
    private toaster: ToasterService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    //Load Categories (RELATION)
    this.menuCategoryService.getList({ maxResultCount: 1000 }).subscribe(res => { this.menuCategories = res.items || []; });
    
    const streamCreator = (query: any) => this.menuItemService.getList({ ...query, ...this.filters });

    this.list.hookToQuery(streamCreator).subscribe(response => { this.menuItems = response; });
  }

  // Form
buildForm(): void {
  this.form = this.fb.group({
    name: [this.selectedMenuItem.name || '', Validators.required],
    description: [this.selectedMenuItem.description || ''],
    price: [this.selectedMenuItem.price || 0, Validators.required],
    isAvailable: [this.selectedMenuItem.isAvailable ?? true],
    menuCategoryId: [this.selectedMenuItem.menuCategoryId || null, Validators.required], //Relation
    imageUrl: [this.selectedMenuItem.imageUrl || ''] 
  });
}

  //Create
  createMenuItem(): void {
    this.selectedMenuItem = {} as MenuItemDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  // ✏️ Edit
  editMenuItem(id: string): void {
    this.menuItemService.get(id).subscribe(res => {
      this.selectedMenuItem = res;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  //Delete
  delete(id: string): void {
     this.confirmation.warn( 'This action cannot be undone.',
     'Delete Record')
      .subscribe(status => {
        if (status === Confirmation.Status.confirm) {
          this.menuItemService.delete(id).subscribe(() => {
            this.list.get();
            this.toaster.success('Record deleted successfully.');
          });
        }
      });
  }

  // Filters
  clearFilters(): void {
    this.filters = {} as GetMenuItemListDto;
    this.list.get();
    this.form?.reset();
  }

  // Save
  save(): void {
    if (this.form.invalid) return;

    if (this.selectedMenuItem?.id && !this.form.dirty) {
      this.toaster.info('Nothing changed');
      return;
    }

    const data = this.form.value;

    if (this.selectedMenuItem?.id) {
      this.menuItemService.update(this.selectedMenuItem.id, data).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Updated Successfully');
      });
    } else {
      this.menuItemService.create(data).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
        this.toaster.success('Created Successfully');
      });
    }
  }
}