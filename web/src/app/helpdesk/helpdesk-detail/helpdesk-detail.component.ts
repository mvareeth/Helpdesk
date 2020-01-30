import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-helpdesk-detail',
  templateUrl: './helpdesk-detail.component.html',
  styleUrls: ['./helpdesk-detail.component.css']
})
export class HelpdeskDetailComponent implements OnInit {

  @Input() public helpdeskId: number;
  @Output() public closePopup: EventEmitter<boolean> = new EventEmitter<boolean>();

  public display: boolean = true;
  public popupTitle: string;

  public clientName: string;
  public title: string;
  public problemDescription: string;
  public assignedUserFullName: string;
  public workItemStatus: any;
  public workItemStatusList: any;

  public canEdit: boolean = true; // depends on permission we can disable or enable edit
  public enableSaveButton: boolean;

  public constructor() { }

  public ngOnInit() {
    if (this.helpdeskId) {
      this.popupTitle = 'Edit Helpdesk Ticket : ' + this.helpdeskId;
    } else {
      this.popupTitle = 'Add Helpdesk Ticket';
    }
    this.getStatusList();
  }

  public save() {
    //
  }
  public cancel() {
    //
  }
  private getStatusList() {
    // this.appService.getStatusList()
    //   .subscribe((data: any) => {
    //     this.workItemStatusList = data;
    //   });
  }

  public hidePopup() {
    this.closePopup.emit(true);
  }
}
