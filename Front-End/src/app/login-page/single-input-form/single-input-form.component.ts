import { Component, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { FormValues } from '../models/formValues';

@Component({
  selector: 'app-single-input-form',
  templateUrl: './single-input-form.component.html',
  styleUrls: ['./single-input-form.component.scss'],
})
export class SingleInputFormComponent implements OnInit {
  public inputControl: FormControl;
  public hideInputValue: boolean;
  public formVisibility: string;
  private visibleClassName: string = 'visible';
  private invisibleClassName: string = 'invisible';

  public get inputType(): string {
    const visibleType =
      this.formValues.inputType == 'password'
        ? 'text'
        : this.formValues.inputType;
    return this.hideInputValue ? 'password' : visibleType;
  }

  @Input()
  formValues: FormValues;

  constructor() {
    this.formVisibility = this.visibleClassName;
  }

  ngOnInit() {
    if (!this.formValues) {
      throw new TypeError("'formValues' object is required.");
    }
    this.inputControl = new FormControl('', this.formValues.inputValidators);
    this.hideInputValue = this.formValues.inputType == 'password';
  }

  public get validationErrorMessage(): string {
    let errorMessage: string = '';
    if (this.inputControl.errors?.required) {
      errorMessage += 'ورودی می‌بایست پر شود\n';
    }
    return errorMessage;
  }

  toggleVisibility() {
    this.formVisibility =
      this.formVisibility == this.visibleClassName
        ? this.invisibleClassName
        : this.visibleClassName;
  }

  clickButton() {
    if (this.inputControl.errors) {
      throw new Error('Input validation failed.');
    }
    this.toggleVisibility();
    this.formValues.onSubmit(this.inputControl.value);
    setTimeout(() => {
      this.toggleVisibility();
    }, 1000);
  }
}
