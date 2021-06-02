import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-list-view-page',
  templateUrl: './list-view-page.component.html',
  styleUrls: ['./list-view-page.component.scss'],
})
export class ListViewPageComponent implements OnInit {
  rightMenuClasses: string[] = ['filter-menu'];
  contentClasses: string[] = ['content'];
  leftMenuClasses: string[] = ['navigation-menu'];
  containerClasses: string[] = [];
  constructor() {}

  ngOnInit(): void {}

  RightMenuToggle(value: boolean) {
    if (value) {
      this.containerClasses.push('expanded-right-menu');
    } else {
      this.containerClasses = this.containerClasses.filter(
        (obj) => obj !== 'expanded-right-menu'
      );
    }
  }
}
