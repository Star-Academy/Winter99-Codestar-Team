import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormValues } from '../models/formValues';

@Component({
  selector: 'app-password-form',
  template:
    '<app-single-input-form [formValues]="getPasswordFormValues"></app-single-input-form>',
})
export class PasswordFormComponent implements OnInit {
  public readonly getPasswordFormValues: FormValues = new FormValues(
    'رمز عبور',
    'password',
    (password: string) => {
      console.log('PasswordFormComponent submit works.');
      this.router.navigate(['listview']);
    },
    []
    // 'فراموشی رمز عبور',
    // '#'
  );

  constructor(private router: Router) {}

  ngOnInit(): void {
    console.log(localStorage.getItem('username'));
  }
}
