import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseNavigationModule, FuseSearchBarModule, FuseShortcutsModule } from '@fuse/components';


import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../../environments/environment';

import { MaterialModule } from '../material/material.module';
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
    RouterModule.forChild(WorkspaceRoutes),    
    MaterialModule,
    ReactiveFormsModule,
    RouterModule,

    FuseSharedModule,
    FuseSidebarModule,
    FuseNavigationModule,        
    FuseSearchBarModule,
    FuseShortcutsModule,
  ],
  exports: [
    
  ]
})
export class WorkspaceModule { }
