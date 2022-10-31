import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AddCommentComponent } from './comment/addComment.component';
import { CommentComponent } from './comment/comment.component';
import { HomeComponent } from './home/home.component';
import { AddObservationComponent } from './observation/addObservation.component';
import { EditObservationComponent } from './observation/editObservation.component';
import { ObservationComponent } from './observation/observation.component';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SafeHtml } from './pipe';

const routes: Routes = [
      { path: '', component: HomeComponent, pathMatch: 'full' },
            /*Observation*/
      { path: 'observation', component: ObservationComponent },
      { path: 'addObservation', component: AddObservationComponent },
      { path: 'editObservation/:id', component: EditObservationComponent },
            /*Comment*/
      { path: 'comment/:id', component: CommentComponent },
      { path: 'addComment/:id', component: AddCommentComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes,
    {relativeLinkResolution: 'legacy'}),
        //AgmCoreModule.forRoot({
        //    apiKey: 'AIzaSyAqyuJni8oooxYmrtJ-7EI0u6gbK5xm4Sg'
        //})
        // NoopAnimationsModule
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
