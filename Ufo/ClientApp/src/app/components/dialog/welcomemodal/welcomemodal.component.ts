import { Component, OnInit } from '@angular/core';
import { GeneralService } from 'src/app/services/general.service';

@Component({
  selector: 'welcomemodal',
  templateUrl: './welcomemodal.component.html',
  styleUrls: ['./welcomemodal.component.scss']
})
export class WelcomemodalComponent implements OnInit {

  constructor(public generalService: GeneralService) { }

  ngOnInit(): void {
  }

}