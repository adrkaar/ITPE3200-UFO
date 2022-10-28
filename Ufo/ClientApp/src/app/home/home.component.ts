import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observation } from '../models/observation.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {
    zoom: number = 5;
    center: google.maps.LatLngLiteral = { lat: 48.647983479154824, lng: 9.865054057063944 }

    allObservations: any = [];

    constructor(private http: HttpClient) { }

    ngOnInit(): void {
        this.fetchAllLocations();
    }

    fetchAllLocations() {
        this.http.get<Observation[]>('api/observation/fetchAllLocations')
            .subscribe(response => {

                // response -> hele observation objekter med kun latitude og longitude fylt ut
                // må gå igjennom og sette riktig
                this.GoThroughResponse(response);
            },
                error => console.log(error)
            );
    }

    markerOptions: google.maps.MarkerOptions = {
        draggable: false  
    };

    markerPositions: google.maps.LatLngLiteral[] = [
        { lat: 48.647983479154824, lng: 9.865054057063944 },
    ];

    addMarker(event: google.maps.MapMouseEvent) {
       console.log(event.latLng.toJSON());
       this.markerPositions.push(event.latLng.toJSON());
    }

    

    latLng: google.maps.LatLngLiteral;

    // tar inn et array med objekter
    GoThroughResponse(response: Observation[]) {
        for (let i = 0; i <= response.length; i++) {
            let latitude = response[i].latitude;
            let longitude = response[i].longitude;

            // kan være at det ikke er et event
            this.markerPositions.push(this.latLng = { lat: Number(latitude), lng: Number(longitude) });
        }
    }
}
