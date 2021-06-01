import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SiteNavigationMenuComponent } from './site-navigation-menu/site-navigation-menu.component';

@NgModule({
  declarations: [SiteNavigationMenuComponent],
  imports: [CommonModule],
  exports: [SiteNavigationMenuComponent],
})
export class SiteNavigationMenuModule {}
