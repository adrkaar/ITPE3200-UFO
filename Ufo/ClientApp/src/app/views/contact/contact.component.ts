import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactMessage } from '../../models/contactMessage.model';
import { DomSanitizer } from '@angular/platform-browser';
import { GeneralService } from '../../services/general.service';

@Component({
    templateUrl: 'contact.component.html',
})

export class ContactComponent {
    ContactForm: FormGroup;

    newContactMessage: ContactMessage = {
        subject: '',
        email: '',
        message: ''
    };

    validation = {
        subject: [
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

    constructor(private http: HttpClient, private router: Router, private formBuilder: FormBuilder, private sanitizer: DomSanitizer, public generalService: GeneralService) { this.ContactForm = formBuilder.group(this.validation); }

    mailtoHeader = "mailto:webapp2022ufo@gmail.com?";
    subjectProp = "subject=";
    bodyProp = "body=";

    contact() {
        /* https://stackblitz.com/edit/mailto-links-for-assets?file=src%2Fapp%2Fmailto-example%2Fmailto-example.component.ts */
        const url = `${this.mailtoHeader}${this.subjectProp}${this.newContactMessage.subject}&${this.bodyProp}${this.newContactMessage.message}`;
        return this.sanitizer.bypassSecurityTrustUrl(url)

        this.http.post<ContactMessage>('api/contact/handleContact', this.newContactMessage)
            .subscribe(() => {
                this.router.navigate(['contact'])
            },
                error => console.log(error)
            );
    }
}