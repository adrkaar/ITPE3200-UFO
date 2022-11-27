import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactMessage } from '../../models/contactMessage.model';

@Component({
    templateUrl: 'contact.component.html',
})

export class ContactComponent {
    ContactForm: FormGroup;

    newContactMessage: ContactMessage = {
        name: '',
        email: '',
        message: ''
    };

    validation = {
        name: [
            null,
            Validators.compose([
                Validators.required,
                Validators.pattern("^[a-zA-Z .,-?!]{1,200}$")
            ])
        ],
        email: [
            null,
            Validators.compose([
                Validators.required,
                Validators.pattern("^[a-zA-Z .,-?!@]{1,200}$")
            ])
        ],
        message: [
            null,
            Validators.compose([
                Validators.required,
                Validators.pattern("^[a-zA-Z .,-?!]{1,200}$")
            ])
        ]
    }

    ngOnInit(): void {
    }

    constructor(private http: HttpClient, private router: Router, private formBuilder: FormBuilder) {
        this.ContactForm = formBuilder.group(this.validation);
    }

    contact() {
        this.ContactForm.reset();
        alert("Thanks for contacting us!");

        this.http.post<ContactMessage>('api/contact/handleContact', this.newContactMessage)
            .subscribe(() => {
                this.router.navigate(['contact'])
            },
                error => console.log(error)
            );
    }
}