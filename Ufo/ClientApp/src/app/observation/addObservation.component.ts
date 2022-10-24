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

    addNewType: string;

    selectedOption(type: string) {
        this.chosenType = type;
        if (this.chosenType === "Add new type") {
            console.log("!!!!!!!!!!!!!!!!") // funker
            this.addNewType = '<label for="newType">Add new type</label> <input type="text" class="form-control" id="newType" name="newType"/> '; // vil ikke input pga sikkerhet
        }
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

    // har hent ufotype i både edit og add...
    fetchUfoTypes() {
        this.http.get<UfoType[]>('api/observation/fetchUfoTypes')
            .subscribe(response => {
                this.types = response;
            },
                error => console.log(error)
            );
    }
}