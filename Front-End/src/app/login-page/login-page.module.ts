import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { LoginPageRoutingModule } from './login-page-routing.module';
import { LoginPageComponent } from './login-page.component';
import { SingleInputFormComponent } from './single-input-form/single-input-form.component';
import { UsernameFormComponent } from './username-form/username-form.component';
import { PasswordFormComponent } from './password-form/password-form.component';

@NgModule({
  declarations: [LoginPageComponent, SingleInputFormComponent, UsernameFormComponent, PasswordFormComponent],
  imports: [
    CommonModule,
    FormsModule,
    LoginPageRoutingModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  exports: [LoginPageComponent],
})
export class LoginPageModule {}
