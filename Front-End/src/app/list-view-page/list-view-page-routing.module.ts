import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListViewPageComponent } from './list-view-page.component';

const routes: Routes = [{ path: '', component: ListViewPageComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ListViewPageRoutingModule {}
