import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-filter-menu',
  templateUrl: './filter-menu.component.html',
  styleUrls: ['./filter-menu.component.scss'],
})
export class FilterMenuComponent implements OnInit {
  formGroup: FormGroup = new FormGroup({
    id: new FormControl('', []),
    sourceAccountId: new FormControl('', []),
    destinationAccountId: new FormControl('', []),
    minAmount: new FormControl('', []),
    maxAmount: new FormControl('', []),
    startDateTime: new FormControl('', []),
    endDateTime: new FormControl('', []),
  });

  constructor() {}

  ngOnInit(): void {}
  onFormSubmit() {}
}
