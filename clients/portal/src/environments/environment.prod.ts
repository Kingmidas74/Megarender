export const environment = {
  production: true,
  identityService: {
    scope: 'phrygiawebapi',
    clientId: 'personal_local',
    secret: 'secret',
    grantType: {
      recieve: 'custom',
      refresh: 'refresh_token'
    }
  }
};
