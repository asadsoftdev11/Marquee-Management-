import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackBookings } from './track-bookings';

describe('TrackBookings', () => {
  let component: TrackBookings;
  let fixture: ComponentFixture<TrackBookings>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrackBookings]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrackBookings);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
