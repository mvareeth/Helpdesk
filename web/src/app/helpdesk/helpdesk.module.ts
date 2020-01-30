import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';

import { HelpDeskRoutingModule } from './helpdesk-routing.module';
import { HelpdeskComponent } from './helpdesk.component';
import { HelpdeskOwnListComponent } from './helpdesk-own-list/helpdesk-own-list.component';
import { HelpdeskFullListComponent } from './helpdesk-full-list/helpdesk-full-list.component';
import { HelpdeskDetailComponent } from './helpdesk-detail/helpdesk-detail.component';

import { HelpdeskService } from './shared/helpdesk.service';

@NgModule({
    imports: [
        HelpDeskRoutingModule,
        SharedModule
    ],
    declarations: [HelpdeskComponent, HelpdeskOwnListComponent, HelpdeskFullListComponent, HelpdeskDetailComponent],
    providers: [HelpdeskService],
    exports: [HelpdeskDetailComponent]
})

export class HelpdeskModule { }
