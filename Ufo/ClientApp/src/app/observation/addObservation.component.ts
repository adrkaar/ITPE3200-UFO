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
    types: Array<UfoType>;
    addNewType: string;

    newObservation: Observation = {
        id: 0,
        date: ' ',
        time: ' ',
        location: ' ',
        description: ' ',
        ufoType: ' '
    }

    ngOnInit() {
        this.fetchUfoTypes();
    }

    constructor(private http: HttpClient, private router: Router) { }

    // hvis brukeren velger "add new type" kommer det opp et inputfelt hvor de kan legge til typen
    selectedOption(type: string) {
        this.chosenType = type;
        if (this.chosenType === 'Add new type') {
            this.addNewType = '<label for="newType">Add new type</label> <input type="text" class="form-control" id="newType" name="newType" [(ngModel)]="newType"/> ';
        }
    }

    addObservation() {
        // sjekker om brukeren vil legge til ny type
        if (this.chosenType === 'Add new type') {
            // henter verdien til ny type
            this.chosenType = (<HTMLInputElement>document.getElementById('newType')).value;
        }
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