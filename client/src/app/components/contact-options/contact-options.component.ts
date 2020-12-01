import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GroupService } from 'src/app/services/group.service';
import { UserContactsService } from 'src/app/services/user-contacts.service';

@Component({
  selector: 'app-contact-options',
  templateUrl: './contact-options.component.html',
  styleUrls: ['./contact-options.component.scss']
})
export class ContactOptionsComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data ,private userContactsService : UserContactsService ,
   private matDialogRef : MatDialogRef<ContactOptionsComponent> , private groupService : GroupService) { }

  ngOnInit(): void {
  }
  removeContact()
  {
    this.userContactsService.removeContact(this.data.id).subscribe(data => {
      this.matDialogRef.close('contact')
    })
  }
  leaveGroup()
  {
      this.groupService.leaveGroup(this.data.id).subscribe(data => {
        this.matDialogRef.close('group')
      })
  }

}
