import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';

import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from 'environments/environment';

import { MaterialModule } from '@common/material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { IdentityRoutes } from './identity-routing';
import { SharedUtilsModule } from '@common/shared-utils/shared-utils.module';
import { ApiModule } from '@DAL/api/api.module';
import { CanLoadIdentity } from './can-load-identity';
import { IdentityServiceModule } from '@DAL/identity-service/identity-service.module';
import { IdentityLayoutComponent } from './identity-layout.component';
import { FuseSharedModule } from '@fuse/shared.module';


@NgModule({
  declarations: [IdentityLayoutComponent, LoginComponent, RegistrationComponent],
  imports: [
    CommonModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    RouterModule.forChild(IdentityRoutes),    
    MaterialModule,
    ReactiveFormsModule,
    SharedUtilsModule,
    ApiModule,
    IdentityServiceModule,
    FuseSharedModule
  ],
  providers: [
    CanLoadIdentity
  ]  
})
export class IdentityModule { }
