import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingMenuOptions } from './booking-menu-options';

describe('BookingMenuOptions', () => {
  let component: BookingMenuOptions;
  let fixture: ComponentFixture<BookingMenuOptions>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookingMenuOptions]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookingMenuOptions);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
