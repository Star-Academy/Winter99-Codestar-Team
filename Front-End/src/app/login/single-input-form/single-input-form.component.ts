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
  inputName: string;
  @Input()
  inputType: string;
  @Input()
  inputValidators: ValidatorFn[] = [];
  @Input()
  linkText: string;
  @Input()
  linkHref: string;
  @Output()
  inputValue: string;
  inputControl: FormControl;

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
}
