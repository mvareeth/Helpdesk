import { HttpClient, HttpHeaders, HttpRequest, HttpResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
// import { Http, Response, Request, Headers, RequestOptions, RequestOptionsArgs, RequestMethod, URLSearchParams } from '@angular/http';
// Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs';

import { TokenConfig, TokenService } from '../token';
import { TypeGuardUtil } from './../../utilities/classes/type-guard-util';

@Injectable()
export class CommonHttpService {
  private config: TokenConfig;
  public tokenStream: Observable<string>;
  private tokenExcludedURLs: string[] = []; // = 'api/jwt/windowstoken'; //Todo: come up with a object model if required
  private excludeTokenValidation: boolean = false;
  private windowsCredentialURLs: string[] = [];
  private windowsCredentialValidation: boolean = false;

  public constructor(private tokenService: TokenService,
    private http: HttpClient) {
    this.tokenExcludedURLs.push('api/jwt/windowstoken');
    this.tokenExcludedURLs.push('api/jwt/token');
    this.windowsCredentialURLs.push('api/jwt/windowstoken');

    this.config = tokenService.getToken();

    this.tokenStream = new Observable<string>((obs: any) => {
      obs.next(this.config.tokenGetter());
    });
  }

  public setGlobalHeaders = (headers: Array<Object>, request: Request | any) => {
    headers.forEach((header: Object) => {
      let key: string = Object.keys(header)[0];
      let headerValue: string = (<any>header)[key];
      request.headers.set(key, headerValue);
    });
  }

  public getRequestHeaders(): any { }

  public request = (method: any, url: any, options?: any, responseType: any = 'json'): Observable<any> => {

    let request: Observable<any>;
    let globalHeaders = this.config.globalHeaders;


    if (!this.tokenService.tokenNotExpired(null, this.config.tokenGetter())) { // token is expired.
      if (!this.excludeTokenValidation && !this.config.noJwtError) {
        return new Observable<any>((obs: any) => {
          throw new Error('Session Timeout');
        });
      } else {
        let httpOptions: any;
        if (url !== undefined || url !== null) {
          httpOptions = {
            body: url.body,
            headers: url.headers,
            observe: 'response',
            params: url.params,
            reportProgress: false,
            responseType: responseType,
            withCredentials: url.withCredentials,

          };

        }
        request = this.http.request(method, url.url, httpOptions);
      }

    } else if (typeof url === 'string') {
      let reqOpts: any = options || {};

      if (globalHeaders) {
        if (reqOpts.headers === null || reqOpts.headers === undefined) {
          reqOpts.headers = new HttpHeaders();
        }
        reqOpts.headers = reqOpts.headers
          .set('Content-Type', 'application/json')
          .set('Accept', 'application/json')
          .set('Access-Control-Allow-Origin', '*')
          .set('Cache-control', 'no-cache, no-store')
          .set('Pragma', 'no-cache')
          .set(this.config.headerName, this.config.headerPrefix + this.config.tokenGetter());
      } else {
        if (reqOpts.headers === null || reqOpts.headers === undefined) {
          reqOpts.headers = new HttpHeaders();
        }
        reqOpts.headers = reqOpts.headers
          .set('Content-Type', 'application/json')
          .set('Accept', 'application/json')
          .set('Access-Control-Allow-Origin', '*')
          .set(this.config.headerName, this.config.headerPrefix + this.config.tokenGetter());
      }

      let httpOptions: any;
      httpOptions = {
        body: null,
        headers: reqOpts.headers,
        observe: 'response',
        params: null,
        reportProgress: false,
        responseType: responseType,
        withCredentials: null,

      };
      request = this.http.request(method, url, httpOptions);

    } else {
      let req: any = <HttpRequest<any>>url;

      if (globalHeaders) {
        if (req.headers === null || req.headers === undefined) {
          req.headers = new HttpHeaders();
        }
        if (req.body instanceof FormData) {
          req.headers = req.headers
            .set('encrtype', 'multipart/form-data')
            .set('Accept', 'application/json')
            .set('Access-Control-Allow-Origin', '*')
            .set('Cache-control', 'no-cache, no-store')
            .set('Pragma', 'no-cache')
            .set(this.config.headerName, this.config.headerPrefix + this.config.tokenGetter());
        } else {
          req.headers = req.headers
            .set('Content-Type', 'application/json')
            .set('Accept', 'application/json')
            .set('Access-Control-Allow-Origin', '*')
            .set('Cache-control', 'no-cache, no-store')
            .set('Pragma', 'no-cache')
            .set(this.config.headerName, this.config.headerPrefix + this.config.tokenGetter());
        }
      } else {
        if (req.headers === null || req.headers === undefined) {
          req.headers = new HttpHeaders();
        }
        req.headers = req.headers
          .set('Content-Type', 'application/json')
          .set('Accept', 'application/json')
          .set('Access-Control-Allow-Origin', '*')
          .set(this.config.headerName, this.config.headerPrefix + this.config.tokenGetter());
      }

      let httpOptions: any;
      let body: any;
      let params: any;
      body = req.body;

      // for post methos we are getting additional bofy tag so we should check that
      if (!TypeGuardUtil.isNullOrUndefined(req.body)) {
        params = req.body.params;
      } else {
        params = req.params;
      }
      httpOptions = {
        body: body,
        headers: req.headers,
        observe: 'response',
        params: params,
        reportProgress: false,
        responseType: responseType,
        withCredentials: req.withCredentials,

      };
      request = this.http.request(method, req.url, httpOptions);
    }
    this.excludeTokenValidation = false;
    this.windowsCredentialValidation = false;
    return request;
  }


  private requestHelper = (requestArgs: any, additionalOptions: any, method, responseType: any = 'json'): Observable<HttpResponse<any>> => {
    let options = requestArgs;
    let httpOptions: any;
    let body: any;
    let reqOpts: any;

    // if requestArgs has body option appended
    if (!TypeGuardUtil.isNullOrUndefined(requestArgs.body)) {
      body = requestArgs.body;
    } else {
      body = null;
    }
    if (additionalOptions) {
      httpOptions = {
        body: body,
        headers: additionalOptions.headers,
        observe: 'response',
        params: additionalOptions.params,
        reportProgress: false,
        responseType: null,
        withCredentials: additionalOptions.withCredentials,
      };
      //options = options.merge(additionalOptions);
    }
    if (method === 'GET') {
      reqOpts = new HttpRequest<any>(method, requestArgs.url, httpOptions);
    } else {
      reqOpts = new HttpRequest<any>(method, requestArgs.url, httpOptions.body, httpOptions);
    }

    return this.request(method, reqOpts, options, responseType);
  }

  private isTokenValidationExcluded = (url: string): boolean => {
    let excludeToken: boolean = false;
    for (const key in this.tokenExcludedURLs) {
      if (!excludeToken && this.tokenExcludedURLs[key]) {
        excludeToken = url.includes(this.tokenExcludedURLs[key]);
      }
    }
    return excludeToken;
  }

  private isWindowsCredentialUrls = (url: string): boolean => {
    let excludeToken: boolean = false;
    for (const key in this.windowsCredentialURLs) {
      if (!excludeToken && this.windowsCredentialURLs[key]) {
        excludeToken = url.includes(this.windowsCredentialURLs[key]);
      }
    }
    return excludeToken;
  }

  public get = (url: string, jsonParamObject: any = null, header: any, responseType: any = 'json'): any => {
    if (jsonParamObject != null) {
      jsonParamObject = this.updateParamsObject(jsonParamObject);
    }

    if (header == null) {
      header = new HttpHeaders();
      header = header.set('Content-Type', 'application/json').set('Accept', 'application/json').set('Access-Control-Allow-Origin', '*');
    }
    this.excludeTokenValidation = this.isTokenValidationExcluded(url);
    let options: any;
    if (jsonParamObject != null) {
      options = {
        headers: header, params: Object.getOwnPropertyNames(jsonParamObject)
          .reduce((p, key) => p.set(key, jsonParamObject[key]), new HttpParams())
      };
    } else {
      options = {
        headers: header, params: null
      };
    }

    return this.requestHelper({ url: url },
      options, 'GET', responseType);
  }

  public updateParamsObject(jsonParamObject: any): any {
    let updatedObject: any = {};
    for (let key in jsonParamObject) {
      if (!TypeGuardUtil.isNullOrUndefined(jsonParamObject[key])) {
        updatedObject[key] = jsonParamObject[key];
      }
    }
    return updatedObject;
  }

  public post = (url: string, data?: any, header: any = null, responseType: any = 'json'): any => {

    if (data != null && header == null) { //if header is specified then data format should be taken care.
      data = JSON.stringify(data);
    }
    if (header == null) {
      header = new HttpHeaders();
      header = header.set('Content-Type', 'application/json').set('Accept', 'application/json').set('Access-Control-Allow-Origin', '*');
    }

    this.excludeTokenValidation = this.isTokenValidationExcluded(url);
    this.windowsCredentialValidation = false; // this.isWindowsCredentialUrls(url);
    let options: any;
    if (this.windowsCredentialValidation) {
      options = {
        headers: header, withCredentials: true
      };
    } else {
      options = {
        headers: header
      };
    }

    return this.requestHelper({ url: url, body: data },
      options, 'POST', responseType);
  }

  public postFormData = (url: string, data: any): any => {
    let header: HttpHeaders = new HttpHeaders();
    this.excludeTokenValidation = this.isTokenValidationExcluded(url);
    this.windowsCredentialValidation = this.isWindowsCredentialUrls(url);
    let options: any;
    if (this.windowsCredentialValidation) {
      options = {
        headers: header, withCredentials: true
      };
    } else {
      options = {
        headers: header
      };
    }
    return this.requestHelper({ url: url, body: data }, options, 'POST', 'json');
  }

  public put(url: string, body: string, options?: any): Observable<HttpResponse<any>> {
    return this.requestHelper({ url: url, body: body }, options, 'PUT');
  }

  public delete(url: string, header: any = null): Observable<HttpResponse<any>> {
    if (header == null) {
      header = new Headers({ 'Content-Type': 'application/json' });
    }
    let options = { headers: header };
    return this.requestHelper({ url: url },
      options, 'DELETE');
  }

  public patch(url: string, body: string, options?: any): Observable<HttpResponse<any>> {
    return this.requestHelper({ url: url, body: body }, options, 'PATCH');
  }

  public head(url: string, options?: any): Observable<HttpResponse<any>> {
    return this.requestHelper({ url: url }, options, 'HEAD');
  }

}

// http://stackoverflow.com/questions/1959947/whats-an-appropriate-http-status-code-to-return-by-a-rest-api-service-for-a-val
// http://jsonapi.org/examples/#error-objects-error-codes
// https://github.com/FriendsOfCake/crud/issues/337
// https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.Core/OkObjectResult.cs
