import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalLogin } from 'src/app/components/modal/modalLogin.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css', './nav-menu.component.scss']
})

export class NavMenuComponent {
  /* isExpanded = false;
  user: any; */
  modal: NgbModal | undefined;
  collapse = false;
  navData = navbarData;

  toggleSideNav() {
    this.collapse = !this.collapse;
  }

  constructor(private modalService: NgbModal) { }

  showLoginForm() {
    this.modalService.open(ModalLogin, {
        backdrop: 'static',
        keyboard: true, 
        centered: true, 
        size: 'lg'
    });
    this.modal.open(ModalLogin);

    /*
    modalRef.result.then(retur => {
      console.log('Closed with: ' + retur);
    }); */
  }

  /*
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  } */
}
