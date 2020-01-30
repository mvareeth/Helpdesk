import { Injectable } from '@angular/core';

import { UtilService } from './util.service';
import { UserProfile } from '../model/user-profile.model';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  public selectedCollegeId = 1;
  public loggedIn: UserProfile;
  public canEditCurrentApplication = false;

  constructor(private utilService: UtilService) { }

  public get WelcomeMessage(): string {
    if (this.loggedIn) {
      return 'Welcome ' + this.loggedIn.FirstName + ' ' + this.loggedIn.LastName;
    } else {
      return '';
    }
  }
}
