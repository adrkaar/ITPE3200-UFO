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
        console.log(this.editObservation);

        // skal hente id via url, i observation html blir iden lagt med i rouring til edit
        this.route.paramMap.subscribe(param => {
            console.log(param)
            const id = param.get('id');
            console.log("id: ", id) // id er null, klarer ikke hente id

            // skal bruke id for å hente objeket med iden
            if (id) {
                console.log("HER")
                this.http.get<Observation>("api/observation/fetchOneObservation" + id)
                    .subscribe(data => {
                        console.log("HergER")
                        console.log(data)
                        this.editObservation = data;
                        console.log(this.editObservation)
                    })
            }
        })
    }

    updateObservation() {
        this.http.post<Observation>("api/observation/editObservation", this.editObservation)
            .subscribe( () => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }


    // gjør det den skal, men gir feilmeldinger pga ngOnInit som kaller fetch one
    deleteObservation(id: number) {
        this.http.delete<Observation>("api/observation/deleteObservation" + id)
            .subscribe( () => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }
}