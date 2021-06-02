import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteNavigationMenuComponent } from './site-navigation-menu.component';

describe('SiteNavigationMenuComponent', () => {
  let component: SiteNavigationMenuComponent;
  let fixture: ComponentFixture<SiteNavigationMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteNavigationMenuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteNavigationMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
