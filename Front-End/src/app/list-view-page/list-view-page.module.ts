import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SiteNavigationMenuModule } from '../site-navigation-menu/site-navigation-menu.module';
import { ListViewPageComponent } from './list-view-page.component';
import { FilterMenuComponent } from './filter-menu/filter-menu.component';
import { TransactionsListComponent } from './transactions-list/transactions-list.component';
import { ListViewPageRoutingModule } from './list-view-page-routing.module';

@NgModule({
  declarations: [
    TransactionsListComponent,
    ListViewPageComponent,
    FilterMenuComponent,
  ],
  imports: [CommonModule, SiteNavigationMenuModule, ListViewPageRoutingModule],
})
export class ListViewPageModule {}
