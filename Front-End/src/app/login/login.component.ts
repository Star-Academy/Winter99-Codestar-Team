import { Component, OnInit } from '@angular/core';
import { FormValues } from './single-input-form/models/formValues';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
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
