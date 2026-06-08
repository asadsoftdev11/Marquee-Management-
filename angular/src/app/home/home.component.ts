import { Component, inject } from '@angular/core';
import { AuthService } from '@abp/ng.core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [CommonModule, RouterModule]
})
export class HomeComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  goToMarquees() {
    this.router.navigate(['/marquees']);
  }

  goToBookings() {
  this.router.navigate(['/bookings']);
}

  goToAbout() {
    this.router.navigate(['/about-marquee']);
  }
}