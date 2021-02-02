import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from 'environments/environment';

import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { IdentityModule } from './identity/identity.module';
import { WorkspaceModule } from './workspace/workspace.module';
import { TranslateModule } from '@ngx-translate/core';
import { fuseConfig } from './fuse-config/index';
import { FuseProgressBarModule } from '@fuse/components/progress-bar/progress-bar.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseModule } from '@fuse/fuse.module';
import { FuseSidebarModule } from '@fuse/components';
import { PageNotFoundComponent } from './error-pages/page-not-found/page-not-found.component';
import { EnvServiceProvider } from './services/env.service.provider';

@NgModule({
  declarations: [
    AppComponent, PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),    
    IdentityModule,
    WorkspaceModule,
    TranslateModule.forRoot(),
    FuseModule.forRoot(fuseConfig),
    FuseProgressBarModule,
    FuseSharedModule,
    FuseSidebarModule,
  ],
  providers: [EnvServiceProvider],
  bootstrap: [AppComponent]
})
export class AppModule { }
