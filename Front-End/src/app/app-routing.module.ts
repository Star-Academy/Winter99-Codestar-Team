import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddTransactionPageComponent } from './add-transaction-page/add-transaction-page.component';
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
  {
    path: 'add-transaction',
    pathMatch: 'full',
    component: AddTransactionPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
