import { Component } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class LoginService {
    Url: string; 
    token: string;
    header: any;


    constructor(private http: HttpClient) { 
        this.Url = 'http://localhost:5000';
        const headerSetting: { [name: string]: string | string[]; } = {};
        this.header = new HttpHeaders(headerSetting);
    }

    ngOnInit() {
    }
}