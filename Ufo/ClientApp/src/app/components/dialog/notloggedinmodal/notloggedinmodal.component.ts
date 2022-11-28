import { Component, OnInit } from '@angular/core';
import { GeneralService } from 'src/app/services/general.service';

@Component({
    selector: 'notloggedinmodal',
    templateUrl: './notloggedinmodal.component.html',
    styleUrls: ['./notloggedinmodal.component.scss']
})
export class NotloggedinmodalComponent implements OnInit {
    constructor(public generalService: GeneralService) { }

    ngOnInit(): void { }
}