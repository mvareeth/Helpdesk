import { Injectable } from '@angular/core';
import { map, switchMap } from 'rxjs/operators';
import { BaseService, CommonDataService } from 'src/app/shared/services';
import { TicketDetailModel } from 'src/app/model/ticket-detail.model';
import { TicketModel } from 'src/app/model/ticket.model';

@Injectable()
export class HelpdeskService extends BaseService {
    public constructor(private commonDataService: CommonDataService) {
        super('api/ticket', 0, 0, 0);
    }

    public getOwnTickets() {
        return this.commonDataService.get(this.getURL('owntickets'), false);
    }

    public getTeamTickets() {
        return this.commonDataService.get(this.getURL('teamtickets'), false);
    }

    public getTicket(ticketId: number) {
        return this.commonDataService.get(this.getURL() + '/' + ticketId, false);
    }
  
    public saveTicket = (ticketModel : TicketModel): any => {
        return this.commonDataService
            .post(ticketModel, this.getURL('saveTicket'), null, true);
    }    
}
