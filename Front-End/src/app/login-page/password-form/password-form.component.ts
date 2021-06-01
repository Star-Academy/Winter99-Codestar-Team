import { Component, OnInit } from '@angular/core';
import { FormValues } from '../models/formValues';

@Component({
  selector: 'app-password-form',
  template:
    '<app-single-input-form [formValues]="getPasswordFormValues"></app-single-input-form>',
})
export class PasswordFormComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
  public readonly getPasswordFormValues: FormValues = new FormValues(
    'رمز عبور',
    'password',
    (password: string) => {
      console.log('getPasswordFormValues');
      console.log(password);
      localStorage.getItem('username');
    },
    [],
    'فراموشی رمز عبور',
    '#'
  );
}
