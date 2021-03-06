import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseNavigationModule, FuseSearchBarModule, FuseShortcutsModule } from '@fuse/components';


import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from 'environments/environment';


import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '@common/material/material.module';
import { GravatarModule } from 'ngx-gravatar';
import { StatisticsComponent } from './statistics.component';




@NgModule({
  declarations: [
    StatisticsComponent
  ],
  imports: [
    CommonModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    RouterModule,    
    MaterialModule,
    ReactiveFormsModule,
    FuseSharedModule,
    FuseSidebarModule,
    FuseNavigationModule,        
    FuseSearchBarModule,
    FuseShortcutsModule,
    GravatarModule
  ],
  providers: [],
  exports: [
    
  ]
})
export class StatisticsModule { }
