import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observation } from '../../models/observation.model';
import { Router } from '@angular/router';
import { GeneralService } from 'src/app/services/general.service';

@Component({
    templateUrl: 'observation.component.html'
})

export class ObservationComponent {
    allObservations: Array<Observation>;

    constructor(private http: HttpClient, private router: Router, public generalService: GeneralService) { }

    ngOnInit() {
        this.fetchAllObservations();
    }
    
    fetchAllObservations() {
        this.http.get<Observation[]>('api/observation/fetchAllObservations')
            .subscribe(response => {
                this.allObservations = response;
            },
                error => console.log(error)
            );
    }

    openMapwithLocation(latitude: number, longitude: number) {
    }

    checkLogIn() {
        // sjekker om brukeren er logget inn
        this.http.get<boolean>('api/user/checkLogIn')
            .subscribe(response => {
                if (response) {
                    this.router.navigate(['addObservation'])
                }
                else {
                    this.generalService.showNotLoggedInDialog = true;
                }
            }, error => console.log(error)
            );
    }

    RouteEditObservation(observationId: number) {
        // sjekker om brukeren er logget inn
        this.http.get<boolean>('api/user/checkLogIn')
            .subscribe(response => {
                if (response) {
                    this.router.navigate(['editObservation', observationId])
                }
                else {
                    this.generalService.showNotLoggedInDialog = true;
                }
            }, error => console.log(error)
            );
    }
}