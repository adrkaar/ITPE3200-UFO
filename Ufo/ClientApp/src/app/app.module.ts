import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';
import { AddCommentComponent } from './views/comment/addComment.component';
import { CommentComponent } from './views/comment/comment.component';
import { HomeComponent } from './views/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AddObservationComponent } from './views/observation/addObservation.component';
import { EditObservationComponent } from './views/observation/editObservation.component';
import { ObservationComponent } from './views/observation/observation.component';

/*Material*/
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';

/*Pipe*/
import { SafeHtml } from './pipe';

/*Maps*/
import { GoogleMapsModule } from '@angular/google-maps';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './views/login/login.component';

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
      /* Login */
        LoginComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule, ReactiveFormsModule,
        MatIconModule,
        MatButtonModule,
        MatFormFieldModule, MatInputModule,
        GoogleMapsModule,
        ReactiveFormsModule
        NgbModule
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule { }