import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: 'login.component.html'
})

export class LoginComponent {
    Form: FormGroup;
    //loading: false;
    // submitted = false;

    constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, /*private accountService: AccountService*/) { 
        this.Form = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        })
    }

    onSubmit() {
        this.Form;
        this.Form.value.username;
        this.Form.touched;

        console.log("Skjema:");
        console.log(this.Form);
        console.log(this.Form.value.username);
        console.log(this.Form.touched);
    }

    ngOnInit() {
    }
}