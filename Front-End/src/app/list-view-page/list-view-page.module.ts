import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilterMenuComponent } from './filter-menu/filter-menu.component';
import { TransactionsListComponent } from './transactions-list/transactions-list.component';
import { ListViewPageComponent } from './list-view-page/list-view-page.component';
import { SiteNavigationMenuModule } from '../site-navigation-menu/site-navigation-menu.module';

@NgModule({
  declarations: [
    FilterMenuComponent,
    TransactionsListComponent,
    ListViewPageComponent,
  ],
  imports: [CommonModule, SiteNavigationMenuModule],
  exports: [ListViewPageComponent],
})
export class ListViewPageModule {}
