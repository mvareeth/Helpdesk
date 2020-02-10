import { Component, OnInit } from '@angular/core';

import { TicketListModel } from '../../model/ticket-list.model';
import { HelpdeskService } from '../shared/helpdesk.service';

@Component({
  selector: 'app-helpdesk-own-list',
  templateUrl: './helpdesk-own-list.component.html',
  styleUrls: ['./helpdesk-own-list.component.css']
})
export class HelpdeskOwnListComponent implements OnInit {
  public ticketList: TicketListModel[];

  private gridColumnApi: any;
  public ticketListColumnDefs = [];
  public ticketListGridApi: any;
  public showDialog: boolean = false;
  public helpdeskId: number;

  constructor(private helpdeskService: HelpdeskService) {
    this.rankListGrid();
    this.getOwnTicketList();
  }

  ngOnInit() {
  }

  private getOwnTicketList() {
    this.helpdeskService.getOwnTickets()
      .subscribe(data => {
        this.ticketList = data;
      });
    // this.ticketList = [];
    // this.ticketList.push({ id: 100, title: 'test', description: 'First ', complexity: 1, priority: 1, status: 'opem', assignedTo: '' });
  }

  public onGridReady(params) {
    this.ticketListGridApi = params.api;
    this.gridColumnApi = params.columnApi;

    params.api.sizeColumnsToFit();
  }

  private rankListGrid() {
    this.ticketListColumnDefs = [
      {
        headerName: 'Id',
        field: 'id',
        width: 70
      },
      {
        headerName: 'Title',
        field: 'title',
        filter: true,
        resizable: true,
        cellRenderer: this.generateFilterLink.bind(this),
      },
      {
        headerName: 'Description',
        field: 'description',
        filter: true,
        resizable: true
      },
      {
        headerName: 'Complexity',
        field: 'complexity',
        type: 'numericColumn',
        filter: 'agNumberColumnFilter', cellClass: 'numberic-cell background-gray',
        resizable: true
      },
      {
        headerName: 'Priority',
        field: 'priority',
        type: 'numericColumn',
        filter: 'agNumberColumnFilter', cellClass: 'numberic-cell background-gray',
        resizable: true
      },
      {
        headerName: 'Status',
        field: 'status',
        filter: true,
        resizable: true
      }
    ];
  }

  /**
   * method to generate filter link
   */
  private generateFilterLink(params) {
    const filterLink = document.createElement('a'), self = this;
    self.showDialog = false;
    self.helpdeskId = undefined;
    filterLink.setAttribute(
      'class',
      `agGridLink tabbingContent tabbingItem_${params.column.getColId()}_${params.rowIndex}`
    ); // Class for CSS
    filterLink.setAttribute('href', '#');
    filterLink.setAttribute('title', `Open ${params.data.title}`);
    filterLink.setAttribute('id', `${params.data.id}`);
    filterLink.innerHTML = params.data.title;
    filterLink.addEventListener('click', function ($event) {
      $event.preventDefault();
      self.helpdeskId = +(($event.target) as any).id;
      self.showDialog = true;
    });
    return filterLink;
  }

  public hidePopupWindow() {
    this.helpdeskId = undefined;
    this.showDialog = false;
  }
}
