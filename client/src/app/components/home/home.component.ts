import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import { UserContactsService } from 'src/app/services/user-contacts.service';
import { UserService } from 'src/app/services/user.service';
import { MessagesComponent } from '../messages/messages.component';
import jwt_decode from 'jwt-decode'
import { HubService } from 'src/app/services/hub.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {

  constructor(private userService: UserService, private userContactsService: UserContactsService, private messageService: MessageService
    , private hubService: HubService) {
    this.hubService.startConnection()
  }
  ngAfterViewInit(): void {
    this.messagesComponent.scrollDown()
  }

  userContacts
  profilePhoto
  newMessage = ''
  interlocutor: any
  userId = jwt_decode(localStorage.getItem('token')).UserId
  @ViewChild(MessagesComponent) messagesComponent
  ngOnInit(): void {
    this.hubService.connection.on("refreshMessages", (recipientId, senderId) => {
      if (this.userId == recipientId) {
        this.messagesComponent.getMessages(senderId)
      }
    })
    this.userContactsService.getUserContacts().subscribe(data => {
      this.interlocutor = data[0]
      this.userContacts = data
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
    this.interlocutor.messageRead = true
  }
  updateNewMessage(newValue) {
    this.newMessage = newValue
  }
  sendMessage(input) {
    if (this.newMessage != '')
      this.messageService.sendMessage(this.newMessage, this.interlocutor.id).subscribe(data => {
        this.resetNewMessage(input)
        this.putContactFirst()
        this.messagesComponent.getMessages(this.interlocutor.id)
        this.userContactsService.getUserContactById(this.interlocutor.id).subscribe(data => {
          this.interlocutor = data
          this.updateLastMessage()
        })
        input.focus()
      })
  }

  updateLastMessage() {
    for (let i = 0; i < this.userContacts.length; i++) {
      if (this.userContacts[i].id == this.interlocutor.id) {
        this.userContacts[i] = this.interlocutor
        break;
      }
    }
  }
  putContactFirst() {
    for (let i = 0; i < this.userContacts.length; i++) {
      if (this.userContacts[i] == this.interlocutor) {
        for (let j = i - 1; j >= 0; j--) {
          [this.userContacts[j], this.userContacts[j + 1]] = [this.userContacts[j + 1], this.userContacts[j]]
        }
      }
    }
  }


}
