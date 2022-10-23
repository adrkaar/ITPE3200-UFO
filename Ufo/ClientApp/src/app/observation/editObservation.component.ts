import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observation } from '../models/observation.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AddObservationComponent } from './addObservation.component';
import { UfoType } from '../models/ufoType.model';

@Component({
    selector: 'editObservation',
    templateUrl: 'editObservation.component.html'
})

export class EditObservationComponent implements OnInit {
    editObservation: Observation = {
        id: 0,
        date: ' ',
        time: ' ',
        location: ' ',
        description: ' ',
        ufoType: ''
    }
    types: Array<UfoType>;

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) { }

    ngOnInit(): void {
        // henter id fra parameter i url
        this.route.paramMap.subscribe(param => {
            const id = param.get('id');

            // bruker id for å hente riktig objekt
            if (id) {
                this.http.get<Observation>("api/observation/fetchOneObservation/" + id)
                    .subscribe(data => {
                        this.editObservation = data;
                    })
            }
        })

        // henter ufo typer
        this.fetchUfoTypes();
    }


    chosenType: string;

    selectedOption(type: string) {
        this.chosenType = type;
    }

    updateObservation() {
        this.editObservation.ufoType = this.chosenType;
        this.http.post<Observation>("api/observation/editObservation", this.editObservation)
            .subscribe(() => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }

    deleteObservation(id: number) {
        this.http.delete<Observation>("api/observation/deleteObservation/" + id)
            .subscribe(() => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }

    fetchUfoTypes() {
        this.http.get<UfoType[]>('api/observation/fetchUfoTypes')
            .subscribe(response => {
                this.types = response; // typs blir undefined
            },
                error => console.log(error)
            );
    }
}