import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilService {

  public defauleDateValue: Date = new Date(1900, 1, 1);
  public today: Date = new Date();
  public minimumDateofBirth: Date = new Date(this.today.getFullYear() - 17, 8, 1); // august- minimu 17 years completed

  constructor() { }

  public isNullOrUndefined(stringValue: string | number) {
    if (stringValue !== undefined && stringValue !== null && stringValue !== '') {
      return false;
    } else {
      return true;
    }
  }

  public isUndefinedDate(dateValue: Date) {
    if (dateValue !== undefined && dateValue > this.defauleDateValue) {
      return false;
    } else {
      return true;
    }
  }

  public isEligibleDateofBirth(dateValue: Date) {
    if (dateValue === undefined || dateValue > this.minimumDateofBirth) {
      return false;
    } else {
      return true;
    }
  }

  public getString(value: string) {
    if (value === null || value === undefined) {
      return '';
    }
    return value;
  }

  public getNumber(value: string) {
    if (value === null || value === undefined || value === '') {
      return null;
    }
    return +value;
  }

  public getDateString(value: Date) {
    if (value === null || value === undefined) {
      return '';
    }
    return value.getFullYear() + '-' + (value.getMonth() + 1) + '-' + value.getDate();;
  }
}
