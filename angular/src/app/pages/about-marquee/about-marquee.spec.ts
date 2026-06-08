import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutMarquee } from './about-marquee';

describe('AboutMarquee', () => {
  let component: AboutMarquee;
  let fixture: ComponentFixture<AboutMarquee>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AboutMarquee]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AboutMarquee);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
