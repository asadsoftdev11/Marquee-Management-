import { Environment } from '@abp/ng.core';

const baseUrl = 'https://marquee-management.netlify.app';

const oAuthConfig = {
 //issuer: 'https://localhost:44380/',
  issuer: 'https://marqueemanagement.runasp.net',
 // redirectUri: baseUrl,
 redirectUri: window.location.origin,
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
     // url: 'https://localhost:44380',
      url: 'https://marqueemanagement.runasp.net',
      rootNamespace: 'MarqueeManagement',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
