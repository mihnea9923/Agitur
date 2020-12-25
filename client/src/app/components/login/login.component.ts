import { Component, OnInit } from '@angular/core';
import {FormsModule} from '@angular/forms'
import { UserService } from 'src/app/services/user.service';
import jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private userService : UserService , private router : Router , private snackBar: MatSnackBar) { }
  formModel = {
    email : '',
    password : ''
  }
  ngOnInit(): void {
    if(localStorage.getItem('token') != null)
    {
      this.router.navigateByUrl('/home')
    }
  }

  logIn(form)
  {
    this.userService.login(this.formModel).subscribe(data=> {
      localStorage.setItem('token' , data.token)
      this.router.navigateByUrl('home')
      //console.log(jwt_decode(data.token))
      //console.log(jwt_decode(data.token).UserId)
      //console.log(jwt_decode(data.token).Email)
      
    },
    error => {
      //alert(error.status)
      this.snackBar.open('Email or password incorect' , "" , {duration : 3000 , panelClass : 'snackbar-danger'});
    })
    
    
  }
}
