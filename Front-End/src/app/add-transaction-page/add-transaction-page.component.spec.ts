import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTransactionPageComponent } from './add-transaction-page.component';

describe('AddTransactionPageComponent', () => {
  let component: AddTransactionPageComponent;
  let fixture: ComponentFixture<AddTransactionPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTransactionPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTransactionPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
