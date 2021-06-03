import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormValues } from './models/formValues';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent implements OnInit {
  public formName = 'username';

  public readonly getUsernameFormValues: FormValues = new FormValues(
    'نام کاربری',
    'text',
    (username: string) => {
      localStorage.setItem('username', username);
      this.formName = 'password';
    },
    [],
    'ایجاد حساب کاربری',
    '#'
  );
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

  ngOnInit(): void {}
}
