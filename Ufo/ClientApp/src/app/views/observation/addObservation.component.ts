import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";
import { FormGroup, FormControl, FormBuilder, Validators } from "@angular/forms";
import { Observation } from '../../models/observation.model';
import { UfoType } from '../../models/ufoType.model';
import { User } from 'oidc-client';
import { waitForAsync } from '@angular/core/testing';
import { emitDistinctChangesOnlyDefaultValue } from '@angular/compiler';

@Component({
    templateUrl: 'addObservation.component.html'
})

export class AddObservationComponent {
    AddObservationForm: FormGroup;

    latitude: number;
    chosenType: string;
    types: Array<UfoType>;
    addNewType: string;
    addLatLngViaMap;

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


    marker: google.maps.Marker;
    map: google.maps.Map;

    // https://www.youtube.com/watch?v=orjkt0VHt1c
    // kan ta tid å få svar
    fetchMyCoords() {
        if (!navigator.geolocation) {
            console.log("geolocations not supported")
        }
        // henter brukeres geo locasjon og fyller ut det for dem med max 10 tegn
        navigator.geolocation.getCurrentPosition((position) => {
            this.newObservation.latitude = String(position.coords.latitude).substring(0,10);
            this.newObservation.longitude = String(position.coords.longitude).substring(0, 10);

            // sette markøren
            var ltlg = { lat: position.coords.latitude, lng: position.coords.longitude };
            // gir feilmelding hvis fetchmycoord blir kalt før addmap
            this.marker.setPosition(ltlg);
        }, error => console.log(error)
        );
    }

    addMap() {
        var center: google.maps.LatLngLiteral = { lat: 12, lng: 12 };

        // åpner kartet
        this.map = new google.maps.Map(document.getElementById("map") as HTMLElement, {
            zoom: 3,
            center: center
        });

        // setter ut markør
        this.marker = new google.maps.Marker({
            position: center,
            map: this.map
        });

        // flytter på markøren dit man trykker på kartet
        google.maps.event.addListener(this.map, 'click', (event) => {
            this.marker.setPosition(event.latLng);

            // setter latitude og longitude inputfeltene til verdien til markøren
            this.newObservation.latitude = event.latLng.lat().toFixed(5);
            this.newObservation.longitude = event.latLng.lng().toFixed(5);
        });
    }

    // er jo ikke så farlig, kan droppes å kunne lukke den
    //closeMap() {
    //    // fjerne kart
    //    console.log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    //    this.map = null;
    //    delete (this.map);

    //    (<HTMLInputElement>document.getElementById("map")).value = null;
    //    //console.log((<HTMLInputElement>document.getElementById("map")).value);
    //}
}