import { AfterViewChecked, Component, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import jwt_decode from 'jwt-decode';
import { DomSanitizer } from '@angular/platform-browser';
import { VocalMessageService } from 'src/app/services/vocal-message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent implements OnInit, AfterViewChecked {

  messages = []
  messagesLoaded
  userId = jwt_decode(localStorage.getItem('token')).UserId
  @ViewChild('messagesContainer') messagesContainer
  constructor(private messageServices: MessageService, private domSanitizer: DomSanitizer , private vocalMessageServices : VocalMessageService) {

  }
  //this hook is called each time a change of component data is made  
  ngAfterViewChecked(): void {
    this.scrollDown()
  }

  getMessages(interlocutorId) {
    this.messageServices.getMessages(interlocutorId).subscribe(data => {
      this.messages = data;
      this.markMessageAsRead()
      this.vocalMessageServices.get(interlocutorId).subscribe(data => {
        for(let i = 0 ; i < data.length ; i++)
        {
          this.vocalMessageServices.getUrlSource(data[i].id).subscribe(res => {
            data[i].urlSource = res
            console.log(res)
            
            this.putVocalMessageInCorectPosition(data[i])
          })
        }
      })
    })
  }

  ngOnInit(): void {
  }

  scrollDown() {
    this.messagesContainer.nativeElement.scrollTop = 10000000
    //this also works
    // document.getElementById('messages').scrollTop = 10000000
  }

  markMessageAsRead() {
    var lastMessage = this.messages[this.messages.length - 1]

    if (lastMessage.read == false && lastMessage.senderId != this.userId) {
      this.messageServices.markMessageAsRead(lastMessage.id).subscribe(data => {

      })
    }
  }
  getLastMessage() {

    return this.messages[this.messages.length - 1].text
  }
  deleteMessages() {
    this.messages = []
  }
  getGroupMessages(groupId) {
    this.messageServices.getGroupMessages(groupId).subscribe(data => {
      this.messages = data
    })

  }
  newGroupMessage(message) {
    this.messages.push(message)
  }
  addVocalMessage(blob , formData , interlocutorId) {
    this.vocalMessageServices.add(formData , interlocutorId).subscribe(data => {
      
    })
    let message = { 'urlSource': blob, 'senderId': this.userId }
    this.messages.push(message)
    this.scrollDown()
  }
  sanitizeURL(url) {
    return this.domSanitizer.bypassSecurityTrustUrl(URL.createObjectURL(url))
  }
  putVocalMessageInCorectPosition(vocalMessage)
  {
    for(let i = 0 ; i < this.messages.length ; i++)
    {
      if(vocalMessage.date < this.messages[i].date)
      {
        this.messages.splice(i , 0 , vocalMessage)
        return
      }
    }
    this.messages.push(vocalMessage)
  }
}
