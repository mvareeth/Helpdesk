import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { BaseService, CommonDataService } from '../shared/services';
import { ClientModel } from '../model/client.model';

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
    public getClientList() {
        return this.commonDataService.get(this.getURL('list'), false)
        .pipe(
            map(this.convertData)
        );        
    }
    private convertData = (response: any): any => {
        response.forEach(item => {
            item.fullName = item.firstName + ' ' + item.lastName;
        });
        return response;
    }       
}