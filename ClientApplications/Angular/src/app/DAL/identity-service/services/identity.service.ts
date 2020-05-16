import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'environments/environment';
import { JWTToken } from '../models/JWTToken';

import { IdentityHttpService } from '../identity-interceptor';
import { GetTokenQuery } from '../models/queries/get-token-query';
import { CreateIdentityCommand } from '../models/commands/create-identity-command';
import { ConfirmIdentityCommand } from '../models/commands/confirm-identity-command';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private identityHttpClient:IdentityHttpService) { }

  public getToken(getTokenQuery:GetTokenQuery):Observable<JWTToken>
  {
    return this.identityHttpClient.post<JWTToken>(
      '/connect/token', 
      new HttpParams()
        .set('grant_type', environment.identityService.grantType.recieve)
        .set('scope', environment.identityService.scope)
        .set('client_id', environment.identityService.clientId)
        .set('client_secret', environment.identityService.secret)
        .set('phone', getTokenQuery.Phone)
        .set('password', getTokenQuery.Password)
      );
  }

  public refreshToken(token:JWTToken):Observable<JWTToken>
  {
    return this.identityHttpClient.post<JWTToken>(
      '/connect/token',
      new HttpParams()
      .set('grant_type', environment.identityService.grantType.refresh)
      .set('scope', environment.identityService.scope)
      .set('client_id', environment.identityService.clientId)
      .set('refresh_token', token.refresh_token)
    );
  }

  public createIdentity(createIdentityCommand:CreateIdentityCommand):Observable<string>
  {
    return this.identityHttpClient.post<string>('/identity/createIdentity',createIdentityCommand);
  }

  public confirmIdentity(confirmIdentityCommand:ConfirmIdentityCommand):Observable<string>
  {
    return this.identityHttpClient.post<string>('/identity/confirmIdentity',confirmIdentityCommand);
  }
}
