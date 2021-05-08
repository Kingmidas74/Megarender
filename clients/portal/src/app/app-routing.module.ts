import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { PageNotFoundComponent } from './error-pages/page-not-found/page-not-found.component';


@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(
      [
        {
          path: "workspace",
          loadChildren: () =>
            import("./workspace/workspace-routing").then( mod => mod.WorkspaceRoutingModule)
        },
        {
          path: "identity",
          loadChildren: () =>
            import("./identity/identity-routing").then( mod => mod.IdentityRoutingModule)
        },
        {
          path: '',
          redirectTo: 'workspace',
          pathMatch:'full'
        },
        { path: '**', component: PageNotFoundComponent }
      ]
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
