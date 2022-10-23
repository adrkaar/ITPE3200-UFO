import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";
import { Observation } from '../models/observation.model';
import { UfoType } from '../models/ufoType.model';

@Component({
    selector: 'addObservation',
    templateUrl: 'addObservation.component.html'
})

export class AddObservationComponent {
    chosenType: string;

    newObservation: Observation = {
        id: 0,
        date: ' ',
        time: ' ',
        location: ' ',
        description: ' ',
        ufoType: ' '
    }

    types: Array<UfoType>;

    ngOnInit() {
        this.fetchUfoTypes();
    }

    constructor(private http: HttpClient, private router: Router) { }

    selectedOption(type: string) {
        this.chosenType = type;
    }

    addObservation() {
        this.newObservation.ufoType = this.chosenType;
        this.http.post<Observation>('api/observation/addObservation', this.newObservation)
            .subscribe(() => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }

    fetchUfoTypes() {
        this.http.get<UfoType[]>('api/observation/fetchUfoTypes')
            .subscribe(response => {
                this.types = response;
            },
                error => console.log(error)
            );
    }
}