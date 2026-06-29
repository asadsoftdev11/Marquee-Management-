import { authGuard, permissionGuard } from '@abp/ng.core';
import { Routes } from '@angular/router';

export const APP_ROUTES: Routes = [
  {
    path: '',
    pathMatch: 'full',
   // canActivate: [authGuard],
    loadComponent: () => import('./home/home.component').then(c => c.HomeComponent),
  },
  {
    path: 'account',
    //canActivate: [authGuard],
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
    canActivate: [authGuard],
  loadComponent: () => import('./marquees/marquees').then(c => c.Marquees),
},
{
  path: 'customers',
    canActivate: [authGuard],
  loadComponent: () => import('./customers/customers').then(c => c.Customers),
},
{
  path: 'menu-items',
    canActivate: [authGuard],
  loadComponent: () => import('./menu-items/menu-items').then(c => c.MenuItems),
},
{
  path: 'menu-categories',
    canActivate: [authGuard],
  loadComponent: () => import('./menu-categories/menu-categories').then(c => c.MenuCategories),
},
{
  path: 'bookings',
    canActivate: [authGuard],
  loadComponent: () => import('./bookings/bookings').then(c => c.Bookings),
},
{
  path: 'booking-menu-options',
    canActivate: [authGuard],
  loadComponent: () => import('./booking-menu-options/booking-menu-options').then(c => c.BookingMenuOptions),
},
{
  path: 'about-marquee',
    canActivate: [authGuard],
  loadComponent: () => import('./pages/about-marquee/about-marquee') .then(m => m.AboutMarquee),
},
{
  path: 'track-bookings',
    canActivate: [authGuard],
  loadComponent: () => import('./track-bookings/track-bookings').then(c => c.TrackBookings),
}

];
