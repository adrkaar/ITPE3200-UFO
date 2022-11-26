import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observation } from '../../models/observation.model';
import { Router } from '@angular/router';

@Component({
    templateUrl: 'observation.component.html'
})

export class ObservationComponent {
    allObservations: Array<Observation>;

    constructor(private http: HttpClient, private router: Router) { }

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

    RouteEditObservation(observationId: number) {
        // sjekker om brukeren er logget inn
        this.http.get<boolean>('api/user/checkLogIn')
            .subscribe(response => {
                if (response) {
                    this.router.navigate(['editObservation', observationId])
                }
                else {
                    alert("You have to log in");
                }
            }, error => console.log(error)
            );
    }
}