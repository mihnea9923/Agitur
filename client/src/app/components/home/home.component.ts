import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import { UserContactsService } from 'src/app/services/user-contacts.service';
import { UserService } from 'src/app/services/user.service';
import { MessagesComponent } from '../messages/messages.component';
import jwt_decode from 'jwt-decode'
import { HubService } from 'src/app/services/hub.service';
import { GroupsComponent } from '../groups/groups.component';
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
  }
  isGroup : boolean = false;
  initial = true
  userContacts
  profilePhoto
  newMessage = ''
  interlocutor: any
  filteredContacts
  userId = jwt_decode(localStorage.getItem('token')).UserId
  @ViewChild(MessagesComponent) messagesComponent
  @ViewChild(GroupsComponent) groupsComponent
  @ViewChild('input') input
  @ViewChild('conversations') conversations

  ngOnInit(): void {
    this.hubService.connection.on("refreshMessages", (recipientId, senderId, message) => {
      if (this.userId == recipientId) {
        // if (this.interlocutor.id == senderId)
        //   this.messagesComponent.getMessages(senderId)
         this.putContactFirst(senderId)  

          this.userContacts[0].message = message
          if (this.interlocutor.id != senderId)
          {

            this.userContacts[0].messageRead = false
            this.userContacts[0].received = true
          }
        

      }
    })
    this.hubService.connection.on("refreshContacts", (user1, user2) => {
      if (this.userId == user1.Id) {
        this.userContacts.unshift(this.convertToJson(user2))
      }
      else if (this.userId == user2.Id) {
        this.userContacts.unshift(this.convertToJson(user1))
      }
    })
    this.userContactsService.getUserContacts().subscribe(data => {
      this.interlocutor = data[0]
      this.userContacts = data
      this.filteredContacts = data
    })
    this.userService.getUserProfilePhoto().subscribe(data => {
      this.profilePhoto = data
    })

  }
  convertToJson(user: any): any {
    let userConverted = {
      "id": user.Id,
      "message": user.Message,
      "messageTime": user.MessageTime,
      "name": user.Name,
      "messageRead": user.MessageRead,
      "position": user.Position,
      "profilePhoto": user.ProfilePhoto,
      "received": user.Received
    }
    return userConverted
  }
  resetNewMessage(input) {
    this.newMessage = ''
    input.value = ''
  }
  updateInterlocutor(interlocutor) {
    this.isGroup = false
    this.interlocutor = interlocutor
    this.interlocutor.messageRead = true
    this.focusMessageInput()
  }
  updateNewMessage(newValue) {
    this.newMessage = newValue
  }
  sendMessage() {
    if (this.newMessage != '' && this.isGroup == false)
      this.messageService.sendMessage(this.newMessage, this.interlocutor.id).subscribe(data => {
        this.resetNewMessage(this.input.nativeElement)
        this.putContactFirst(this.interlocutor.id)
        this.messagesComponent.getMessages(this.interlocutor.id)
        this.userContactsService.getUserContactById(this.interlocutor.id).subscribe(data => {
          this.interlocutor = data
          this.updateLastMessage()
        })
        this.focusMessageInput()
      })
     else if(this.newMessage != '' && this.isGroup == true)
     {
       this.messageService.sendGroupMessage(this.newMessage , this.interlocutor.id).subscribe(data => {
         this.resetNewMessage(this.input.nativeElement)
         this.messagesComponent.getGroupMessages(this.interlocutor.id)
         this.groupsComponent.getGroups()
         this.focusMessageInput()
       })
     } 
  }

  updateLastMessage() {
    for (let i = 0; i < this.userContacts.length; i++) {
      if (this.userContacts[i].id == this.interlocutor.id) {
        this.userContacts[i] = this.interlocutor
        break;
      }
    }
  }

  putContactFirst(id) {
    for (let i = 0; i < this.userContacts.length; i++) {
      if (this.userContacts[i].id == id) {
        for (let j = i - 1; j >= 0; j--) {
          [this.userContacts[j], this.userContacts[j + 1]] = [this.userContacts[j + 1], this.userContacts[j]]
        }
        return i
      }
    }
    return -1
  }
  focusMessageInput() {
    this.input.nativeElement.focus()
  }
  filterContacts(value: string) {
    if (value == '') {
      this.filteredContacts = this.userContacts
      return;
    }
    this.filteredContacts = []
    for (let i = 0; i < this.userContacts.length; i++) {
      if (this.userContacts[i].name.toLowerCase().includes(value.toLowerCase())) {
        this.filteredContacts.push(this.userContacts[i])
      }
    }
  }
  sendFirstMessage(data) {
    this.messagesComponent.deleteMessages()
    let newInterlocutor = {
      'name': data.user.name,
      'profilePhoto': data.profilePhoto,
      'id': data.user.id
    }
    this.interlocutor = newInterlocutor

  }
  toggleConversations() {
    if (this.conversations.nativeElement.innerHTML.trim() == 'Groups')
      this.conversations.nativeElement.innerHTML = 'Friends'
    else this.conversations.nativeElement.innerHTML = 'Groups'
    this.initial = this.initial == true ? false : true
  }
  loadGroupMessages(group) {
    this.messagesComponent.getGroupMessages(group.id)
    this.setInterlocutorAsGroup(group)
  }
  setInterlocutorAsGroup(group) {
    let newInterlocutor = {
      'name': group.name,
      'profilePhoto': group.photo,
      'id': group.id
    }
    this.interlocutor = newInterlocutor
    this.isGroup = true
  }
}
