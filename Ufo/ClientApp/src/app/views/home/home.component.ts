import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observation } from '../../models/observation.model';
import { GeneralService } from 'src/app/services/general.service';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
    map: google.maps.Map;
    markerOptions: google.maps.MarkerOptions = { draggable: false };

    icon = {
        url: "marker.png",
        scaledSize: new google.maps.Size(40, 40),
    }

    constructor(private http: HttpClient, public generalService: GeneralService) { }

    ngOnInit(): void {
        this.fetchAllLocations();

        // lager kartet
        this.map = new google.maps.Map(document.getElementById("homeMap") as HTMLElement, {
            center: { lat: 48.647983479154824, lng: 9.865054057063944 },
            zoom: 3,
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

        for (let i = 0; i < observations.length; i++) {

            var marker: google.maps.Marker;
            var info = "<div id='content'><p>" + observations[i].description + "</div></p>";

            var infoWindow = new google.maps.InfoWindow({
                content: info
            });

            // lager markører med lengde og breddegrad fra observasjonen
            marker = new google.maps.Marker({
                position: { lat: Number(observations[i].latitude), lng: Number(observations[i].longitude) },
                map: this.map,
                icon: this.icon,
            })

            /* https://stackoverflow.com/questions/13674194/google-maps-api-multiple-markers-info-window-only-showing-last-element */
            // satter info vindu til hver markør med beskrivelsen av observasjonen
            marker.addListener("click", (function (marker, info) {
                return function () {
                    infoWindow.setContent(info);
                    infoWindow.open(this.map, marker);
                }
            })(marker, info));
        }
    }
}