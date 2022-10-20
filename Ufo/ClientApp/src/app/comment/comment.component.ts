import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observation } from '../models/observation.model';
import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Comment } from "../models/comment.model"; 

@Component({
    selector: 'comment',
    templateUrl: 'comment.component.html'
})

export class CommentComponent {
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

    newComment: Comment = {
        id: 0,
        text: '',
        observationId: 0 // vet ikke hvordan få den enda
    }

    constructor(private http: HttpClient, private router: Router) { }

    testComment: Comment = {
        id: 1,
        text: 'sdfgb',
        observationId: 1
    }

    addComment() {
        this.http.post<Comment>('api/observation/addComment', this.testComment) // må endre test til new
            .subscribe(() => {
                this.router.navigate(['comment'])
            },
                error => console.log(error)
            );
    }
}