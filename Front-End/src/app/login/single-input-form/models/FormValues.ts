import { ValidatorFn, Validators } from '@angular/forms';

export class FormValues {
  public inputName: string;
  public inputType: string;
  public onSubmit: (para: string) => void;
  public linkText: string | null;
  public linkHref: string | null;
  public inputValidators: ValidatorFn[];

  constructor(
    inputName: string,
    inputType: string,
    onSubmit: (para: string) => void,
    inputValidators: ValidatorFn[] = [],
    linkText: string | null = null,
    linkHref: string | null = null
  ) {
    this.inputName = inputName;
    this.inputType = inputType;
    this.onSubmit = onSubmit;
    this.inputValidators = inputValidators;
    this.inputValidators.push(Validators.required);
    if (!linkHref != !linkText) {
      throw new TypeError(
        "'linkText' and 'linkType' aren't required except when you input one of them."
      );
    }
    this.linkText = linkText;
    this.linkHref = linkHref;
  }
}
