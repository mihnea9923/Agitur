import { AfterViewChecked, Component, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent implements OnInit,AfterViewChecked {

  messages = []
  messagesLoaded 
  userId = jwt_decode(localStorage.getItem('token')).UserId
  @ViewChild('messagesContainer') messagesContainer
  constructor(private messageServices : MessageService) 
  { 
    
  }
  //this hook is called each time a change of component data is made  
  ngAfterViewChecked(): void {
    this.scrollDown()
  }
  
  getMessages(interlocutorId)
  {
    this.messageServices.getMessages(interlocutorId).subscribe(data => {
      this.messages = data;
      this.markMessageAsRead()
    })
  }
 
  ngOnInit(): void {
  }

  scrollDown()
  {
    this.messagesContainer.nativeElement.scrollTop = 10000000
    //this also works
    // document.getElementById('messages').scrollTop = 10000000
  }

  markMessageAsRead()
  {
    var lastMessage = this.messages[this.messages.length - 1]

    if(lastMessage.read == false && lastMessage.senderId != this.userId)
    {
      this.messageServices.markMessageAsRead(lastMessage.id).subscribe(data => {
        
      })
    }
  }
  getLastMessage()
  {
    
    //console.log(this.messages[this.messages.length - 1])
    return this.messages[this.messages.length - 1].text
  }
  deleteMessages()
  {
    this.messages = []
  }
  getGroupMessages(groupId)
  {
    this.messageServices.getGroupMessages(groupId).subscribe(data => {
      // console.log(data)
      this.messages = data
    })

  }
  newGroupMessage(message)
  {
    this.messages.push(message)
  }
}
