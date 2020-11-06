import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-new-message',
  templateUrl: './new-message.component.html',
  styleUrls: ['./new-message.component.scss']
})
export class NewMessageComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<NewMessageComponent>) { }
  email = ''
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
      
    }
  }
}
