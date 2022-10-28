import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {
    zoom: number = 5;
    center: google.maps.LatLngLiteral = { lat: 48.647983479154824, lng: 9.865054057063944 }

    position: any = [
        { lat: 51.678418, lng: 7.809007 }, 
        { lat: 51.679418, lng: 7.809607 },
        { lat: 51.678718, lng: 7.804007 },
        { lat: 51.673418, lng: 7.809507 },
        { lat: 51.618418, lng: 7.807007 },
    ]

    constructor(private http: HttpClient) { }

    ngOnInit(): void {
        
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
}
