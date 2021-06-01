import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { LoginPageComponent } from './login-page/login-page.component';
import { SingleInputFormComponent } from './single-input-form/single-input-form.component';

@NgModule({
  declarations: [LoginPageComponent, SingleInputFormComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    MatIconModule,
  ],
  exports: [LoginPageComponent],
})
export class LoginPageModule {}
