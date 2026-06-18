import { Environment } from '@abp/ng.core';

const baseUrl = 'https://marquee-management.netlify.app';
const apiUrl = 'https://marqueemanagement.runasp.net';

const oAuthConfig = {
  issuer: apiUrl,
  redirectUri: baseUrl,
  clientId: 'MarqueeManagement_App',
  responseType: 'code',
  scope: 'offline_access openid profile email phone roles MarqueeManagement',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'MarqueeManagement',
  },

  oAuthConfig,

  apis: {
    default: {
     url: apiUrl,
      rootNamespace: 'MarqueeManagement',
    },
    
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },

} as Environment;
