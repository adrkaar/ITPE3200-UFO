import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Comment } from "../models/comment.model"; 

@Component({
    selector: 'addComment',
    templateUrl: 'addComment.component.html'
})

export class AddComment {
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