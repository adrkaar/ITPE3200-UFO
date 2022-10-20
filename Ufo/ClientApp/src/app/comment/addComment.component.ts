import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Comment } from "../models/comment.model"; 
import { CommentComponent } from "./comment.component"; 

@Component({
    selector: 'addComment',
    templateUrl: 'addComment.component.html'
})

export class AddCommentComponent {
    newComment: Comment = {
        id: 0,
        text: '',
        observationId: 0
    }
   

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) { }

    hei: any;
    ngOnInit(): void {
        // henter id fra parameter i url
        this.route.paramMap.subscribe(param => {
            this.hei = param.get('id');
        })
    }

    testComment: Comment = {
        id: 1,
        text: 'sdfgb',
        observationId: 1
    }

    addComment() {
        this.http.post<Comment>('api/observation/addComment', this.newComment) // må endre test til new
            .subscribe(response => {
                console.log(response)
                //this.router.navigate(['/comment'] + this.hei);
            },
                error => console.log(error)
            );
    }
}