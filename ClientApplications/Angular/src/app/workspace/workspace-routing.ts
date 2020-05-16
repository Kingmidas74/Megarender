import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { StatisticsComponent } from './components/statistics/statistics.component';
import { WorkspaceLayoutComponent } from './workspace-layout.component';
import { CanLoadWorkspace } from './can-load-workspace';



export const WorkspaceRoutes: Routes = [
    { 
        path: 'workspace',        
        component: WorkspaceLayoutComponent,
        canLoad:[CanLoadWorkspace],
        canActivateChild:[CanLoadWorkspace],
        children: [
            { 
                path: '',      
                redirectTo: 'dashboard',
                pathMatch:'full'
            },
            { 
                path: 'dashboard',      
                component: DashboardComponent 
            },
            { 
                path: 'statistics',     
                component: StatisticsComponent
            }
        ]
    }
];
  