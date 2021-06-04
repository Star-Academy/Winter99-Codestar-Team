export class Transaction {
  public id: string;
  public sourceAccountId: string;
  public destinationAccountId: string;
  public amount: string;
  public dateTime: Date;

  constructor(
    id: string,
    sourceAccountId: string,
    destinationAccountId: string,
    amount: string,
    dateTime: Date
  ) {
    this.id = id;
    this.sourceAccountId = sourceAccountId;
    this.destinationAccountId = destinationAccountId;
    this.amount = amount;
    this.dateTime = dateTime;
  }
}
