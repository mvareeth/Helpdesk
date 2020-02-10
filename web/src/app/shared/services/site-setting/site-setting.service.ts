import { environment } from './../../../../environments/environment';
import { LocationStrategy } from '@angular/common';
import { Injectable } from '@angular/core';

import { ILoggerSetting } from './shared/logger-setting.model';
import { IAuthSetting } from './shared/auth-setting.model';

@Injectable()
export class SiteSettingService {
    public isProduction: boolean;
    private serviceUri: string;

    private clientLoggerSettings: ILoggerSetting;

    public constructor(private locationStrategy: LocationStrategy
    ) {
        this.isProduction = environment.production;
        this.serviceUri = environment.baseUrl + '/';
    }

    public authSettings(): IAuthSetting {
        const authSettings: IAuthSetting = {
            serviceUri: this.serviceUri,
            enableApplicationCache: false,
            clientId: 'helpdesk'
        };
        return authSettings;
    }

    public set loggerSettings(data: ILoggerSetting) {
        this.clientLoggerSettings = {
            info: data.info,
            warning: data.warning,
            success: data.success,
            error: data.error
        };
    }

    public get loggerSettings(): ILoggerSetting {
        return this.clientLoggerSettings;
    }
}
