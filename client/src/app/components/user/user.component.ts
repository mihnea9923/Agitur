import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  constructor(private userService : UserService , private router : Router) { }

  ngOnInit(): void {
  }
  logOut()
  {
    localStorage.removeItem('token')
    this.router.navigate(['/user/login'])

  }
}
