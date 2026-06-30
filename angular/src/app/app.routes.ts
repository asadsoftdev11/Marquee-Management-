import { authGuard, permissionGuard } from '@abp/ng.core';
import { Routes } from '@angular/router';

export const APP_ROUTES: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadComponent: () => import('./home/home.component').then(c => c.HomeComponent),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(c => c.createRoutes()),
  },
  {
    path: 'identity',
    canActivate: [authGuard],
    loadChildren: () => import('@abp/ng.identity').then(c => c.createRoutes()),
  },
  {
    path: 'tenant-management',
    canActivate: [authGuard],
    loadChildren: () => import('@abp/ng.tenant-management').then(c => c.createRoutes()),
  },
  {
    path: 'setting-management',
    canActivate: [authGuard],
    loadChildren: () => import('@abp/ng.setting-management').then(c => c.createRoutes()),
  },
{
  path: 'marquees',
  canActivate: [authGuard, permissionGuard],
  data: { requiredPolicy: 'MarqueeManagement.Marquees' },
  loadComponent: () => import('./marquees/marquees').then(c => c.Marquees),
},
{
  path: 'customers',
  canActivate: [authGuard, permissionGuard],
  data: { requiredPolicy: 'MarqueeManagement.Customers' },
  loadComponent: () => import('./customers/customers').then(c => c.Customers),
},
{
  path: 'menu-items',
  canActivate: [authGuard, permissionGuard],
   data: { requiredPolicy: 'MarqueeManagement.MenuItems' },
  loadComponent: () => import('./menu-items/menu-items').then(c => c.MenuItems),
},
{
  path: 'menu-categories',
  canActivate: [authGuard, permissionGuard],
   data: { requiredPolicy: 'MarqueeManagement.MenuCategories' },
  loadComponent: () => import('./menu-categories/menu-categories').then(c => c.MenuCategories),
},
{
  path: 'bookings',
  canActivate: [authGuard, permissionGuard],
  data: { requiredPolicy: 'MarqueeManagement.Bookings' },
  loadComponent: () => import('./bookings/bookings').then(c => c.Bookings),
},
{
  path: 'booking-menu-options',
  canActivate: [authGuard, permissionGuard],
  data: { requiredPolicy: 'MarqueeManagement.BookingMenuOptions' },
  loadComponent: () => import('./booking-menu-options/booking-menu-options').then(c => c.BookingMenuOptions),
},
{
  path: 'about-marquee',
  loadComponent: () => import('./pages/about-marquee/about-marquee') .then(m => m.AboutMarquee),
},
{
  path: 'track-bookings',
  loadComponent: () => import('./track-bookings/track-bookings').then(c => c.TrackBookings),
}

];
