import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedUtilsModule } from '@common/shared-utils/shared-utils.module';
import { ApiInterceptor, API_INTERCEPTOR, ApiHttpService } from './api-interceptor';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedUtilsModule
  ],
  providers: [
    ApiInterceptor,
    {provide: API_INTERCEPTOR, useClass: ApiInterceptor, multi: true},
    ApiHttpService
  ]
})
export class ApiModule { }
