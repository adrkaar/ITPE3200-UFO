import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { CommentComponent } from './views/comment/comment.component';
import { HomeComponent } from './views/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AddObservationComponent } from './views/observation/addObservation.component';
import { EditObservationComponent } from './views/observation/editObservation.component';
import { ObservationComponent } from './views/observation/observation.component';

/* Material */
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatFormFieldModule } from '@angular/material/form-field';

/* Pipe */
import { SafeHtml } from './pipe';

/* Maps */
import { GoogleMapsModule } from '@angular/google-maps';
import { ContactComponent } from './views/contact/contact.component';
import { Router, RouterModule } from '@angular/router';
import { LoginComponent } from './views/login/login.component';

/* Modal */
import { DialogComponent } from './components/dialog/dialog.component';
import { WarningmodalComponent } from './components/dialog/warningmodal/warningmodal.component';
import { WelcomemodalComponent } from './components/dialog/welcomemodal/welcomemodal.component';
import { LoginmodalComponent } from './components/dialog/loginmodal/loginmodal.component';
import { NotloggedinmodalComponent } from './components/dialog/notloggedinmodal/notloggedinmodal.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        SafeHtml,
        /* Oversation */
        ObservationComponent,
        AddObservationComponent,
        EditObservationComponent,
        /* Comment */
        CommentComponent,
        ContactComponent,
        /* Login */
        LoginComponent,
        /* Dialog / Modals */
        DialogComponent,
        WarningmodalComponent,
        WelcomemodalComponent,
        LoginmodalComponent,
        NotloggedinmodalComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule, ReactiveFormsModule,
        MatIconModule, MatTooltipModule,
        MatButtonModule,
        MatFormFieldModule, MatInputModule,
        GoogleMapsModule,
        ReactiveFormsModule,
        NgbModule,
        RouterModule, AppRoutingModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule { }