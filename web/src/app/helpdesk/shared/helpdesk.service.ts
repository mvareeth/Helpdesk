import { Injectable } from '@angular/core';
import { map, switchMap } from 'rxjs/operators';
import { BaseService, CommonDataService } from 'src/app/shared/services';

@Injectable()
export class HelpdeskService extends BaseService {
    public canEditCurrentApplication = false;

    public constructor(private commonDataService: CommonDataService) {
        super('api/ticket', 0, 0, 0);
    }

    public getOwnTickets() {
        return this.commonDataService.get(this.getURL('owntickets'), false)
            .map((response: any) => response);
    }

    public getTeamTickets() {
        return this.commonDataService.get(this.getURL('teamtickets'), false)
            .map((response: any) => response);
    }

    public getTicket(ticketId: number) {
        return this.commonDataService.get(this.getURL() + '/' + ticketId, false)
            .map((response: any) => response);
    }
}
