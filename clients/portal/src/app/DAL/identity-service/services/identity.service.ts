import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'environments/environment';
import { JWTToken } from '../models/JWTToken';

import { IdentityHttpService } from '../identity-interceptor';
import { GetTokenQuery } from '../models/queries/get-token-query';
import { VerifyCodeCommand } from '../models/commands/confirm-identity-command';
import { SendCodeCommand } from '../models/commands/send-code-command';

@Injectable()
export class IdentityService {

  constructor(private identityHttpClient:IdentityHttpService) { }

  public getToken(getTokenQuery:GetTokenQuery):Observable<JWTToken>
  {
    return this.identityHttpClient.post<JWTToken>(
      '/connect/token', 
      new HttpParams()
        .set('grant_type', environment.identityService.grantType.recieve)
        .set('scopes', environment.identityService.scope)
        .set('client_id', environment.identityService.clientId)
        .set('client_secret', environment.identityService.secret)
        .set('phone', getTokenQuery.phone)
        .set('password', getTokenQuery.password)
      );
  }

  public refreshToken(token:JWTToken):Observable<JWTToken>
  {
    return this.identityHttpClient.post<JWTToken>(
      '/connect/token',
      new HttpParams()
      .set('grant_type', environment.identityService.grantType.refresh)
      .set('scopes', environment.identityService.scope)
      .set('client_id', environment.identityService.clientId)
      .set('refresh_token', token.refresh_token)
    );
  }
  
  public verifyCode(verifyCodeCommand:VerifyCodeCommand):Observable<string>
  {
    return this.identityHttpClient.post<string>('/identity/verifyCode',verifyCodeCommand);
  }

  public sendCode(sendCodeCommand: SendCodeCommand) {
    return this.identityHttpClient.post<string>('/identity/sendCode', sendCodeCommand);
  }
}
