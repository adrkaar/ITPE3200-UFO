import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observation } from '../models/observation.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'editObservation',
    templateUrl: 'editObservation.component.html'
})

export class EditObservationComponent implements OnInit {
    editObservation: Observation = {
        id: 0,
        name: ' ',
        date: ' ',
        time: ' ',
        location: ' ',
        description: ' '
    }

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) { }

    ngOnInit(): void {
        // henter id fra parameter i url
        this.route.paramMap.subscribe(param => {
            const id = param.get('id');

            // skal bruke id for å hente riktig objekt
            if (id) {
                this.http.get<Observation>("api/observation/fetchOneObservation" + id)
                    .subscribe(data => {
                        this.editObservation = data;
                    })
            }
        })
    }

    updateObservation() {
        this.http.post<Observation>("api/observation/editObservation", this.editObservation)
            .subscribe(() => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }

    deleteObservation(id: number) {
        this.http.delete<Observation>("api/observation/deleteObservation" + id)
            .subscribe(() => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }
}