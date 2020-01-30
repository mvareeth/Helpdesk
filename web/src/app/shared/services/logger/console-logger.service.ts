import { Injectable } from '@angular/core';
import { SiteSettingService } from '../site-setting/site-setting.service';

@Injectable()
export class ConsoleLoggerService {
    public constructor(private siteSettings: SiteSettingService) {
    }

    public log = (source: string, message: string, data: any): void => {
        if (!this.siteSettings.isProduction) {
            console.log('message:' + message + '\n source: ' + source + '\n stack trace:' + data);
        }
    }

    public error = (source: string, message: string, data: any): void => {
        if (!this.siteSettings.isProduction) {
            console.error('message:' + message + '\n source: ' + source + '\n stack trace:' + data);
        }
    }

    public info = (message: string): void => {
        if (!this.siteSettings.isProduction) {
            console.log('message:' + message);
        }
    }
}
