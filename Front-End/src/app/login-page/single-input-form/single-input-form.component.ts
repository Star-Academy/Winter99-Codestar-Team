import { Component, Input, OnInit } from '@angular/core';
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
  @Input()
  formValues: FormValues;

  constructor() {}

  ngOnInit() {
    if (!this.formValues) {
      throw new TypeError("'formValues' object is required.");
    }
    this.inputControl = new FormControl('', this.formValues.inputValidators);
    this.hideInputValue = this.formValues.inputType == 'password';
  }

  public get inputType(): string {
    const visibleType =
      this.formValues.inputType == 'password'
        ? 'text'
        : this.formValues.inputType;
    return this.hideInputValue ? 'password' : visibleType;
  }

  public get validationErrorMessage(): string {
    let errorMessage: string = '';
    if (this.inputControl.errors?.required) {
      errorMessage += 'ورودی می‌بایست پر شود\n';
    }
    return errorMessage;
  }

  clickButton() {
    if (this.inputControl.errors) {
      throw new Error('Input validation failed.');
    }
    this.formValues.onSubmit(this.inputControl.value);
  }
}
