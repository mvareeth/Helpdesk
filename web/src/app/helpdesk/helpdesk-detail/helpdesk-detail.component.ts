import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HelpdeskService } from '../shared/helpdesk.service';
import { TicketDetailModel } from 'src/app/model/ticket-detail.model';
import { HttpError } from 'src/app/shared/services';
import { MessageService } from 'primeng/api';
import { TicketModel } from 'src/app/model/ticket.model';
import { ClientService } from 'src/app/services/client.service';
import { ClientModel } from 'src/app/model/client.model';

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

  // public clientName: string;
  public title: string;
  public problemDescription: string;
  public assignedUserFullName: string;
  public workItemStatus: any;
  public workItemStatusList: any;
  public clientList: any;
  public selectedClient: any;

  public canEdit: boolean = true; // depends on permission we can disable or enable edit
  public get enableSaveButton(): boolean {
    if (this.selectedClient) {
      return true;
    }
  }

  public ticketDetail: TicketDetailModel;

  public constructor(private helpdeskService: HelpdeskService, private clientService: ClientService,
      private messageService: MessageService) { }

  public ngOnInit() {
    this.getStatusList();
    this.getClientList();    
    if (this.helpdeskId) {
      this.popupTitle = 'Edit Helpdesk Ticket : ' + this.helpdeskId;
      this.getTicketDetail(this.helpdeskId);
    } else {
      this.popupTitle = 'Add Helpdesk Ticket';
    }
  }

  public save() {
    const  ticketModel = new TicketModel();
    ticketModel.title = this.title;
    ticketModel.description = this.problemDescription;
    ticketModel.clientId = this.selectedClient.id;
    if (this.ticketDetail) {
      ticketModel.id = this.ticketDetail.id;
    }
    this.saveTicket(ticketModel);
  }

  public cancel() {
    this.closePopup.emit(false);
  }
  private getStatusList() {
    // this.appService.getStatusList()
    //   .subscribe((data: any) => {
    //     this.workItemStatusList = data;
    //   });
  }
  private getClientList() {
    this.clientService.getClientList()
      .subscribe((data: any) => {
        this.clientList = data;
      });
  }

  public hidePopup() {
    this.display = false;
    this.closePopup.emit(true);
  }

  private getTicketDetail(ticketId: number) {
    this.helpdeskService.getTicket(ticketId)
    .subscribe((data : TicketDetailModel ) => {
      this.ticketDetail = data;
      this.selectedClient = data.client as ClientModel;
      this.populateData(data);
    });
  }

  private populateData(ticketDetail: TicketDetailModel) {
    this.selectedClient.fullName = this.selectedClient.firstName + ' ' + this.selectedClient.lastName;
    this.title = ticketDetail.title;
    this.problemDescription = ticketDetail.description;
    const assigedTechnician = (ticketDetail.assigedTechnician as any);
    if (assigedTechnician !== undefined) {
      this.assignedUserFullName = assigedTechnician.firstName + ' ' + assigedTechnician.lastName;
    }
  }

  private saveTicket(ticketDetail: TicketModel) {
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
