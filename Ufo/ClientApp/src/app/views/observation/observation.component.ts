import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observation } from '../../models/observation.model';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    templateUrl: 'observation.component.html'
})

export class ObservationComponent {
    allObservations: Array<Observation>;

    constructor(private http: HttpClient) { }

    ngOnInit() {
        this.fetchAllObservations();
    }
    
    fetchAllObservations() {
        // fetch all observations eller locations
        // har en lille tiden man sparer noe å si?
        this.http.get<Observation[]>('api/observation/fetchAllObservations')
            .subscribe(response => {
                this.allObservations = response;
            },
                error => console.log(error)
            );
    }

    openMapwithLocation(latitude: number, longitude: number) {
    }
}