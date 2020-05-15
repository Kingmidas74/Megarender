import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { StatisticsComponent } from './components/statistics/statistics.component';



export const WorkspaceRoutes: Routes = [
    { 
        path: '',
        redirectTo:'workspace/dashboard',
        pathMatch: 'prefix'
    },
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'statistics',     component: StatisticsComponent}
];