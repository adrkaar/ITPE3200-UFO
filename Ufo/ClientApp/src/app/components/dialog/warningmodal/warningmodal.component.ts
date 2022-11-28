import { Component, OnInit } from '@angular/core';
import { GeneralService } from 'src/app/services/general.service';

@Component({
  selector: 'app-warningmodal',
  templateUrl: './warningmodal.component.html',
  styleUrls: ['./warningmodal.component.scss']
})
export class WarningmodalComponent implements OnInit {

  constructor(public generalService: GeneralService) { }

  ngOnInit(): void {
  }

}