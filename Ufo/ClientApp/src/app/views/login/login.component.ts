import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../models/user.model';
import { LoggedInUser } from '../../models/LoggedInUser.component';

@Component({
    selector: 'login',
    templateUrl: 'login.component.html'
})

export class LoginComponent {
    Form: FormGroup;
    user: User = {
        id: '',
        username: '',
        password: '',
        token: ''
    }

    constructor(private http: HttpClient, private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router) {
        this.Form = this.formBuilder.group({
            username: [null,
                Validators.compose([
                    Validators.required,
                    Validators.pattern("^[a-zA-Z0-9]+$")
                ])],
            password: [null,
                Validators.compose([
                    Validators.required,
                    Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                ])]
        })
    }

    logIn() {
        this.http.post<User>('api/user/logIn', this.user)
            .subscribe(response => {
                if (response) this.router.navigate(['observation'])
                else alert("Wrong username or password, please try again");
            },
                error => console.log(error)
        );
    }
}