import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PasswordFormComponent } from './password-form/password-form.component';
import { UsernameFormComponent } from './username-form/username-form.component';

const routes: Routes = [
  { path: '', component: UsernameFormComponent },
  { path: 'password', component: PasswordFormComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LoginPageRoutingModule {}
