import { CanLoad, Route, UrlSegment, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router} from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../identity/services/authentication.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class CanLoadWorkspace implements CanLoad, CanActivateChild {
  
  constructor(private authenticationService:AuthenticationService,
              private router: Router) {}

  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean>|Promise<boolean>|boolean {    
    return this.authenticationService.IsAuthenticatedUser();
  }

  canActivateChild(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean|UrlTree>|Promise<boolean|UrlTree>|boolean|UrlTree {
    return this.authenticationService.IsAuthenticatedUser().pipe(
      map(result => {        
        if (result) return true; 
        return this.router.parseUrl('identity');
      })
    );
  }
}