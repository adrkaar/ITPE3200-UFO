import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { ObservationComponent } from './observation/observation.component'
import { AddObservationComponent } from './observation/addObservation.component'
import { EditObservationComponent } from './observation/editObservation.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations'
import { CommentComponent } from './comment/comment.component';
import { AddComment } from './comment/addComment.component';

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
        AddComment
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            /*Observation*/
            { path: 'observation', component: ObservationComponent },
            { path: 'addObservation', component: AddObservationComponent },
            { path: 'editObservation/:id', component: EditObservationComponent },
            /*Comment*/
            { path: 'comment/:id', component: CommentComponent },
            { path: 'addComment/:id', component: AddComment },
        ]),
        NoopAnimationsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }