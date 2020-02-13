import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { BaseService, CommonDataService } from '../shared/services';
import { EnumModel } from '../model/enum.model';

@Injectable({
  providedIn: 'root'
})
export class EnumService  {
    private statusList : EnumModel[];
    public constructor(private commonDataService: CommonDataService) {
        // super('api/enum', 0, 0, 0); // we can get the name and description from server enum
    }

    public getStatusList() {
        if (!this.statusList) {
            this.statusList=[];
            this.statusList.push(
               {id:1, name: "Created"},
               {id:2, name: "Assigned"},
               {id:3, name: "Open"},
               {id:4, name: "Closed"}
               );
        }
        return of(this.statusList);       
    }      
}