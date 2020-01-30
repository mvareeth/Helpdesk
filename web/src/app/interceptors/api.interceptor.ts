import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest
} from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (!req.url.includes('/api/')) {
      return next.handle(req);
    }
    // console.warn('HttpsInterceptor');
    const baseUrl = environment.baseUrl ;
    // clone request and append baseurl
    const httpsReq = req.clone({
      url: baseUrl + req.url
    });

    return next.handle(httpsReq);
  }
}
