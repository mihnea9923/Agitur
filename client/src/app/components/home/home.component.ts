import { AfterViewInit, Component, OnInit, Renderer2, ViewChild } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import { UserContactsService } from 'src/app/services/user-contacts.service';
import { UserService } from 'src/app/services/user.service';
import { MessagesComponent } from '../messages/messages.component';
import jwt_decode from 'jwt-decode'
import { HubService } from 'src/app/services/hub.service';
import { GroupsComponent } from '../groups/groups.component';
import { MatDialog } from '@angular/material/dialog';
import { ContactOptionsComponent } from '../contact-options/contact-options.component';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {

  constructor(private userService: UserService, private userContactsService: UserContactsService, private messageService: MessageService
    , private hubService: HubService , private renderer : Renderer2 , private matDialog : MatDialog) {
    this.hubService.startConnection()
  }
  ngAfterViewInit(): void {
  }
  isGroup: boolean = false;
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
        if (this.interlocutor.id != senderId) {

          this.userContacts[0].messageRead = false
          this.userContacts[0].received = true
        }

      }
    })
    this.hubService.connection.on("refreshContacts", (user1, user2) => {
      if (this.userId == user1.Id) {
        this.userContacts.unshift(this.convertUserToJson(user2))
      }
      else if (this.userId == user2.Id) {
        this.userContacts.unshift(this.convertUserToJson(user1))
      }
    })

    this.hubService.connection.on("putGroupFirst", (groupId, groupUsersId) => {
      for (let i = 0; i < groupUsersId.length; i++) {
        if (groupUsersId[i] == this.userId)
          this.groupsComponent.putGroupFirst(groupId)
      }
    })
    this.hubService.connection.on("groupCreated", (groupUsersId) => {
      for (let i = 0; i < groupUsersId.length; i++) {
        if (this.userId == groupUsersId[i])
          this.groupsComponent.getGroups()
      }
    })

    this.hubService.connection.on("groupMessage", (groupId, text, time, groupUsersId , newGroupMessage) => {
      for (let i = 0; i < groupUsersId.length; i++) {
        if (this.userId == groupUsersId[i])
        {
          this.groupsComponent.updateGroupLastMessage(groupId, text, time , this.interlocutor.id == groupId)
          if(this.interlocutor.id == groupId)
          {
            this.groupsComponent.markGroupLastMessageAsRead(groupId)
            this.messagesComponent.newGroupMessage(this.convertNewGroupMessageToJson(newGroupMessage))
          }
        }
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
  convertNewGroupMessageToJson(newGroupMessage: any): any {
    return {
      'id' : newGroupMessage.Id,
      'text' : newGroupMessage.Text,
      'date' : newGroupMessage.Date,
      'senderId' : newGroupMessage.SenderId,
      'senderPhoto' : newGroupMessage.SenderPhoto
    }
  }
  convertUserToJson(user: any): any {
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
        this.putContactFirst(this.interlocutor.id)
        this.messagesComponent.getMessages(this.interlocutor.id)
        this.userContactsService.getUserContactById(this.interlocutor.id).subscribe(data => {
          this.interlocutor = data
          this.updateLastMessage()
        })
      })
    else if (this.newMessage != '' && this.isGroup == true) {
      this.messageService.sendGroupMessage(this.newMessage, this.interlocutor.id).subscribe(data => {
        this.messagesComponent.getGroupMessages(this.interlocutor.id)
        this.groupsComponent.getGroups()
      })
    }
    this.focusMessageInput()
    this.resetNewMessage(this.input.nativeElement)

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
    this.focusMessageInput()
  }
  removeContact(deleteContact)
  {
    var coordinates = deleteContact.getBoundingClientRect()

    let dialog = this.matDialog.open(ContactOptionsComponent  , {position : {left : coordinates.x - 70 + 'px' , top : coordinates.y + 30 + 'px'} , 
    data :{'id' : this.interlocutor.id , 'isGroup' : this.isGroup}})
    dialog.afterClosed().subscribe(data => {
      if(data == 'contact')
      this.userContactsService.getUserContacts().subscribe(data => {
        this.filteredContacts = data
        this.userContacts = data
        this.interlocutor = this.filteredContacts[0]
        this.messagesComponent.deleteMessages()
      })
      else if(data == 'group')
      {
        this.groupsComponent.getGroups()
      }
    })
  }
}
