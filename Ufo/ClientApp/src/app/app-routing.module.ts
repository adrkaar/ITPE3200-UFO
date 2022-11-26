import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { CommentComponent } from './views/comment/comment.component';
import { HomeComponent } from './views/home/home.component';
import { AddObservationComponent } from './views/observation/addObservation.component';
import { EditObservationComponent } from './views/observation/editObservation.component';
import { ObservationComponent } from './views/observation/observation.component';
import { LoginComponent } from './views/login/login.component';

const routes: Routes = [
      { path: '', component: HomeComponent, pathMatch: 'full' },
            /*Observation*/
      { path: 'observation', component: ObservationComponent },
      { path: 'addObservation', component: AddObservationComponent },
      { path: 'editObservation/:id', component: EditObservationComponent },
            /*Comment*/
      { path: 'comment/:id', component: CommentComponent },
      /*LogIn*/
      { path: 'logIn', component: LoginComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, {relativeLinkResolution: 'legacy'}),
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
