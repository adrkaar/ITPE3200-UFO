import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Comment } from "../models/comment.model"; 

@Component({
    selector: 'addComment',
    templateUrl: 'addComment.component.html'
})

export class AddCommentComponent {
    newComment: Comment = {
        id: 0,
        text: '',
        observationId: 0,
        upVote: 0,
        downVote: 0
    }

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) { }

    obsId: number;
    ngOnInit(): void {
        // henter id fra parameter i url
        this.route.paramMap.subscribe(param => {
            this.obsId = Number(param.get('id'));
        })
    }

    addComment() {
        // setter riktig observationId før objektet sendes
        this.newComment.observationId = this.obsId;

        this.http.post<Comment>('api/comment/addComment', this.newComment)
            .subscribe(() => {
                this.router.navigate(['/comment', this.obsId]);
            },
                error => console.log(error)
            );
    }
}