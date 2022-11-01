import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observation } from '../../models/observation.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {
    zoom: number = 3;
    center: google.maps.LatLngLiteral = { lat: 48.647983479154824, lng: 9.865054057063944 }
    markerPositions: google.maps.LatLngLiteral[] = [];
    latLng: google.maps.LatLngLiteral;

    constructor(private http: HttpClient) { }

    ngOnInit(): void {
        this.fetchAllLocations();
    }

    // henter alle observasjonene som kun inneholder lokasjonen
    fetchAllLocations() {
        this.http.get<Observation[]>('api/observation/fetchAllLocations')
            .subscribe(response => {

                // legger til lokasjonene p� kartet
                this.addToMap(response);
            },
                error => console.log(error)
            );
    }

    // legger til lokasjonene p� kartet
    addToMap(observations: Observation[]) {
        for (let i = 0; i <= observations.length; i++) {
            // henter ut lengde og breddegrad fra objektet
            let latitude = observations[i].latitude;
            let longitude = observations[i].longitude;

            // legger til lengde og bredde grad i arrray med mark�rer
            this.markerPositions.push(this.latLng = { lat: Number(latitude), lng: Number(longitude) });
        }
    }

    markerOptions: google.maps.MarkerOptions = {
        draggable: false
    };
}
