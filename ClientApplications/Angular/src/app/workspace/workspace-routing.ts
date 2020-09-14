import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { StatisticsComponent } from './components/statistics/statistics.component';
import { WorkspaceLayoutComponent } from './workspace-layout.component';
import { CanLoadWorkspace } from './can-load-workspace';
import { NgModule } from '@angular/core';


@NgModule({
    imports: [RouterModule.forChild([
        { 
            path: '',        
            component: WorkspaceLayoutComponent,
            canLoad:[CanLoadWorkspace],
            children: [
                { 
                    path: '',      
                    canActivateChild:[CanLoadWorkspace],
                    children: [
                        {path:'dashboard', component: DashboardComponent},
                        {path:'statistics', component: StatisticsComponent},
                        {path:'', component: DashboardComponent},
                    ]
                },
            ]
        }
    ])],
    exports: [RouterModule],
  })
  export class WorkspaceRoutingModule {}
  