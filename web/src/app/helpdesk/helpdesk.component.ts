import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-helpdesk',
  templateUrl: './helpdesk.component.html',
  styleUrls: ['./helpdesk.component.css']
})
export class HelpdeskComponent implements OnInit {
  // @Output() public refreshOwnList: EventEmitter<boolean> = new EventEmitter<boolean>();
  // @Output() public refreshFullList: EventEmitter<boolean> = new EventEmitter<boolean>();
   
  public isBusy = false;
  public busyMessage: string;
  public selectedTab: string = 'myTicketTab';
  public showDialog: boolean = false;
  public helpdeskId: number;

  public constructor(private router: Router) { }

  public ngOnInit() {
  }

  public onSelect(selectedTab: string): void {
    this.selectedTab = selectedTab;
  }

  public addTicket() {
    this.helpdeskId = undefined;
    this.showDialog = true;
  }

  public hidePopupWindow() {
    this.selectedTab = 'myTicketTab';
    this.router.navigateByUrl('/helpdesk/own'); 
    // Todo: based on the selected tab we have to call corresponding grid refresh 
    this.showDialog = false;
  }

}
