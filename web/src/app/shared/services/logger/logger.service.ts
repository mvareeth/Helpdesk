import { Injectable } from '@angular/core';
import { TokenService } from '../token/token.service';
import { CommonHttpService } from '../http/common-http.service';
import { SiteSettingService } from '../site-setting/site-setting.service';
import { ILoggerSetting } from '../site-setting/shared/logger-setting.model';
import { ConsoleLoggerService } from './console-logger.service';
import { MessageService } from 'primeng/api';

@Injectable()
export class LoggerService {
    private sasErrorMessage: string;

    public constructor(private $log: ConsoleLoggerService, private messageService: MessageService,
        private tokenService: TokenService, private commonHttpService: CommonHttpService,
        private siteSettings: SiteSettingService) {

    }

    public log = (message: string, data?: any, source?: string, showToast?: boolean, saveToDB?: boolean) => {
        this.logIt(message, 'info', data, source, showToast, saveToDB);
    }

    public warning = (message: string, data: any = null, source: string = null, showToast = true, saveToDB = false) => {
        this.logIt(message, 'warning', data, source, showToast, saveToDB);
    }

    public success = (message: string, data: any = null, source: string = null, showToast = true, saveToDB = false) => {
        this.logIt(message, 'success', data, source, showToast, saveToDB);
    }

    public error = (message: string, data: any = null, source: string = null, showToast = true, saveToDB = false) => {
        this.logIt(message, 'error', data, source, showToast, saveToDB);
    }

    public logIt = (message: string, toastType: string, data: any = null, source: string = null, showToast = true, saveToDB = false) => {
        const write = (toastType === 'error') ? this.$log.error : this.$log.log;
        source = source ? '[' + source + '] ' : '';
        write(source, message, data);
        if (showToast) {
            this.messageService.add({ severity: toastType, summary: message });
            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
        // Call the Service Layer
        if (!this.siteSettings.isProduction || !this.tokenService.tokenNotExpired()) {
            saveToDB = false;
        }
        if (saveToDB) {
            const loggerSettings: ILoggerSetting = this.getLogSetting();
            if (loggerSettings.info && toastType === 'info') {
                saveToDB = false;
            } else if (loggerSettings.success && toastType === 'success') {
                saveToDB = false;
            } else if (loggerSettings.warning && toastType === 'warning') {
                saveToDB = false;
            } else if (loggerSettings.error && toastType === 'error') {
                saveToDB = true;
            }
        }
        if (saveToDB) {
            let stackTrace = '';
            let errorNumber = '';
            let description = toastType.toUpperCase() + ' from Angular client';
            if (data != null && data != undefined) {
                stackTrace = (data.stack === undefined) ? '' : data.stack;
                errorNumber = (data.number === undefined) ? 0 : data.number;
                description = (data.description === undefined) ? description : data.description;
            }
            let loggerVm = {
                'logType': toastType,
                'message': message,
                'stackTrace': stackTrace,
                'errorNumber': errorNumber,
                'description': description,
                'source': (source === undefined) ? 'Angular' : source,
                'ipAddress': '',
                'browser': '',
                'userName': ''
            };
            this.logToDatabase(loggerVm);
        }
    }

    private logToDatabase = (loggerVm: any) => {
        const apiServiceBaseUri = this.siteSettings.authSettings().serviceUri;
        const url = 'api/logger/log';
        return this.commonHttpService.post((apiServiceBaseUri + url), loggerVm)
            .subscribe((response: any) => {
                // log the error in DB
            },
                (error: any) => {
                    this.$log.error('saveToDB', error.message, null);
                });
    }

    private getLogSetting = () => {
        if (this.siteSettings.loggerSettings !== undefined) {
            return this.siteSettings.loggerSettings;
        } else {
            const url = 'api/logsettings';
            return this.commonHttpService.get(url, null, null)
                .subscribe((response: any) => {
                    // set logger settings
                    this.siteSettings.loggerSettings = response;
                },
                    (error: any) => {
                        this.$log.error('getLogSetting', error.message, null);
                    });
        }
    }

    /* a method to store the sass error message */
    public get errorMessage(): string {
        return this.sasErrorMessage;
    }

    public set errorMessage(sasErrorMessage: string) {
        this.sasErrorMessage = sasErrorMessage;
    }
}
