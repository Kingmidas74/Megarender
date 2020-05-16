import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseNavigationModule, FuseSearchBarModule, FuseShortcutsModule } from '@fuse/components';


import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from 'environments/environment';


import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { WorkspaceRoutes } from './workspace-routing';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { StatisticsComponent } from './components/statistics/statistics.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { QuickPanelComponent } from './components/quick-panel/quick-panel.component';
import { ContentComponent } from './components/content/content.component';
import { WorkspaceLayoutComponent } from './workspace-layout.component';
import { CanLoadWorkspace } from './can-load-workspace';
import { MaterialModule } from '@common/material/material.module';




@NgModule({
  declarations: [
    WorkspaceLayoutComponent,    
    NavbarComponent,
    FooterComponent,
    ToolbarComponent,
    QuickPanelComponent,
    ContentComponent,
    DashboardComponent, 
    StatisticsComponent    
  ],
  imports: [
    CommonModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    RouterModule,    
    MaterialModule,
    ReactiveFormsModule,
    RouterModule.forChild(WorkspaceRoutes),
    FuseSharedModule,
    FuseSidebarModule,
    FuseNavigationModule,        
    FuseSearchBarModule,
    FuseShortcutsModule,
  ],
  providers: [CanLoadWorkspace],
  exports: [
    
  ]
})
export class WorkspaceModule { }
