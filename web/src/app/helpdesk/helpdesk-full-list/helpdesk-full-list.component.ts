import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

import { TicketListModel } from '../../model/ticket-list.model';
import { HelpdeskService } from '../shared/helpdesk.service';

@Component({
  selector: 'app-helpdesk-full-list',
  templateUrl: './helpdesk-full-list.component.html',
  styleUrls: ['./helpdesk-full-list.component.css']
})
export class HelpdeskFullListComponent implements OnInit {
  public ticketList: TicketListModel[];

  private gridColumnApi: any;
  public ticketListColumnDefs = [];
  public ticketListGridApi: any;

  constructor(private helpdeskService: HelpdeskService) {
    this.rankListGrid();
    this.getTeamTicketList();
  }

  ngOnInit() {
  }

  private getTeamTicketList() {
    this.helpdeskService.getTeamTickets()
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
        resizable: true
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
        headerName: 'Assigned To',
        field: 'assignedTo',
        filter: true,
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
}
