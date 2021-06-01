import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-site-navigation-menu',
  templateUrl: './site-navigation-menu.component.html',
  styleUrls: ['./site-navigation-menu.component.scss'],
})
export class SiteNavigationMenuComponent implements OnInit {
  menuIsExpanded: boolean = false;
  @Output()
  theEvent = new EventEmitter<boolean>();
  constructor() {}

  someFunctionName() {
    this.menuIsExpanded = !this.menuIsExpanded;
    this.theEvent.emit(this.menuIsExpanded);
  }
  ngOnInit(): void {}
}
