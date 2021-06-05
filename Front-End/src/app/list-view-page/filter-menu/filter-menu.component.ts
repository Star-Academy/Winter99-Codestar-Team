import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-filter-menu',
  templateUrl: './filter-menu.component.html',
  styleUrls: ['./filter-menu.component.scss'],
})
export class FilterMenuComponent implements OnInit {
  public formGroup: FormGroup = new FormGroup({
    transactionId: new FormControl('', []),
    sourceAccountId: new FormControl('', []),
    destinationAccountId: new FormControl('', []),
    minAmount: new FormControl('', []),
    maxAmount: new FormControl('', []),
    startDateTime: new FormControl('', []),
    endDateTime: new FormControl('', []),
  });
  public menuVisibility: string = 'container-hide';

  constructor() {}

  ngOnInit(): void {}

  onFormSubmit() {}

  toggleMenuVisibilaty() {
    this.menuVisibility =
      this.menuVisibility == 'container-show'
        ? 'container-hide'
        : 'container-show';
  }
}
