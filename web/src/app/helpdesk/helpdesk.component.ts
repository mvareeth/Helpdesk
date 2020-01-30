import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-helpdesk',
  templateUrl: './helpdesk.component.html',
  styleUrls: ['./helpdesk.component.css']
})
export class HelpdeskComponent implements OnInit {

  public isBusy = false;
  public busyMessage: string;
  public selectedTab: string = 'myTickeTab';
  public showDialog: boolean = false;

  public constructor() { }

  public ngOnInit() {
  }

  public onSelect(selectedTab: string): void {
    this.selectedTab = selectedTab;
  }

  public addTicket() {
    this.showDialog = true;
    this.selectedTab = 'myTickeTab';
  }

  public hidePopupWindow() {
    this.showDialog = false;
  }

}
