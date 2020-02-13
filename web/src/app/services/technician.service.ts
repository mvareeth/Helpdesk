import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { BaseService, CommonDataService } from '../shared/services';

@Injectable({
  providedIn: 'root'
})
export class TechniciamService extends BaseService {
    public constructor(private commonDataService: CommonDataService) {
        super('api/user', 0, 0, 0);
    }

    public getTechnician(userId: number) {
        return this.commonDataService.get(this.getURL() + '/' + userId, false);
    }
    public getTechnicianList() {
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