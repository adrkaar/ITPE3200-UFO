import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observation } from '../../models/observation.model';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
    map: google.maps.Map;
    markerOptions: google.maps.MarkerOptions = { draggable: false };

    constructor(private http: HttpClient) { }

    ngOnInit(): void {
        this.fetchAllLocations();

        // lager kartet
        this.map = new google.maps.Map(document.getElementById("homeMap") as HTMLElement, {
            center: { lat: 48.647983479154824, lng: 9.865054057063944 },
            zoom: 3,
            //mapTypeId: google.maps.MapTypeId.SATELLITE,
            disableDefaultUI: true,
        });
    }

    // henter alle observasjonene som kun inneholder lokasjonen
    fetchAllLocations() {
        this.http.get<Observation[]>('api/observation/fetchAllObservations')
            .subscribe(response => {
                // legger til lokasjonene paa kartet
                this.addToMap(response);
            }, error => console.log(error)
            );
    }

    // legger til lokasjonene paa kartet
    addToMap(observations: Observation[]) {
        var marker: google.maps.Marker;
        //const infoWindow = new google.maps.InfoWindow();

        for (let i = 0; i < observations.length; i++) {
            // lager markører med lengde og breddegrad fra observasjonen
            marker = new google.maps.Marker({
                position: { lat: Number(observations[i].latitude), lng: Number(observations[i].longitude) },
                map: this.map
            })

            //marker.addListener("click", () => {
            //    infoWindow.setContent(observations[i].description);
            //    infoWindow.open({
            //        anchor: marker,
            //        map: this.map
            //    });
            //});
        }
    }

}