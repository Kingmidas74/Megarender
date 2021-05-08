import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseNavigationModule, FuseSearchBarModule, FuseShortcutsModule } from '@fuse/components';


import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from 'environments/environment';


import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { WorkspaceRoutingModule } from './workspace-routing';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { QuickPanelComponent } from './components/quick-panel/quick-panel.component';
import { ContentComponent } from './components/content/content.component';
import { WorkspaceLayoutComponent } from './workspace-layout.component';
import { CanLoadWorkspace } from './can-load-workspace';
import { MaterialModule } from '@common/material/material.module';
import { GravatarModule } from 'ngx-gravatar';
import { DashboardModule } from './dashboard/dashboard.module';
import { StatisticsModule } from './statistics/statistics.module';




@NgModule({
  declarations: [
    WorkspaceLayoutComponent,    
    NavbarComponent,
    FooterComponent,
    ToolbarComponent,
    QuickPanelComponent,
    ContentComponent 
  ],
  imports: [
    CommonModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    RouterModule,    
    MaterialModule,
    ReactiveFormsModule,
    WorkspaceRoutingModule,
    FuseSharedModule,
    FuseSidebarModule,
    FuseNavigationModule,        
    FuseSearchBarModule,
    FuseShortcutsModule,
    GravatarModule,

    DashboardModule,
    StatisticsModule
  ],
  providers: [CanLoadWorkspace],
  exports: [
    
  ]
})
export class WorkspaceModule { }
