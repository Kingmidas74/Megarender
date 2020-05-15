import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';

import { IdentityComponent } from './identity/identity/identity.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { IdentityModule } from './identity/identity.module';
import { WorkspaceModule } from './workspace/workspace.module';

@NgModule({
  declarations: [
    AppComponent,
    IdentityComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),    
    IdentityModule,
    WorkspaceModule
  ],
  providers: [ 
      /*IdentityInterceptor, ApiInterceptor,
      {provide: IDENTITY_INTERCEPTOR, useClass: IdentityInterceptor, multi: true},
      {provide: API_INTERCEPTOR, useClass: ApiInterceptor, multi: true},
      IdentityHttpService, ApiHttpService,   */
   /* {
      provide: HTTP_INTERCEPTORS,
      useClass: AppInterceptor,
      multi: true,
    }*/
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
