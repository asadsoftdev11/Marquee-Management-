import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44380/',
  redirectUri: baseUrl,
  clientId: 'MarqueeManagement_App',
  responseType: 'code',
  scope: 'offline_access MarqueeManagement',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'MarqueeManagement',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44380',
      rootNamespace: 'MarqueeManagement',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
