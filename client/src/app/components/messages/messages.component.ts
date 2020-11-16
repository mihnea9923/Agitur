import { Component, OnInit } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent implements OnInit {

  messages = []
  userId = jwt_decode(localStorage.getItem('token')).UserId
  constructor(private messageServices : MessageService) 
  { 
    
  }
  getMessages(interlocutorId)
  {
    this.messageServices.getMessages(interlocutorId).subscribe(data => {
      this.messages = data;
    })
  }
  ngOnInit(): void {
  }

}
