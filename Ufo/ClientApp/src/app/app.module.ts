import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module'
import { AddCommentComponent } from './views/comment/addComment.component';
import { CommentComponent } from './views/comment/comment.component';
import { HomeComponent } from './views/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AddObservationComponent } from './views/observation/addObservation.component';
import { EditObservationComponent } from './views/observation/editObservation.component';
import { ObservationComponent } from './views/observation/observation.component';

import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';

/*Pipe*/
import { SafeHtml } from './pipe';

/*Maps*/
//import { AgmCoreModule } from '@agm/core';
import { GoogleMapsModule } from '@angular/google-maps';
import { RouterModule } from '@angular/router';


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
        HttpClientModule,
        FormsModule,
        MatIconModule,
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        GoogleMapsModule,
        AppRoutingModule,
        ReactiveFormsModule
        //AgmCoreModule.forRoot({
        //    apiKey: 'AIzaSyAqyuJni8oooxYmrtJ-7EI0u6gbK5xm4Sg'
        //})
        // NoopAnimationsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }