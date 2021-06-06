import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSortModule } from '@angular/material/sort';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CookieService } from 'ngx-cookie-service';
import { LoginPageComponent } from './login-page/login-page.component';
import { SingleInputFormComponent } from './login-page/single-input-form/single-input-form.component';
import { ListViewPageComponent } from './list-view-page/list-view-page.component';
import { TransactionsListComponent } from './list-view-page/transactions-list/transactions-list.component';
import { FilterMenuComponent } from './list-view-page/filter-menu/filter-menu.component';
import { FiltersListComponent } from './list-view-page/filter-menu/filters-list/filters-list.component';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { from } from 'rxjs';
import { PageWithNavigationMenuComponent } from './page-with-navigation-menu/page-with-navigation-menu.component';
import { AddTransactionPageComponent } from './add-transaction-page/add-transaction-page.component';

@NgModule({
  declarations: [
    AppComponent,
    AddTransactionPageComponent,
    SingleInputFormComponent,
    LoginPageComponent,
    TransactionsListComponent,
    ListViewPageComponent,
    FilterMenuComponent,
    FiltersListComponent,
    PageWithNavigationMenuComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    MatButtonModule,
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSidenavModule,
    MatSortModule,
    MatTableModule,
    NgScrollbarModule,
    ReactiveFormsModule,
  ],
  providers: [CookieService],
  bootstrap: [AppComponent],
})
export class AppModule {}
