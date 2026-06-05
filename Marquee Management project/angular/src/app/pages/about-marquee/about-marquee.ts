import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
    selector: 'app-about-marquee',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './about-marquee.html',
  styleUrls: ['./about-marquee.scss']
})
export class AboutMarquee {

}
