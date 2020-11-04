import { Component, OnInit } from '@angular/core';
import { UserContactsService } from 'src/app/services/user-contacts.service';
import { UserService } from 'src/app/services/user.service';
import jwt_decode from 'jwt-decode';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private userService: UserService , private userContactsService : UserContactsService) { }
  userContacts
  profilePhoto
  ngOnInit(): void {
    
    this.userContactsService.getUserContacts().subscribe(data => {
      this.userContacts = data
    })
    
    this.userService.getUserProfilePhoto().subscribe(data => {
      this.profilePhoto = data
    })
    
  }
  

}
