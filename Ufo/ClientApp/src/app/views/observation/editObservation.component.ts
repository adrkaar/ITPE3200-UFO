﻿import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observation } from '../../models/observation.model';
import { ActivatedRoute, Router } from '@angular/router';
import { UfoType } from '../../models/ufoType.model';

@Component({
    selector: 'editObservation',
    templateUrl: 'editObservation.component.html'
})

export class EditObservationComponent implements OnInit {
    editObservation: Observation = {
        id: 0,
        date: ' ',
        time: ' ',
        latitude: ' ',
        longitude: ' ',
        description: ' ',
        ufoType: ''
    }
    types: Array<UfoType>;
    chosenType: string;
    date;
    addNewType: string;

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

        // gjør slik at man ikke kan velge dato fram i tid
        this.date = new Date().toISOString().slice(0, 10);
    }

    // hvis brukeren velger "add new type" dukker det opp et inputfelt hvor de kan legge til typen
    selectedOption(type: string) {
        this.chosenType = type;
        if (type === 'Add new type') {
            this.addNewType = '<label for="newType" style="color: black">Add new type</label> <input type="text" class="form-control" id="newType" name="newType" [(ngModel)]="newType" style="color: black"/>';
        }
        else {
            this.addNewType = " ";
        }
    }

    updateObservation() {
        // sjekker om brukeren vil legge til ny type
        if (this.chosenType === 'Add new type') {
            // henter verdien til ny type
            this.chosenType = (<HTMLInputElement>document.getElementById('newType')).value;
        }
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