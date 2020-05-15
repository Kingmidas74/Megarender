import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';

import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../../environments/environment';

import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { IdentityRoutes } from './identity-routing';
import { IdentityHttpService, IDENTITY_INTERCEPTOR, IdentityInterceptor } from './identity-interceptor';
import { SharedUtilsModule } from '../shared-utils/shared-utils.module';
import { ApiModule } from '../api/api.module';
import { ApiHttpService, API_INTERCEPTOR, ApiInterceptor } from '../api/api-interceptor';


@NgModule({
  declarations: [LoginComponent, RegistrationComponent],
  imports: [
    CommonModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    RouterModule.forChild(IdentityRoutes),    
    MaterialModule,
    ReactiveFormsModule,
    SharedUtilsModule,
    ApiModule
  ],
  providers: [
    IdentityInterceptor, ApiInterceptor,
    {provide: IDENTITY_INTERCEPTOR, useClass: IdentityInterceptor, multi: true},
    {provide: API_INTERCEPTOR, useClass: ApiInterceptor, multi: true},
    IdentityHttpService, ApiHttpService  
  ]  
})
export class IdentityModule { }
