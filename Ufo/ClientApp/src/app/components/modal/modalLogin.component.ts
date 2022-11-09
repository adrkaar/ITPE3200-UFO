import { Component} from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  templateUrl: 'modalLogin.component.html',
  styleUrls: ['modal.component.css']
})
export class ModalLogin {
  constructor(public modal: NgbActiveModal) { }
}