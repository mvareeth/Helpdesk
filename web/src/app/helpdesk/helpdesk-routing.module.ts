import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HelpdeskComponent } from './helpdesk.component';
import { HelpdeskOwnListComponent } from './helpdesk-own-list/helpdesk-own-list.component';
import { HelpdeskFullListComponent } from './helpdesk-full-list/helpdesk-full-list.component';
import { AuthGuard } from '../router-guard/auth.guard';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: HelpdeskComponent,
                //canActivate: [AuthGuard],
                children: [
                    {
                        path: 'own',
                        component: HelpdeskOwnListComponent
                    },
                    {
                        path: 'all',
                        component: HelpdeskFullListComponent
                    },
                    {
                        path: '',
                        redirectTo: 'own',
                        pathMatch: 'full',
                        data: { title: 'ticktes' }
                    }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class HelpDeskRoutingModule { }
