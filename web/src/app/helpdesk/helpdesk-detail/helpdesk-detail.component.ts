import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HelpdeskService } from '../shared/helpdesk.service';
import { TicketDetailModel } from 'src/app/model/ticket-detail.model';
import { HttpError } from 'src/app/shared/services';
import { MessageService } from 'primeng/api';

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

  public ticketDetail: TicketDetailModel;

  public constructor(private helpdeskService: HelpdeskService, private messageService: MessageService) { }

  public ngOnInit() {
    if (this.helpdeskId) {
      this.popupTitle = 'Edit Helpdesk Ticket : ' + this.helpdeskId;
      this.getTicketDetail(this.helpdeskId);
    } else {
      this.popupTitle = 'Add Helpdesk Ticket';
      this.enableSaveButton = true;
    }
    this.getStatusList();
  }

  public save() {
    if (!this.ticketDetail) {
      this.ticketDetail = new TicketDetailModel();
    }
    this.ticketDetail.title = this.title;
    this.ticketDetail.description = this.problemDescription;    
    this.saveTicket(this.ticketDetail);
  }

  public cancel() {
    this.closePopup.emit(true);
  }
  private getStatusList() {
    // this.appService.getStatusList()
    //   .subscribe((data: any) => {
    //     this.workItemStatusList = data;
    //   });
  }

  public hidePopup() {
    this.display = false;
    this.closePopup.emit(true);
  }

  private getTicketDetail(ticketId: number) {
    this.helpdeskService.getTicket(ticketId)
    .subscribe(data => {
      this.ticketDetail = data;
      this.populateData(data);
      this.enableSaveButton = true;
    });
  }

  private populateData(ticketDetail: TicketDetailModel) {
    const client = (ticketDetail.client as any);
    if (client !== undefined) {
      this.clientName = client.firstName + ' ' + client.LastName;
    }
    this.title = ticketDetail.title;
    this.problemDescription = ticketDetail.description;
    const assigedTechnician = (ticketDetail.assigedTechnician as any);
    if (assigedTechnician !== undefined) {
      this.assignedUserFullName = assigedTechnician.firstName + ' ' + assigedTechnician.lastName;
    }
  }

  private saveTicket(ticketDetail: TicketDetailModel) {
    this.helpdeskService.saveTicket(ticketDetail)
      .subscribe(
        (data: any) => {
          this.messageService.add({ severity: 'success', summary: 'Successfully saved the ticket' });
          this.hidePopup();
        },
        (error: HttpError) => {
          this.messageService.add({ severity: 'error', summary: error.message });
        }
      ); 
  }
}
