import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { IdentityLayoutComponent } from './identity-layout.component';
import { CanLoadIdentity } from './can-load-identity';
import { NgModule } from '@angular/core';


@NgModule({
    imports: [RouterModule.forChild([
        { 
            path: '',        
            component: IdentityLayoutComponent,
            canLoad:[CanLoadIdentity],
            children: [
                { 
                    path: '',      
                    canActivateChild:[CanLoadIdentity],
                    children: [
                        {path:'login', component: LoginComponent},
                        {path:'', component: LoginComponent},
                    ]
                },
            ]
        }
    ])],
    exports: [RouterModule],
  })
  export class IdentityRoutingModule {}