import { KeyValue } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TransactionFilter } from '../models/TransactionFilter';

@Component({
  selector: 'app-filters-list',
  templateUrl: './filters-list.component.html',
  styleUrls: ['./filters-list.component.scss'],
})
export class FiltersListComponent implements OnInit {
  public filters: TransactionFilter[] = [
    {
      id: '894618454',
      // sourceAccountId: null,
      // destinationAccountId: null,
      // minAmount: null,
      // maxAmount: null,
      // startDateTime: null,
      // endDateTime: null,
    },
    {
      // id: '894618454',
      // sourceAccountId: null,
      // destinationAccountId: null,
      minAmount: '4413554515',
      // maxAmount: null,
      // startDateTime: null,
      // endDateTime: null,
    },
    {
      // id: '894618454',
      // sourceAccountId: null,
      destinationAccountId: '4653654131',
      // minAmount: null,
      // maxAmount: null,
      // startDateTime: null,
      // endDateTime: null,
    },
    {
      // id: '894618454',
      // sourceAccountId: null,
      // destinationAccountId: null,
      // minAmount: null,
      // maxAmount: null,
      startDateTime: new Date(1999, 5, 15, 16, 45, 20),
      endDateTime: new Date(2020, 5, 15, 16, 45, 20),
    },
  ];

  constructor() {}

  ngOnInit(): void {}

  getFilterValues(filter: TransactionFilter): KeyValue<string, string>[] {
    let values: KeyValue<string, string>[] = [];
    if (filter.id) {
      values.push({ key: 'شماره مرجع', value: filter.id });
    }
    if (filter.sourceAccountId) {
      values.push({ key: 'شماره حساب مبدا', value: filter.sourceAccountId });
    }
    if (filter.destinationAccountId) {
      values.push({
        key: 'شماره حساب مقصد',
        value: filter.destinationAccountId,
      });
    }
    if (filter.minAmount) {
      values.push({ key: 'حداقل مقدار تراکنش', value: filter.minAmount });
    }
    if (filter.maxAmount) {
      values.push({ key: 'حداکثر مقدار تراکنش', value: filter.maxAmount });
    }
    if (filter.startDateTime) {
      values.push({
        key: 'از تاریخ',
        value: filter.startDateTime.toISOString(),
      });
    }
    if (filter.endDateTime) {
      values.push({ key: 'تا تاریخ', value: filter.endDateTime.toISOString() });
    }
    return values;
  }

  removeFilter(index: number) {
    this.filters = this.filters
      .slice(0, index)
      .concat(this.filters.slice(index + 1));
  }
}
