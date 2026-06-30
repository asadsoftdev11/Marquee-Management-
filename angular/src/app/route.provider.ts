import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
    {
      path: '/',
      name: '::Menu:Home',
      iconClass: 'fas fa-home',
      order: 1,
      layout: eLayoutType.application,
    },
    // {
    //   path: '/marquee-management',
    //   name: '::Menu:MarqueeManagement',
    //   iconClass: 'fas fa-building',
    //   order: 2,
    //   layout: eLayoutType.application,
    // },

    {
      path: '/marquees',
      name: '::Menu:Marquees',
      iconClass: 'fas fa-warehouse',
      //  parentName: '::Menu:MarqueeManagement',
      order: 1,
      layout: eLayoutType.application,
      requiredPolicy: 'MarqueeManagement.Marquees',
    },
    {
      path: '/customers',
      name: '::Menu:Customers',
      iconClass: 'fas fa-users',
      // parentName: '::Menu:MarqueeManagement',
      order: 2,
      layout: eLayoutType.application,
      requiredPolicy: 'MarqueeManagement.Customers',
    },
    {
      path: '/menu-categories',
      name: '::Menu:MenuCategories',
      iconClass: 'fas fa-list-alt',
      // parentName: '::Menu:MarqueeManagement',
      order: 3,
      layout: eLayoutType.application,
      requiredPolicy: 'MarqueeManagement.MenuCategories',
    },
    {
      path: '/menu-items',
      name: '::Menu:MenuItems',
      iconClass: 'fas fa-utensils',
      //  parentName: '::Menu:MarqueeManagement',
      order: 4,
      layout: eLayoutType.application,
      requiredPolicy: 'MarqueeManagement.MenuItems',
    },
    {
      path: '/bookings',
      name: '::Menu:Bookings',
      iconClass: 'fas fa-calendar-check',
      //  parentName: '::Menu:MarqueeManagement',
      order: 5,
      layout: eLayoutType.application,
      requiredPolicy: 'MarqueeManagement.Bookings',
    },
    {
      path: '/booking-menu-options',
      name: '::Menu:BookingMenuOptions',
      iconClass: 'fas fa-concierge-bell',
      // parentName: '::Menu:MarqueeManagement',
      order: 6,
      layout: eLayoutType.application,
      requiredPolicy: 'MarqueeManagement.BookingMenuOptions',
    },
    {
      path: '/about-marquee',
      name: 'About Marquee',
      iconClass: 'fas fa-info-circle',
      order: 8,
      layout: eLayoutType.application,
    },
    {
      path: '/track-bookings',
      name: '::Menu:BookingTrack',
      iconClass: 'fas fa-search',
      order: 6.5,
      layout: eLayoutType.application,
    },
  ]);
}
