import { Component, OnInit } from '@angular/core';
import { FormValues } from '../models/formValues';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  public readonly getUsernameFormValues: FormValues = new FormValues(
    'نام کاربری',
    'text',
    (username: string) => {
      console.log('getUsernameFormValues');
      console.log(username);
      localStorage.setItem('username', username);
    },
    [],
    'ایجاد حساب کاربری',
    '#'
  );

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
