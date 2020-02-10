import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UtilService } from '../services/util.service';

import { MessageService } from 'primeng/api';
import { AuthenticationService, HttpError } from '../shared/services';
import { LoginUser } from '../shared/services/authentication';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public userName: string;
  public password: string;
  public welcomeMessage: string;
  public user: LoginUser;
  public showBusyIndicator: boolean = false;
  public errorMessage: string = '';

  public constructor(private router: Router,
    private authenticationService: AuthenticationService,
    private utilService: UtilService,
    private messageService: MessageService) {

    this.user = authenticationService.getLogInUser();
  }

  public ngOnInit() {
    //
  }

  public get enableLoginButton(): boolean {
    return !this.utilService.isNullOrUndefined(this.userName) && !this.utilService.isNullOrUndefined(this.password);
  }

  public login() {
    this.user.userName = this.userName;
    this.user.password = this.password;

    this.authenticationService.login(this.user)
      .subscribe(
        (data: any) => {
          this.showBusyIndicator = false;
          this.router.navigateByUrl('/helpdesk');
        },
        (error: HttpError) => {
          this.errorMessage = error.message;
          this.messageService.add({ severity: 'error', summary: this.errorMessage });
          this.showBusyIndicator = false;
        }
      );
  }
}
