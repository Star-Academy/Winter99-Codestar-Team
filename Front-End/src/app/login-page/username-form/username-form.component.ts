import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormValues } from '../models/formValues';

@Component({
  selector: 'app-username-form',
  template:
    '<app-single-input-form  [formValues]="getUsernameFormValues"></app-single-input-form>',
})
export class UsernameFormComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {}

  public readonly getUsernameFormValues: FormValues = new FormValues(
    'نام کاربری',
    'text',
    (username: string) => {
      console.log('getUsernameFormValues');
      console.log(username);
      localStorage.setItem('username', username);
      this.router.navigate(['login/password']);
    },
    [],
    'ایجاد حساب کاربری',
    '#'
  );
}
