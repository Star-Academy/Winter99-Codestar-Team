import { KeyValue } from '@angular/common';

export interface TransactionFilter {
  id?: string;
  sourceAccountId?: string;
  destinationAccountId?: string;
  minAmount?: string;
  maxAmount?: string;
  startDateTime?: Date;
  endDateTime?: Date;
}
