import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-new-message',
  templateUrl: './new-message.component.html',
  styleUrls: ['./new-message.component.scss']
})
export class NewMessageComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<NewMessageComponent> , private messageService : MessageService) { }
  email = ''
  interlocutors = []
  interlocutorsFound = true
  ngOnInit(): void {
  }
  closeDialog()
  {
    this.dialogRef.close()
  }
  findFriends(input)
  {
    
    if(this.email != '')
    {
       this.messageService.findNewInterlocutors(this.email).subscribe(data  => {
           
           this.interlocutors = data
           if(data.length == 0)
           {
             this.interlocutorsFound = false
           }
           else
           {
             this.interlocutorsFound = true
           }
       })    
    }
  }
}
