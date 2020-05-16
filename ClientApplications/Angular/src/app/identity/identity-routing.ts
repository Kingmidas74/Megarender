import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { IdentityComponent } from './identity/identity.component';
import { CanLoadIdentity } from './can-load-identity';



export const IdentityRoutes: Routes = [
    { 
        path: 'identity',
        component: IdentityComponent,
        canLoad: [CanLoadIdentity],
        canActivateChild: [CanLoadIdentity],
        children: [
            { 
                path: '',      
                redirectTo: 'login',
                pathMatch:'full'
            },
            { 
                path: 'login',      
                component: LoginComponent 
            },
            { 
                path: 'registration',     
                component: RegistrationComponent
            }
        ]
    }
];