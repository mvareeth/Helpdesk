import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HelpdeskFullListComponent } from './helpdesk-full-list.component';

describe('HelpdeskFullListComponent', () => {
  let component: HelpdeskFullListComponent;
  let fixture: ComponentFixture<HelpdeskFullListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HelpdeskFullListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HelpdeskFullListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
