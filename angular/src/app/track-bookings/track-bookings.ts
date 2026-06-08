import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PageModule } from '@abp/ng.components/page';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { TrackBookingService } from '../proxy/track-bookings/track-booking.service';
import { TrackBookingResultDto } from '../proxy/track-bookings/models';

@Component({
  selector: 'app-track-bookings',
  standalone: true,
  imports: [CommonModule, FormsModule, PageModule, ThemeSharedModule],
  templateUrl: './track-bookings.html',
  styleUrls: ['./track-bookings.scss']
})
export class TrackBookings {

  searchName  = '';
  searchPhone = '';

  isLoading = false;  
  searched  = false;   
  notFound  = false;   

  result: TrackBookingResultDto | null = null;

  constructor(
    private trackBookingService: TrackBookingService
  ) {}

  search(): void {
    if (!this.searchName.trim() && !this.searchPhone.trim()) return;

    this.isLoading = true;
    this.searched  = true;
    this.notFound  = false;
    this.result    = null;

    this.trackBookingService
      .getByNameOrPhone(this.searchName.trim(), this.searchPhone.trim())
      .subscribe({
        next: (res) => {
          this.result    = res;    
          this.notFound  = !res;
          this.isLoading = false;
        },
        error: () => {
          this.notFound  = true;
          this.isLoading = false;
        }
      });
  }

  getStatusLabel(status: number): string {
    if (status === 1) return 'Confirmed';
    if (status === 2) return 'Cancelled';
    return 'Pending';
  }

  getStatusClass(status: number): string {
    if (status === 1) return 'badge-confirmed';
    if (status === 2) return 'badge-cancelled';
    return 'badge-pending';
  }

  getInitials(name: string): string {
    if (!name) return '?';
    return name.split(' ').map(w => w[0]).join('').toUpperCase().slice(0, 2);
  }

  clear(): void {
    this.searchName  = '';
    this.searchPhone = '';
    this.searched    = false;
    this.notFound    = false;
    this.result      = null;
  }

  // Total of all bookings combined
getTotalAmount(): number {
  return this.result?.bookings
    .reduce((sum, b) => sum + (b.grandTotal || 0), 0) ?? 0;
}

// Count bookings by status number
getCount(status: number): number {
  return this.result?.bookings
    .filter(b => b.status === status).length ?? 0;
}

print(): void {
  const content = document.querySelector('.print-area')?.innerHTML;
  if (!content) return;

  const printWindow = window.open('', '_blank');
  if (!printWindow) return;

  // Grab all styles from current page and inject them
  const styles = Array.from(document.styleSheets)
    .map(sheet => {
      try {
        return Array.from(sheet.cssRules).map(r => r.cssText).join('\n');
      } catch { return ''; }
    }).join('\n');

  printWindow.document.write(`
    <html>
      <head>
        <title>Track Booking</title>
        <style>${styles}</style>
      </head>
      <body>${content}</body>
    </html>
  `);

  printWindow.document.close();

  setTimeout(() => {
    printWindow.print();
    printWindow.close();
  }, 500);
}

}