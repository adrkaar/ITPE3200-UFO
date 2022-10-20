import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observation } from '../models/observation.model';
import { ActivatedRoute, Router } from "@angular/router";
import { Comment } from "../models/comment.model"; 

@Component({
    selector: 'comment',
    templateUrl: 'comment.component.html'
})

export class CommentComponent {

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) { }

  

    /*
    allObservations: Array<Observation>;

    constructor(private http: HttpClient) { }

    ngOnInit() {
        this.fetchAllObservations();
    }

    fetchAllObservations() {
        this.http.get<Observation[]>('api/observation/fetchAllObservations')
            .subscribe(response => {
                this.allObservations = response;
            },
                error => console.log(error)
            );
    }
    */

    deleteObservation(id: number) {
        this.http.delete<Observation>("api/observation/deleteObservation" + id)
            .subscribe(() => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }
}