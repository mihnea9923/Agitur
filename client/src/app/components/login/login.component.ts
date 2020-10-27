import { Component, OnInit } from '@angular/core';
import {FormsModule} from '@angular/forms'
import { UserService } from 'src/app/services/user.service';
import jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private userService : UserService , private router : Router) { }
  ngOnInit(): void {
    if(localStorage.getItem('token') != null)
    {
      this.router.navigateByUrl('/home')
    }
  }
  formModel = {
    email : '',
    password : ''
  }

  logIn(form)
  {
    this.userService.login(this.formModel).subscribe(data => {
      localStorage.setItem('token' , data.token)
      this.router.navigateByUrl('home')
      console.log(data)
      console.log(jwt_decode(data.token))
      console.log(jwt_decode(data.token).UserId)
      console.log(jwt_decode(data.token).Email)
    }),
    error => {
      
      alert(error.status)
      console.log(error)
    }
  }
}
