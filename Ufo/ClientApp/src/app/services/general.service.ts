import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GeneralService {
  showDialog = false;
  showWarningDialog = false;
  constructor() { }
}
