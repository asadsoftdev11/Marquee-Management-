import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuCategories } from './menu-categories';

describe('MenuCategories', () => {
  let component: MenuCategories;
  let fixture: ComponentFixture<MenuCategories>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuCategories]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MenuCategories);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
