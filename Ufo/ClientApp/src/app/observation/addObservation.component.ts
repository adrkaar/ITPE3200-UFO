import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";
import { Observation } from '../models/observation.model';

@Component({
    selector: 'addObservation',
    templateUrl: 'addObservation.component.html'
})

export class AddObservationComponent {
    newObservation: Observation = {
        id: 0,
        name: ' ',
        date: ' ',
        time: ' ',
        location: ' ',
        description: ' '
    }

    constructor(private http: HttpClient, private router: Router) { }

    addObservation() {
        this.http.post<Observation>('api/observation/addObservation', this.newObservation)
            .subscribe(() => {
                console.log(this.newObservation)
                this.router.navigate(['observation'])
                console.log(this.newObservation)
            },
                error => console.log(error)
            );
    }
}