import { Injectable } from '@angular/core';
import { Observable, throwError, interval, Subscription, of } from 'rxjs';
import { catchError, map, tap, mergeMap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { StorageMap } from '@ngx-pwa/local-storage';
import { environment } from 'environments/environment';

import { JWTToken } from '@DAL/identity-service/models/JWTToken';
import { IdentityService } from '@DAL/identity-service/services/identity.service';
import { GetTokenQuery } from '@DAL/identity-service/models/queries/get-token-query';

import * as jwt_decode from "jwt-decode";
import { IdentityUser } from '../models/identityUser';

@Injectable({
  providedIn:'root'
})
export class AuthenticationService {

  subscriptions:Array<Subscription> = new Array<Subscription>();
  
  constructor(private identityService:IdentityService, 
              private storage: StorageMap) {
    
  };

  public login(userPhone:string, userPassword:string):Observable<string|JWTToken> {      
    this.unsubscribeAll();
    var query = new GetTokenQuery();
    query.phone=userPhone;
    query.password=userPassword;
    return this.identityService.getToken(query).pipe(
      catchError((error)=>this.handleError(error)),
      tap(response => {
          this.storage.set(environment.constants.JWTTokenStorageKey,response).subscribe();
          this.startTokenRefresh();
      })
    );    
  }

  public logout():Observable<undefined> {      
      this.unsubscribeAll();      
      return this.storage.delete(environment.constants.JWTTokenStorageKey);
  }

  public startTokenRefresh() {
    this.unsubscribeAll();
    this.storage.get<JWTToken>(environment.constants.JWTTokenStorageKey).toPromise()
    .then(token=> {
      this.subscriptions.push(
        interval(this.calculateRefreshTime(token as JWTToken))
        .subscribe(()=>(this.refreshToken()))
      )
    })
    .catch(console.error);
  }

  private unsubscribeAll() {
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
  }

  private calculateRefreshTime(token:JWTToken):number {
    return token.expires_in-token.expires_in/10
  }

  private refreshToken() {   
    this.storage.get<JWTToken>(environment.constants.JWTTokenStorageKey).pipe(
      mergeMap(token => this.identityService.refreshToken(token as JWTToken)),
      mergeMap(token => this.storage.set(environment.constants.JWTTokenStorageKey,token)),
      catchError((error) => () => {
        this.unsubscribeAll();
        this.handleError(error);
      })  
    ).subscribe();
  };

  public IsAuthenticatedUser():Observable<boolean> {
    return this.storage.get<JWTToken>(environment.constants.JWTTokenStorageKey)
    .pipe(      
      map(response => !!response),
      catchError(() => {
        return Observable.throw(false)
      })   
    );
  }

  public getDecodedAccessToken(token: JWTToken): Observable<IdentityUser> {
    return of(jwt_decode(token.access_token)).pipe(
      catchError(error=>()=>this.handleError(error))
    );
  }

  private handleError(error: HttpErrorResponse):Observable<string> {
    if(error.status>=400 && error.status<500) {
      console.error('An error occurred:', error.error.message, error);
      return throwError(error.message);
    }
    else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
      return throwError('Unknown error');
    }
  };
}
