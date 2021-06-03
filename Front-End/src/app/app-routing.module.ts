import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListViewPageComponent } from './list-view-page/list-view-page.component';
import { LoginPageComponent } from './login-page/login-page.component';

const routes: Routes = [
  {
    path: 'login',
    pathMatch: 'full',
    component: LoginPageComponent,
  },
  {
    path: 'listview',
    pathMatch: 'full',
    component: ListViewPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
