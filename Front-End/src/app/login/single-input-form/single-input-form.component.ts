import { Component, Input, OnInit, Output } from '@angular/core';
import {
  FormControl,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-single-input-form',
  templateUrl: './single-input-form.component.html',
  styleUrls: ['./single-input-form.component.scss'],
})
export class SingleInputFormComponent implements OnInit {
  @Input()
  public inputName: string;
  @Input()
  public inputType: string;
  @Input()
  public inputValidators: ValidatorFn[] = [];
  @Input()
  public linkText: string;
  @Input()
  public linkHref: string;
  @Output()
  public inputValue: string;
  public inputControl: FormControl;
  public hideInputValue: boolean = false;

  constructor() {
    this.inputValidators.push(Validators.required);
    this.inputControl = new FormControl('', this.inputValidators);
  }

  ngOnInit() {
    if (!this.inputName) {
      throw new TypeError("'inputName' is required.");
    }
    if (!this.inputType) {
      throw new TypeError("'inputType' is required.");
    }
    if (!this.linkHref != !this.linkText) {
      throw new TypeError(
        "'linkText' and 'linkType' aren't required except when you input one of them."
      );
    }
  }

  getInputType(): string {
    const visibleType = this.inputType == 'password' ? 'text' : this.inputType;
    return this.hideInputValue ? 'password' : visibleType;
  }
}
