import { Injectable } from '@angular/core';
import { Observable, throwError, interval, Subscription } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HttpParams, HttpErrorResponse } from '@angular/common/http';
import { StorageMap } from '@ngx-pwa/local-storage';
import { environment } from '../../../environments/environment';

import { JWTToken } from '../../api/models/JWTToken';
import { UserRegistrationData } from '../models/userRegistrationData';

import * as uuid from 'uuid';
import { IdentityHttpService } from '../identity-interceptor';
import { ApiHttpService } from '../../api/api-interceptor';


@Injectable({
  providedIn:'root'
})
export class AuthenticationService {

  subscriptions:Array<Subscription> = new Array<Subscription>();
  
  constructor(private identityHttpClient:IdentityHttpService, 
              private storage: StorageMap, 
              private apiHttpClient:ApiHttpService) {
    
  };

  calculateRefreshTime(token:JWTToken):number {
    return token.expires_in-token.expires_in/10
  }

  public IsAuthenticatedUser():Observable<boolean> {
    return this.storage.get<JWTToken>(environment.constants.JWTTokenStorageKey)
    .pipe(      
      map(response => !!response),
      catchError(() => {
        return Observable.throw(false)
      })   
    );
  }

  startTokenRefresh() {
    this.storage.get<JWTToken>(environment.constants.JWTTokenStorageKey).toPromise()
    .then(token=> {
      this.subscriptions.push(
        interval(this.calculateRefreshTime(token as JWTToken))
        .subscribe(()=>(this.refreshToken()))
      )
    })
    .catch(console.error);
  }
  
  public sendTokenRequest(userPhone:string, userPassword:string):Observable<string|JWTToken> {      
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
    const body = new HttpParams()
    .set('grant_type', environment.identityService.user.grantType)
    .set('scope', environment.identityService.user.scope)
    .set('client_id', environment.identityService.user.clientId)
    .set('client_secret', environment.identityService.user.secret)
    .set('phone', userPhone)
    .set('password', userPassword);
  return this.identityHttpClient.post<JWTToken>('/connect/token',body).pipe(
      catchError((error)=>this.handleError(error)),
      tap(response => {
          this.storage.set(environment.constants.JWTTokenStorageKey,response).subscribe();
      }),
      tap(response=> {
          this.subscriptions.push(
            interval(this.calculateRefreshTime(response as JWTToken))
            .subscribe(()=>(this.refreshToken()))
          )
      })
    );    
  }

  public createIdentity(phone:string, email:string, password:string, confirmPassword:string):Observable<UserRegistrationData> {
    const body = new  UserRegistrationData();
    body.ConfirmPassword=confirmPassword;
    body.Password=password;
    body.Email=email;
    body.Phone=phone;
    body.Id=uuid.v4();
    
    return this.identityHttpClient.post('/identity/createIdentity',body).pipe(
      catchError((error)=>this.handleError(error)),
      map(_=>body)
    );
  }

  public confirmIdentity(id:string, code:string):Observable<any> {
    const body = {
      Id:id,
      Code:code
    };    
    return this.identityHttpClient.post('/identity/confirmIdentity',body).pipe(
      catchError((error)=>this.handleError(error))
    );
  }

  createUser(id: string, firstName: string, surname: string, secondName: string, userBirthdate: Date):Observable<any> {
    const body = {
      id:id,
      firstName:firstName,
      surName:surname,
      secondName:secondName,
      birthdate: userBirthdate
    };
    return this.apiHttpClient.post('/users',body)
    .pipe(
      catchError((error)=>this.handleError(error))
    );
  }

  refreshToken() {   
    this.storage.get<JWTToken>(environment.constants.JWTTokenStorageKey).toPromise()
      .then(token=>{
        const body = new HttpParams()
        .set('grant_type', 'refresh_token')
        .set('scope', environment.identityService.user.scope)
        .set('client_id', environment.identityService.user.clientId)
        .set('refresh_token', (token as JWTToken).refresh_token);
        console.log("request new token");
        return this.identityHttpClient.post<JWTToken>('/connect/token',body).pipe(
          catchError((error)=>this.handleError(error))          
        ).toPromise();
      })
      .then(token=>{        
        this.storage.set(environment.constants.JWTTokenStorageKey,token).subscribe();
      })
      .catch(error=>{
        console.error("Token wasn't found",error);
        this.subscriptions.forEach(element => {
          element.unsubscribe();
        });
      });
  };

  handleError(error: HttpErrorResponse):Observable<string> {
    if(error.status>=400 && error.status<500) {
      console.error('An error occurred:', error.error.message, error);
      return throwError(error.message);
    }
    else {
      console.error(error);
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
      return throwError('Unknown error');
    }
  };
}
