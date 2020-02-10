import { Injectable } from '@angular/core';
import { Observable, of, throwError, concat } from 'rxjs';
import { catchError, map, filter, switchMap } from 'rxjs/operators';

// our services
import { CacheService } from '../caching/cache.service';
import { CommonHttpService } from './common-http.service';
import { SiteSettingService } from '../site-setting';
import { SubscriptionService } from '../subscription';
import { LoggerService } from '../logger';
import { TokenService } from '../token';
import { HttpError } from './http-error.model';
import { HttpResponse } from '@angular/common/http';
import { TypeGuardUtil } from './../../utilities/classes/type-guard-util';

@Injectable()
export class CommonDataService {
  private isApplicationCacheEnabled: boolean;
  private apiServiceBaseUri: string;

  public constructor(private tokenService: TokenService,
    private commonHttpService: CommonHttpService,
    private loggerService: LoggerService,
    private cacheService: CacheService,
    private siteSettings: SiteSettingService,
    private subscriptionService: SubscriptionService
  ) { }


  public get = (url = '', useCache = false, jsonParamObject: any = null, mapToJson = true,
    header: any = null, responseType: any = 'json'): any => {
    this.isApplicationCacheEnabled = this.siteSettings.authSettings().enableApplicationCache;
    this.apiServiceBaseUri = this.siteSettings.authSettings().serviceUri;

    const appCache: any = this.cacheService.get(url);
    // if the caching is enabled application wide and this request uses cache and
    // if the cache is not null then get the data from the cache and return
    if (this.isApplicationCacheEnabled && useCache && appCache != null) {
      return of(appCache);
    } else {
      // if the data is not present in the cache then get the data from the server and also put it in the cache.
      // return this.commonHttpService.get((this.apiServiceBaseUri + url), jsonParamObject, header, mapToJson);
      if (mapToJson) {
        return this.commonHttpService.get((this.apiServiceBaseUri + url), jsonParamObject, header)
          .pipe(
            map(this.extractData),
            catchError(this.handleError)
          );
      } else {
        return this.commonHttpService.get((this.apiServiceBaseUri + url), jsonParamObject, header, responseType)
          .pipe(
            map(this.extractData),
            catchError(this.handleError)
          );
      }
    }
  }


  public post = (data: any, url = '', header: any = null, mapToJson = false, responseType: any = 'json'): any => {
    this.apiServiceBaseUri = this.siteSettings.authSettings().serviceUri;
    if (mapToJson) {
      return this.commonHttpService.post((this.apiServiceBaseUri + url), data, header)
        .pipe(
          map(this.extractData),
          catchError(this.handleError)
        );
    } else {
      return this.commonHttpService.post((this.apiServiceBaseUri + url), data, header, responseType)
        .pipe(
          map(this.extractData),
          catchError(this.handleError)
        );
    }
  }

  public postFormData = (url: string, data: any): any => {
    return this.commonHttpService.postFormData((this.apiServiceBaseUri + url), data)
      .pipe(
        map(this.extractData),
        catchError(this.handleError)
      );
  }

  public put = (data: any, url: string): any => {
    const fullUrl = this.siteSettings.authSettings().serviceUri + url;
    const body = JSON.stringify(data);
    const headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });
    const options = {
      headers: headers
    }

    // returning response.json automatically because put should return the updated object
    return this.commonHttpService.put(fullUrl, body, options)
      .pipe(
        map(this.extractData),
        catchError(this.handleError)
      );
  }

  public extractData = (response: HttpResponse<any>): any => {
    if (response.status < 200 || response.status >= 300) {
      throw new Error('Bad Response status: ' + response.status);
    }
    return response.body;
  }

  public handleError = (error: any): any => {
    // https://angular.io/docs/ts/latest/guide/server-communication.html

    if (error.headers === undefined) {
      // This is throwing from angular code.
      this.loggerService.error(error.message, error.stack, null, true, false);
      this.subscriptionService.announceApplicationHalt(null);
      const httpError: HttpError = {
        message: error.message,
        url: '',
        statusCode: 400,
        statusText: error.message,
        handled: true,
        isCustomError: false
      };
      // return Observable.throw(httpError);
      throwError(httpError);
    } else {
      return this.handleServerError(error);
    }
  }

  public handleServerError = (error: any): any => {
    const applicationError: any = error.headers.get('Application-Error');
    const customError: any = error.headers.get('IsCustomErrorObject');
    let serverError: any; // = error.json(); // error.json() is not working for plain text error message.
    let modelStateErrors = '';
    let isCustomError: boolean = false;

    if (!applicationError && !customError) {
      try {
        serverError = error;
        if (!TypeGuardUtil.isNullOrUndefined(serverError.error)) {
          console.log(serverError.error);
          modelStateErrors = serverError.error;
        }
        modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
      } catch (error) {
        // Empty on Purpose
      }
    }
    if (customError) {
      modelStateErrors = error.error;
      isCustomError = true;
    }

    const errorMessage = applicationError || modelStateErrors || error.message;
    const httpError: HttpError = {
      message: errorMessage,
      url: error.url,
      statusCode: error.status,
      statusText: error.statusText,
      handled: false,
      isCustomError: isCustomError
    };

    if (httpError.message === '') {
      httpError.message = httpError.statusText; // 403}
    }
    if (error.status === 0 && error.ok === false && error.statusText === '') {
      httpError.message = 'Unable to access service'; // 403}
      httpError.handled = true;
      this.loggerService.error(httpError.message, null, httpError.url, true, false);
      this.subscriptionService.announceApplicationHalt(httpError.message);
      throwError(httpError); // return Observable.throw(httpError);
    }


    if (error.status === 401 || applicationError) {
      this.loggerService.error(httpError.message, null, httpError.url, true, false);
      if (error.status === 401) {
        this.subscriptionService.announceApplicationHalt(httpError.message);
      }
      httpError.handled = true;
      throwError(httpError); // return Observable.throw(httpError);
    } else if (error.status === 403) {
      this.loggerService.error(httpError.message, null, httpError.url, true, false);
      httpError.handled = true;
      throwError(httpError); // return Observable.throw(httpError);
    } else if (error.status >= 400 && error.status < 417) { // client errors or validation errors
      throwError(httpError); // return Observable.throw(httpError);
    } else {
      httpError.handled = true;
      if (httpError.statusCode === 0 && httpError.url == null && !(httpError.message instanceof String)) {
        httpError.message = 'Connection problem';
      }
      this.loggerService.error(httpError.message, null, httpError.url, true, false);
      throwError(httpError); // return Observable.throw(httpError);
    }
  }

  // separate method for parsing errors into a single flat array
  public parseErrors = (response: any): any => {
    const errors: any = [];
    // tslint:disable-next-line: forin
    for (const key in response.ModelState) {
      // tslint:disable-next-line: prefer-for-of
      for (let i = 0; i < response.ModelState[key].length; i++) {
        errors.push(response.ModelState[key][i]);
      }
    }
    return errors;
  }
}
