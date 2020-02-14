import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { LoggerService } from '../logger/logger.service';

@Injectable()
export class GlobalExceptionHandlerService extends ErrorHandler {
    private loggerService: LoggerService;

    public constructor(private injector: Injector) {
        super();
    }

    public handleError(error: any): void {
        this.loggerService = this.injector.get(LoggerService) as LoggerService;
        // this.loggerService = <LoggerService>this.injector.get(LoggerService);
        // window.alert(error);
        this.loggerService.error(error.message, null, null, true, true);
        super.handleError(error);
    }
}
