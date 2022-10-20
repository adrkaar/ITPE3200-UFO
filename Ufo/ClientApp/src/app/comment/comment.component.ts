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
    allcomments: Array<Comment>;

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) { }

    ngOnInit() {
        this.fetchAllComments();
    }

    fetchAllComments() {
        this.http.get<Comment[]>('api/observation/fetchAllComments')
            .subscribe(response => {
                console.log(response)
                this.allcomments = response;
            },
                error => console.log(error)
            );
    }

    deleteComment(id: number) {
        this.http.delete<Observation>("api/observation/deleteComment" + id)
            .subscribe(response => {
                console.log(response)
                //this.router.navigate(['comment'])
            },
                error => console.log(error)
            );
    }
}