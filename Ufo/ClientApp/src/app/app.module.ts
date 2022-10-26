import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { ObservationComponent } from './observation/observation.component';
import { AddObservationComponent } from './observation/addObservation.component';
import { EditObservationComponent } from './observation/editObservation.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { CommentComponent } from './comment/comment.component';
import { AddCommentComponent } from './comment/addComment.component';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { MatIconModule } from '@angular/material/icon';


import { SafeHtml } from './pipe';


// m�  g�es igjennom hca som faktisk brukes og er p� riktig sted typ trenger alle de decalrations?
@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        /*Oversation*/
        ObservationComponent,
        AddObservationComponent,
        EditObservationComponent,
        /*Comment*/
        CommentComponent,
        AddCommentComponent,
        SafeHtml,
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClentModule,
        FormsModule
        RouterModule.forRoot([
    { path: '', component: HomeComponent, pathMatch: 'full' },
    /*Observation*/
    { path: 'observation', component: ObservationComponent },
    { path: 'addObservation', component: AddObservationComponent },
    { path: 'editObservation/:id', component: EditObservationComponent },
    /*Comment*/
    { path: 'comment/:id', component: CommentComponent },
    { path: 'addComment/:id', component: AddCommentComponent },
], { relativeLinkResolution: 'legacy' })
        // NoopAnimationsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }