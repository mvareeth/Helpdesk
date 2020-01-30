import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppHeaderComponent } from './app-header.component';

describe('AppHeaderComponent', () => {
  let component: AppHeaderComponent;
  let fixture: ComponentFixture<AppHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AppHeaderComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have banner', () => {
    const header: HTMLElement = fixture.nativeElement.querySelector('header');
    const classname = header.className;
    expect(classname).toBe('clearfix');
  });
  it('should have logo', () => {
    const img: HTMLImageElement = fixture.nativeElement.querySelector('img');
    const src = img.src;
    expect(src).toContain('assets/images/logo4.png');
  });
  it('should have alt text', () => {
    const img: HTMLImageElement = fixture.nativeElement.querySelector('img');
    const alt = img.alt;
    expect(alt).toBe('Business Ask Logo');
  });
  it('should have title', () => {
    const img: HTMLImageElement = fixture.nativeElement.querySelector('img');
    const title = img.title;
    expect(title).toBe('Business Ask');
  });
  it('should have heading', () => {
    const h1: HTMLElement = fixture.nativeElement.querySelector('h1');
    const text = h1.textContent;
    expect(text).toBe('Business Ask');
  });
});
