import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageWithNavigationMenuComponent } from './page-with-navigation-menu.component';

describe('PageWithNavMenuComponent', () => {
  let component: PageWithNavigationMenuComponent;
  let fixture: ComponentFixture<PageWithNavigationMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PageWithNavigationMenuComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PageWithNavigationMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
