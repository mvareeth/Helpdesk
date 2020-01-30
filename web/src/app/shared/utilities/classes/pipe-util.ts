import { DatePipe } from '@angular/common';
import * as moment from 'moment';

export class PipeUtil {

    public static getCurrentDateTimeAsString = (): string => {
        const currentDate: Date = new Date(Date.now());
        return moment(currentDate).format('MM/DD/YYYY hh:mm a');
    }

    public static getCurrentDateTime = (): Date => {
        return new Date(Date.now());;
    }

    public static convertStringToDate = (value: string): Date => {
        const nd = new Date(value);
        return nd;
    }

    public static convertDateTimeToDate = (value: Date): Date => {
        const cd = new Date(value.toString()).toISOString();
        const dt = new Date(cd).setHours(0, 0, 0, 0);
        return new Date(dt);
    }

    /** Converts a boolean to 'Yes' or 'No' */
    public static getBooleanDisplay(value: boolean): string {
        return value ? 'Yes' : 'No';
    }

    /** Converts a boolean to 'Active' or 'Inactive' */
    public static getActiveDisplay(value: boolean): string {
        return value ? 'Active' : 'Inactive';
    }
}
