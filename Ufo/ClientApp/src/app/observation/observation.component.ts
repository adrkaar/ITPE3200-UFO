import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observation } from '../models/observation.model';

@Component({
    selector: 'observation',
    templateUrl: 'observation.component.html'
})

export class ObservationComponent {
    allObservations: Array<Observation>;

    constructor(private http: HttpClient) { }

    ngOnInit() {
        this.fetchAllObservations();
    }

    fetchAllObservations() {
        this.http.get<Observation[]>('api/observation/fetchAllObservations')
            .subscribe(response => {
                console.log(response)
                this.allObservations = response;
                console.log("YES")
            },
                error => console.log(error)
            );
    }
}