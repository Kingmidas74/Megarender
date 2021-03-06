import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedUtilsModule } from '@common/shared-utils/shared-utils.module';
import { ApiInterceptor, API_INTERCEPTOR, ApiHttpService } from './api-interceptor';
import { UserService } from './services/user.service';
import { EnvServiceProvider } from 'app/services/env.service.provider';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedUtilsModule
  ],
  providers: [
    ApiInterceptor,
    {provide: API_INTERCEPTOR, useClass: ApiInterceptor, multi: true},
    ApiHttpService,
    UserService,
    EnvServiceProvider
  ]
})
export class ApiModule { }
