import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from "@angular/router";
import { Comment } from "../../models/comment.model"; 
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GeneralService } from 'src/app/services/general.service';

@Component({
    templateUrl: 'comment.component.html'
})

export class CommentComponent {
    allcomments: Array<Comment>;
    observationId: any;

    CommentForm: FormGroup;
    newComment: Comment = {
        id: 0,
        text: '',
        observationId: 0,
        upVote: 0,
        downVote: 0
    }

    validation = {
        id: [""],
        comment: [
            null,
            Validators.compose([
                Validators.required,
                Validators.pattern("^[a-zA-Z .,?!]{1,200}$")
            ])
        ],
    }

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder, public generalService: GeneralService) {
        this.CommentForm = formBuilder.group(this.validation);
    }

    ngOnInit() {
        // henter id fra parameter i url
        this.route.paramMap.subscribe(param => {
            this.observationId = Number(param.get('id'));
        })

        this.fetchAllComments(this.observationId);
    }

    fetchAllComments(id: any) {
        this.http.get<Comment[]>('api/comment/fetchAllComments/' + id)
            .subscribe(response => {
                this.allcomments = response;
            },
                error => console.log(error)
            );
    }

    addComment() {
        // sjekker om brukeren er logget inn
        this.http.get<boolean>('api/user/checkLogIn')
            .subscribe(response => {
                if (response) {
                    // setter riktig observationId før objektet sendes
                    this.newComment.observationId = this.observationId;

                    this.http.post<Comment>('api/comment/addComment/', this.newComment)
                        .subscribe(() => {
                            this.fetchAllComments(this.observationId);
                        },
                            error => console.log(error)
                        );
                }
                else {
                    this.generalService.showNotLoggedInDialog = true;
                }
            }, error => console.log(error)
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