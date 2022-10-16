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
        this.http.post<Observation>('api/ufo/addObservation', this.newObservation)
            .subscribe(() => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }
}