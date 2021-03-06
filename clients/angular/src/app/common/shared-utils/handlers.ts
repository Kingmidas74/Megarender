import { HttpRequest, HttpHandler, HttpInterceptor, HttpBackend, HttpEvent } from '@angular/common/http';
import { InjectionToken, Injector } from '@angular/core';
import { Observable } from 'rxjs';


export class HttpInterceptorHandler implements HttpHandler {
  constructor(private next: HttpHandler, private interceptor: HttpInterceptor) {}

  handle(req: HttpRequest<any>): Observable<HttpEvent<any>> {
    return this.interceptor.intercept(req, this.next);
  }
}

export class HandlerService implements HttpHandler {

  private chain: HttpHandler | null = null;

  constructor(private backend: HttpBackend, private injector: Injector, private interceptors: InjectionToken<HttpInterceptor[]>) { }

  handle(req: HttpRequest<any>): Observable<HttpEvent<any>> {
    if (this.chain === null) {
      const interceptors = this.injector.get(this.interceptors, []);
      this.chain = interceptors.reduceRight(
        (next, interceptor) => new HttpInterceptorHandler(next, interceptor), this.backend);
    }
    return this.chain.handle(req);
  }
}
