import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

// our service
import { BaseService } from '../base';
import { CommonDataService } from '../http';
import { LoginUser } from './login-user.model';

import { TokenService, Token } from '../token';
import { SubscriptionService } from '../subscription';

@Injectable()
export class AuthenticationService extends BaseService {
    private user: LoginUser;
    private isLoggedIn = false;
    private isloggingOut = false; // whenever the user clicked on the logout button but logout is not yet completed.
    public redirectUrl: string = '';

    public constructor(private commonDataService: CommonDataService, private tokenService: TokenService,
        private subscriptionService: SubscriptionService) {
        super('api', 0, 0, 0);
        this.user = { userName: '', password: '' };
    }

    private endSession(): any {
        if (this.tokenService.tokenNotExpired()) {
            return this.commonDataService.post(null, 'api/security/endsession');
        } else {
            let isTokenExpired: boolean = true;
            return of(isTokenExpired);
        }
    }

    public isLoggedOut(): boolean {
        return this.isloggingOut;
    }

    public logout(): any {
        if (!this.isloggingOut) {
            this.isloggingOut = true;
            return this.endSession()
                .map((response: any) => {
                    this.isLoggedIn = false;
                    this.tokenService.removeToken();
                });
        } else {
            this.isLoggedIn = false;
            return of(this.isloggingOut);
        }
    }

    public isUserAuthenticated(): boolean {
        return this.isLoggedIn;
    }

    public getLogInUser(): LoginUser {
        return this.user;
    }

    public login = (user: LoginUser, isDummyUser = false): any => {
        this.tokenService.removeToken();
        const data = { userName: user.userName, password: user.password };
        return this.commonDataService
            .post(data, 'api/jwt/token', null, true)
            .pipe(
                map(this.convertData)
            );
    }

    public convertData = (response: any): any => {
        const token: Token = response as Token;
        this.tokenService.setToken(token.access_token);
        this.user.userName = '';
        this.user.password = '';
    }
}
