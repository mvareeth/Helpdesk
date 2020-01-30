/* tslint:disable */
import { TypeGuardUtil } from '../classes';
import { Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';

@Injectable()
export class ValidationService {
    public static getValidatorErrorMessage(validatorCode: string, validatorValue?: any) {
        let config: any = {
            'required': 'Required',
            'invalidEmailAddress': 'Invalid email address',
            'invalidPassword': 'Invalid password. Password must be at least 6 characters long, and contain a number.',
            'invalidMaxlength': 'max length',
            'maxlength': `Exceeded max length ${validatorValue.maxLength}`,
            'minlength': `Minimum length ${validatorValue.requiredLength}`,
            'invalidZipCode': 'Invalid zipcode',
            'invalidLatitude': 'Invalid Latitude',
            'invalidLongitude': 'Invalid Longitude',
            'invalidPhoneNumber': 'Invalid Phone Number',
            'invalidNumeric': 'Invalid numeric value',            
        };
    }
    public static emailValidator(control: any) {
        return !ValidationService.isEmailValid(control.value) ? { 'invalidEmailAddress': true } : null;
    }

    public static emailOptionalValidator(control: any) {
        if (!TypeGuardUtil.isNullOrUndefined(control.value))
            return !ValidationService.isEmailValid(control.value) ? { 'invalidEmailAddress': true } : null;

        return null;
    }

    public static isEmailValid(value: string): boolean {
        if (value === null) {
            return false;
        }
        // RFC 2822 compliant regex
        return /[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?/.test(value);
    }

    public static numericValidator(control: any) {
        if (!TypeGuardUtil.isNullOrUndefined(control.value)) {
            return !ValidationService.isNumeric(control.value) ? { 'invalidNumeric': true } : null;
        }
        return null;
    }

    public static isNumeric(value: string): boolean {
        if (value === null) {
            return false;
        }
        return /^[0-9]*$/.test(value);
    }

    public static zipCodeValidator(control: any) {
        return !ValidationService.isZipValid(control.value) ? { 'invalidZipCode': true } : null;
    }

    public static isZipValid(value: string): boolean {
        if (value === null) {
            return false;
        }
        //we do store numbers only for ZipCode, 
        //so we do not validate format - it's a control business to display formatted string
        return (value.length === 5) || (value.length === 9);
    }

    public static passwordValidator(control: any) {
        // {6,100}           - Assert password is between 6 and 100 characters
        // (?=.*[0-9])       - Assert a string has at least one number
        if (control.value != null &&
            control.value.match(/^(?=.*[0-9])[a-zA-Z0-9!@#$%^&*]{6,100}$/)) {
            return null;
        } else {
            return { 'invalidPassword': true };
        }
    }

    public static maxlengthValidator(control: any): any {
        if (control.value != null) {
            alert(control.maxlength);
        } else {
            return { 'invalidMaxlength': true };
        }
    }

    public static stateValidator(control: any) {
        return { 'required': true };
    }

    public static stringIsNotNullOrEmptyValidator = (control: FormControl): any => {
        const value = control.value;
        const valid = control.value !== undefined && (<string>(control.value)).trim() !== '';
        return valid ? null : { error: true };
    }
}
