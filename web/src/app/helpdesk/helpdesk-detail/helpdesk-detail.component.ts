import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HelpdeskService } from '../shared/helpdesk.service';
import { TicketDetailModel } from 'src/app/model/ticket-detail.model';
import { HttpError } from 'src/app/shared/services';
import { MessageService } from 'primeng/api';
import { TicketModel } from 'src/app/model/ticket.model';
import { ClientService } from 'src/app/services/client.service';
import { ClientModel } from 'src/app/model/client.model';
import { UserProfileModel } from 'src/app/model/user-profile.model';
import { TechniciamService } from 'src/app/services/technician.service';
import { EnumService } from 'src/app/services/enum.service';

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
  public workItemStatus: any;
  public workItemStatusList: any;
  public clientList: any;
  public selectedClient: any;
  public selectedTechnician: any;
  public technicianList: any;

  public canEdit: boolean = true; // depends on permission we can disable or enable edit
  public get enableSaveButton(): boolean {
    if (this.selectedClient && this.title && this.workItemStatus) {
      return true;
    }
  }

  public ticketDetail: TicketDetailModel;

  public constructor(private helpdeskService: HelpdeskService, private clientService: ClientService,
      private technicianService: TechniciamService, private enumService: EnumService,
      private messageService: MessageService) { }

  public ngOnInit() {
    this.getStatusList();
    this.getClientList();    
    this.getTechnicianList();
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
    ticketModel.statusId = this.workItemStatus.id;
    if (this.selectedTechnician) ticketModel.assigedTechnicianId = this.selectedTechnician.id;
    if (this.ticketDetail) {
      ticketModel.id = this.ticketDetail.id;
    }
    this.saveTicket(ticketModel);
  }

  public cancel() {
    this.closePopup.emit(false);
  }
  private getStatusList() {
    this.enumService.getStatusList()
      .subscribe((data: any) => {
        this.workItemStatusList = data;
      });
  }
  private getClientList() {
    this.clientService.getClientList()
      .subscribe((data: any) => {
        this.clientList = data;
      });
  }

  private getTechnicianList() {
    this.technicianService.getTechnicianList()
      .subscribe((data: any) => {
        this.technicianList = data;
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
      this.selectedTechnician = data.assigedTechnician as UserProfileModel;
      this.workItemStatus = this.workItemStatusList.find(os=>os.id == data.statusId);
      this.populateData(data);
    });
  }

  private populateData(ticketDetail: TicketDetailModel) {
    this.selectedClient.fullName = this.selectedClient.firstName + ' ' + this.selectedClient.lastName;
    this.title = ticketDetail.title;
    this.problemDescription = ticketDetail.description;
    if (this.selectedTechnician) {
      this.selectedTechnician.fullName = this.selectedTechnician.firstName + ' ' + this.selectedTechnician.lastName;
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
