import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GeneralService } from 'src/app/services/general.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent {

    constructor(private http: HttpClient, private router: Router) {
    }

  constructor(public generalService: GeneralService) { }

    checkLogIn() {
        this.http.get<boolean>('api/user/checkLogIn')
            .subscribe(response => {
                if (response) {
                    this.router.navigate(['addObservation'])
                }
                else {
                    alert("You have to log in");
                }
            }, error => console.log(error)
            );
    }

    logOut() {
        this.http.post<boolean>('api/user/logOut', "")
            .subscribe(() => {
                alert("You have been logged out");
            }, error => console.log(error)
            );
    }
}