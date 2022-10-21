import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observation } from '../models/observation.model';
import { ActivatedRoute, Router } from "@angular/router";
import { Comment } from "../models/comment.model"; 

@Component({
    selector: 'comment',
    templateUrl: 'comment.component.html'
})

export class CommentComponent {
    allcomments: Array<Comment>;
    id: any;

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) { }

    ngOnInit() {
        // henter id fra parameter i url
        this.route.paramMap.subscribe(param => {
            this.id = param.get('id');
        })

        this.fetchAllComments(this.id);
    }

    fetchAllComments(id: any) {
        this.http.get<Comment[]>('api/observation/fetchAllComments/' + id)
            .subscribe(response => {
                this.allcomments = response;
            },
                error => console.log(error)
            );
    }

    // må refrese siden for å se endringene
    deleteComment(id: number) {
        this.http.delete<Observation>("api/observation/deleteComment/" + id)
            .subscribe(() => {
                window.location.reload();
            },
                error => console.log(error)
            );
    }
}