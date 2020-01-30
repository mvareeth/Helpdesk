import { AbstractControl, FormGroup, ValidatorFn } from '@angular/forms';
import { TypeGuardUtil } from '../classes/type-guard-util';

// tslint:disable-next-line:only-arrow-functions
export function decimalValidator(control: AbstractControl): any {
  if (!TypeGuardUtil.isNullOrUndefined(control.value)) {
    const value: string = control.value.toString() || '';
    const valid = value.match(/^(\d+\.?\d*|\.\d+)$/);
    return valid ? null : { pattern: true };
  } else {
    return null;
  }
}

// tslint:disable-next-line:only-arrow-functions
export function numericValidator(control: AbstractControl): any {
  if (!TypeGuardUtil.isNullOrUndefined(control.value)) {
    const value: string = control.value.toString() || '';
    const valid = value.match(/^[0-9]*$/);
    return valid ? null : { pattern: true };
  } else {
    return null;
  }
}

// tslint:disable-next-line:only-arrow-functions
export function emptyValidator(control: AbstractControl): any {
  if (!TypeGuardUtil.isNullOrUndefined(control.value)) {
    const value: string = control.value.toString() || '';
    const isEmpty = value.trim().length === 0;
    return !isEmpty ? null : { empty: true };
  } else {
    return null;
  }
}

