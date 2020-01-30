// import { Injectable } from '@angular/core';
// //https://github.com/auth0/angular2-jwt
// /**
//  * Helper class to decode and find JWT expiration.
//  */

import { Injectable } from '@angular/core';

import { TokenConfig } from './';

/**
 * Sets up the authentication configuration.
 */

// Avoid TS error 'cannot find name escape'
declare var escape: any;

@Injectable()
export class TokenService {

  public headerName: string;
  public headerPrefix: string;
  public tokenName: string;
  public tokenGetter: any;
  public noJwtError: boolean;
  public noTokenScheme: boolean;
  public globalHeaders: Array<Object>;

  public constructor() {
    const config: any = {};
    config.tokenName = 'jwt';

    this.headerName = config.headerName || 'Authorization';
    if (config.headerPrefix) {
      this.headerPrefix = config.headerPrefix + ' ';
    } else if (config.noTokenScheme) {
      this.headerPrefix = '';
    } else {
      this.headerPrefix = 'Bearer ';
    }
    this.tokenName = config.tokenName || 'jwt';
    this.noJwtError = config.noJwtError || false;
    this.tokenGetter = config.tokenGetter || (() => sessionStorage.getItem(this.tokenName));
    this.globalHeaders = config.globalHeaders || [{ 'Cache-control': 'no-cache, no-store' }, { 'Pragma': 'no-cache' }];
    this.noTokenScheme = config.noTokenScheme || false;
  }

  public getToken = (): TokenConfig => {
    return {
      headerName: this.headerName,
      headerPrefix: this.headerPrefix,
      tokenName: this.tokenName,
      tokenGetter: this.tokenGetter,
      noJwtError: this.noJwtError,
      noTokenScheme: this.noTokenScheme,
      globalHeaders: this.globalHeaders
    };
  }

  public removeToken = (): void => {
    sessionStorage.removeItem(this.tokenName);
  }

  public setToken = (encryptedToken: any): void => {
    sessionStorage.removeItem(this.tokenName);
    sessionStorage.setItem(this.tokenName, encryptedToken);
  }

  public urlBase64Decode = (str: string): any => {
    let output = str.replace(/-/g, '+').replace(/_/g, '/');
    switch (output.length % 4) {
      case 0:
        break;
      case 2:
        output += '==';
        break;
      case 3:
        output += '=';
        break;
      default:
        throw new Error('Illegal base64url string!');
    }

    return decodeURIComponent(escape(typeof window === 'undefined' ? atob(output) : window.atob(output)));
    // polyfill https://github.com/davidchambers/Base64.js
  }

  public decodeToken = (token: string): any => {
    const parts = token.split('.');

    if (parts.length !== 3) {
      throw new Error('JWT must have 3 parts');
    }

    const decoded = this.urlBase64Decode(parts[1]);
    if (!decoded) {
      throw new Error('Cannot decode the token');
    }

    return JSON.parse(decoded);
  }

  public getTokenExpirationDate = (token: string): any => {
    let decoded: any;
    decoded = this.decodeToken(token);

    if (typeof decoded.exp === 'undefined') {
      return null;
    }

    const date = new Date(0); // The 0 here is the key, which sets the date to the epoch
    date.setUTCSeconds(decoded.exp);

    return date;
  }

  public isTokenExpired = (token: string, offsetSeconds?: number): any => {
    const date = this.getTokenExpirationDate(token);
    offsetSeconds = offsetSeconds || 0;
    if (date === null) {
      return false;
    }

    // Token expired?
    return !(date.valueOf() > (new Date().valueOf() + (offsetSeconds * 1000)));
  }

  /**
   * Checks for presence of token and that token hasn't expired.
   * For use with the @CanActivate router decorator and NgIf
   */

  public tokenNotExpired = (tokenName = 'jwt', jwt?: string): boolean => {

    const token: string = jwt || this.tokenGetter(); // localStorage.getItem(tokenName);
    return token && !this.isTokenExpired(token, null);
  }

}
