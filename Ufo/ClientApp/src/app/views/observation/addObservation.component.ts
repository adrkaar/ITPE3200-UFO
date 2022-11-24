import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";
import { FormGroup, FormControl, FormBuilder, Validators } from "@angular/forms";
import { Observation } from '../../models/observation.model';
import { UfoType } from '../../models/ufoType.model';

@Component({
    templateUrl: 'addObservation.component.html'
})

export class AddObservationComponent {
    AddObservationForm: FormGroup;

    chosenType: string;
    types: Array<UfoType>;
    addNewType: string;

    maxDate;
    newObservation: Observation = {
        id: 0,
        date: ' ',
        time: ' ',
        latitude: '',
        longitude: '',
        description: '',
        ufoType: ' '
    }

    validation = {
        id: [""],
        date: [
            null,
            Validators.compose([
                Validators.required,
                Validators.pattern("\\d{4}-\\d{2}-\\d{2}")
            ])
        ],
        time: [
            null,
            Validators.compose([
                Validators.required,
                Validators.pattern("([0-1]?[0-9]|2[0-3]):[0-5][0-9]")
            ])
        ],
        latitude: [
            null,
            Validators.compose([
                Validators.required,
                Validators.pattern("^[0-9.-]{1,10}$")
            ])
        ],
        longitude: [
            null,
            Validators.compose([
                Validators.required,
                Validators.pattern("^[0-9.-]{1,10}$")
            ])
        ],
        description: [
            null,
            Validators.compose([
                Validators.required,
                Validators.pattern("^[a-zA-Z .,-?!]{1,200}$")
            ])
        ],
        UfoType: [""]
    }

    constructor(private http: HttpClient, private router: Router, private formBuilder: FormBuilder) {
        this.AddObservationForm = formBuilder.group(this.validation);
    }

    ngOnInit() {
        // gjør slik at man ikke kan velge dato fram i tid
        this.maxDate = new Date().toISOString().slice(0, 10);

        this.fetchUfoTypes();
    }

    // hvis brukeren velger "add new type" dukker det opp et inputfelt hvor de kan legge til typen
    selectedOption(type: string) {
        this.chosenType = type;
        if (type === 'Add new type') {
            this.addNewType = '<label for="newType" style="color: black">Add new type</label> <input type="text" class="form-control" id="newType" name="newType" [(ngModel)]="newType" style="color: black"/>';
        }
        else {
            this.addNewType = " ";
        }
    }

    addObservation() {
        // sjekker om brukeren vil legge til ny type
        if (this.chosenType === 'Add new type') {
            // henter verdien til ny type
            this.chosenType = (<HTMLInputElement>document.getElementById('newType')).value;
        }
        this.newObservation.ufoType = this.chosenType;
        this.http.post<Observation>('api/observation/addObservation', this.newObservation)
            .subscribe(() => {
                this.router.navigate(['observation'])
            },
                error => console.log(error)
            );
    }

    fetchUfoTypes() {
        this.http.get<UfoType[]>('api/observation/fetchUfoTypes')
            .subscribe(response => {
                this.types = response;
            },
                error => console.log(error)
            );
    }
}