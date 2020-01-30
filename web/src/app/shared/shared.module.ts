import { CommonModule, DatePipe } from '@angular/common';
import { ErrorHandler, ModuleWithProviders, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { DropdownModule } from 'primeng/dropdown'
import { CalendarModule } from 'primeng/calendar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { ConfirmationService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { AgGridModule } from 'ag-grid-angular';

import { MomentModule } from 'angular2-moment';
import { ControlNames, DirectiveNames, PipeNames, ServiceNames } from './constants';
import * as Services from './services';

/**
 * Do not specify providers for modules that might be imported by a lazy loaded module.
 */
@NgModule({
    imports: [
        CommonModule, FormsModule, ReactiveFormsModule,
        DropdownModule, CalendarModule, DialogModule, ConfirmDialogModule, ToastModule,
        AgGridModule.withComponents([])
    ],
    declarations: [
        ...ControlNames,
        ...DirectiveNames,
        ...PipeNames,
    ],
    providers: [ConfirmationService, MessageService
        , DatePipe,
        { provide: ErrorHandler, useClass: Services.GlobalExceptionHandlerService }
    ],
    exports: [
        ...ControlNames,
        ...DirectiveNames,
        ...PipeNames,

        CommonModule, FormsModule, ReactiveFormsModule
        , DropdownModule, CalendarModule, DialogModule, ConfirmDialogModule, ToastModule, AgGridModule
        , MomentModule
    ]
})
export class SharedModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule,
            providers: [
                ...ServiceNames
            ]
        };
    }
}
