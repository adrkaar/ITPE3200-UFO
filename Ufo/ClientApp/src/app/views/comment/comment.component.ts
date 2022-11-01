import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from "@angular/router";
import { Comment } from "../../models/comment.model"; 

@Component({
    selector: 'comment',
    templateUrl: 'comment.component.html'
})

export class CommentComponent {
    allcomments: Array<Comment>;
    observationId: any;

    constructor(private http: HttpClient, private route: ActivatedRoute) { }

    ngOnInit() {
        // henter id fra parameter i url
        this.route.paramMap.subscribe(param => {
            this.observationId = param.get('id');
        })

        this.fetchAllComments(this.observationId);
    }

    refreshWebsite(): void {
        window.location.reload()
    }

    // føler ikke egt frontend brude sende med id, føler det er riktigere å gjøre det i backend??
    fetchAllComments(id: any) {
        this.http.get<Comment[]>('api/comment/fetchAllComments/' + id)
            .subscribe(response => {
                this.allcomments = response;
            },
                error => console.log(error)
            );
    }

    deleteComment(id: number) {
        this.http.delete<Comment>("api/comment/deleteComment/" + id)
            .subscribe(() => {
                this.fetchAllComments(this.observationId);
            },
                error => console.log(error)
            );
    }

    upVote(id: number) {
        this.http.get<Comment>("api/comment/upVote/" + id)
            .subscribe(() => {
                this.fetchAllComments(this.observationId);
            },
                error => console.log(error)
            );
    }

    downVote(id: number) {
        this.http.get<Comment>("api/comment/downVote/" + id)
            .subscribe(() => {
                this.fetchAllComments(this.observationId);
            },
                error => console.log(error)
            );
    }
}