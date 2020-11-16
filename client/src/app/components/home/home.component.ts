import { Component, OnInit } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import { UserContactsService } from 'src/app/services/user-contacts.service';
import { UserService } from 'src/app/services/user.service';
import jwt_decode from 'jwt-decode';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private userService: UserService, private userContactsService: UserContactsService, private messageService: MessageService) { }
  userContacts
  userId = jwt_decode(localStorage.getItem('token')).UserId
  profilePhoto
  newMessage = ''
  interlocutor
  ngOnInit(): void {
    this.userContactsService.getUserContacts().subscribe(data => {
      this.userContacts = data
      console.log(this.userId)
    })
    this.userService.getUserProfilePhoto().subscribe(data => {
      this.profilePhoto = data
    })

  }
  resetNewMessage(input) {
    this.newMessage = ''
    input.value = ''
  }
  updateInterlocutor(interlocutor) {
    this.interlocutor = interlocutor
    
  }
  updateNewMessage(newValue) {
    this.newMessage = newValue
  }
  sendMessage(input , messages) {
     this.messageService.sendMessage(this.newMessage , this.interlocutor.id).subscribe(data => {
       this.resetNewMessage(input)
       messages.getMessages(this.interlocutor.id)
       input.focus()
     })
  }


}
