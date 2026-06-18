import { Environment } from '@abp/ng.core';

const baseUrl = window.location.origin;

const oAuthConfig = {
  issuer: 'https://marqueemanagement.runasp.net',
  redirectUri: baseUrl,
  clientId: 'MarqueeManagement_App',
  responseType: 'code',
  scope: 'offline_access openid profile email phone roles',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl: 'https://marquee-management.netlify.app',
    name: 'MarqueeManagement',
  },
  oAuthConfig,
  apis: {
    default: {

      url: 'https://marqueemanagement.runasp.net',
      rootNamespace: 'MarqueeManagement',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
