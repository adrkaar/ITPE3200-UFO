import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css', './nav-menu.component.scss']
})

export class NavMenuComponent {

  collapse = false;

  toggleSideNav() {
    this.collapse = !this.collapse;
  }

  constructor() { }
}
