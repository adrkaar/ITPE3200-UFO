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
        this.route.paramMap.subscribe(response => {
            const id = response.get('id');

            if (id) {
                this.http.get<Observation>("api/ufo/fetchOneObservation" + id)
                    .subscribe(data => {
                        this.editObservation = data;
                    })
            }
        })
    }

    updateObservation() {
        this.http.post<Observation>("api/ufo/editObservation", this.editObservation)
            .subscribe( () => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }
}