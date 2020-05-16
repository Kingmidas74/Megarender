// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  API: {
    URL: 'http://localhost:5002/api/0.1'
  },
  identityService: {
    URL: 'http://localhost:5000',
    scope: 'megarender_api offline_access',
    clientId: 'personal_local',
    secret: 'secret',
    grantType: {
      recieve: 'custom',
      refresh: 'refresh_token'
    }
  },
  constants: {
    JWTTokenStorageKey: 'JWTToken',
    snackBarDuration:5000
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
