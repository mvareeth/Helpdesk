import { Injectable } from '@angular/core';
import { BaseService, CommonDataService } from '../shared/services';

@Injectable({
  providedIn: 'root'
})
export class ClientService extends BaseService {
    public constructor(private commonDataService: CommonDataService) {
        super('api/client', 0, 0, 0);
    }

    public getClient(clientId: number) {
        return this.commonDataService.get(this.getURL() + '/' + clientId, false);
    }
}