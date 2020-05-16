import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedUtilsModule } from '@common/shared-utils/shared-utils.module';
import { IdentityInterceptor, IDENTITY_INTERCEPTOR, IdentityHttpService } from './identity-interceptor';
import { IdentityService } from './services/identity.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedUtilsModule
  ],
  providers: [
    IdentityInterceptor,
    {provide: IDENTITY_INTERCEPTOR, useClass: IdentityInterceptor, multi: true},
    IdentityHttpService,
    IdentityService 
  ]
})
export class IdentityServiceModule { }
