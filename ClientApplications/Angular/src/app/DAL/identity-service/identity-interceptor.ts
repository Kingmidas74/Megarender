import { Injectable, InjectionToken, Injector } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpClient, HttpBackend } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { HandlerService } from '@common/shared-utils/handlers';

export const IDENTITY_INTERCEPTOR = new InjectionToken<HttpInterceptor[]>('IDENTITY_INTERCEPTOR');


@Injectable()
export class IdentityHttpService extends HttpClient {
  constructor(backend: HttpBackend, private injector: Injector) {
    super(new HandlerService(backend, injector, IDENTITY_INTERCEPTOR));
  }
}

@Injectable()
export class IdentityInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req.clone({ url: `${environment.identityService.URL}${req.url}` }));
  }
}