import { Environment } from '@abp/ng.core';

//const baseUrl = 'http://localhost:4200';
const baseUrl = window.location.origin;

const oAuthConfig = {
  // issuer: 'https://localhost:44380/',
  issuer: 'https://marqueemanagement.runasp.net',
  redirectUri: baseUrl,
  clientId: 'MarqueeManagement_App',
  responseType: 'code',
  scope: 'offline_access openid profile email phone roles',
  requireHttps: true,
};

export const environment = {
  //production: false,
  production: true,
  application: {
    baseUrl: 'https://marquee-management.netlify.app',
    name: 'MarqueeManagement',
  },
  oAuthConfig,
  apis: {
    default: {
      //url: 'https://localhost:44380',
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
    mergeStrategy: 'deepmerge',
  },
} as Environment;
