import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalLogin } from 'src/app/components/modal/modalLogin.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent {
  /* isExpanded = false;
  user: any; */

  constructor(private modalService: NgbModal) { }

  showLoginForm() {
    const modalRef = this.modalService.open(ModalLogin, {
        backdrop: 'static',
        keyboard: false, 
        centered: true, 
        size: 'lg'
    });  

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
