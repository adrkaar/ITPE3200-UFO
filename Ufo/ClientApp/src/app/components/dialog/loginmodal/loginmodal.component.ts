import { Component, OnInit } from '@angular/core';
import { GeneralService } from 'src/app/services/general.service';
import { LoginComponent } from 'src/app/views/login/login.component';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { TooltipPosition } from '@angular/material/tooltip';

@Component({
  selector: 'app-loginmodal',
  templateUrl: './loginmodal.component.html',
  styleUrls: ['./loginmodal.component.scss']
})
export class LoginmodalComponent implements OnInit {

  Form: FormGroup;
    user: User = {
        id: '',
        username: '',
        password: '',
        token: ''
    }
  
    positionOptions: TooltipPosition[] = ['after', 'before', 'above', 'below', 'left', 'right'];
    position = new FormControl(this.positionOptions[0]);
    
  constructor(private http: HttpClient, private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, public generalService: GeneralService) {
    this.Form = this.formBuilder.group({
        username: [null,
            Validators.compose([
                Validators.required,
                Validators.pattern("^[a-zA-Z0-9]+$")
            ])],
        password: [null,
            Validators.compose([
                Validators.required,
                Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
            ])]
    })
}

logIn() {
  this.http.post<User>('api/user/logIn', this.user)
      .subscribe(response => {
          if (response) this.router.navigate(['observation'])
          else alert("Wrong username or password, please try again");
      },
          error => console.log(error)
  );
}

  ngOnInit(): void {
  }

}
