import { Injectable, InjectionToken, Injector } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpClient, HttpBackend, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageMap } from '@ngx-pwa/local-storage';
import { environment } from 'environments/environment';
import { HandlerService } from '@common/shared-utils/handlers';
import { JWTToken } from '@DAL/identity-service/models/JWTToken';
import { mergeMap, catchError } from 'rxjs/operators';
import { EnvService } from 'app/services/env.service';


export const API_INTERCEPTOR = new InjectionToken<HttpInterceptor[]>('API_INTERCEPTOR');


@Injectable()
export class ApiHttpService extends HttpClient {
  constructor(backend: HttpBackend, private injector: Injector) {
    super(new HandlerService(backend, injector, API_INTERCEPTOR));
  }
}



@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  constructor(private storage: StorageMap,
              private env: EnvService
            ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {    
    return this.storage.get<JWTToken>(environment.constants.JWTTokenStorageKey)
      .pipe(
        mergeMap(token => {
          let httpOptions = {
            headers: new HttpHeaders({
              'Content-Type': 'application/json; charset=utf-8',
              'X-Accept-Version': environment.API.Version,
              'Authorization': `Bearer ${(token as JWTToken).access_token}`
            })
          };
          //return next.handle(req.clone({ url: `${this.env.API_URI}${environment.API.management_prefix}${req.url}`, headers:httpOptions.headers }))
          return next.handle(req.clone({ url: `http://localhost:5002${req.url}`, headers:httpOptions.headers }))
        })
      )
  }
}