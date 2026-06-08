import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Marquees } from './marquees';

describe('Marquees', () => {
  let component: Marquees;
  let fixture: ComponentFixture<Marquees>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Marquees]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Marquees);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
