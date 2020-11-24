import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GroupService } from '../services/group.service';
import { UserContactsService } from '../services/user-contacts.service';

@Component({
  selector: 'app-new-group',
  templateUrl: './new-group.component.html',
  styleUrls: ['./new-group.component.scss']
})
export class NewGroupComponent implements OnInit {

  constructor(private dialogRef : MatDialogRef<NewGroupComponent> , private userContactsServices : UserContactsService ,
    private groupService : GroupService , private snackBar: MatSnackBar ) { }
  slide1 = true
  slide2 = false
  userContacts
  groupMembers = []
  groupName = ''
  @ViewChild('container') container
  ngOnInit(): void {
    this.userContactsServices.getUserContacts().subscribe(data => {
      this.userContacts = data
    })
  }

  createGroup()
  {
    this.groupService.create(this.groupMembers , this.groupName).subscribe(data => {
      this.dialogRef.close()
      this.snackBar.open('Group was created', "", { duration: 3000, panelClass: 'snackbar-success' })

    })
  }

  loadNextSlide()
  {
    if(this.groupName != '')
    {
      this.slide1 = false
      this.slide2 = true
    }
  }

  updateMember(contact , button)
  {
    if(this.memberAdded(contact))
    {
      this.removeMember(contact ,button)
      button.classList.remove('added')
    }
    else
    {
      this.addMember(contact , button)
      button.classList.add('added')
    }
  }

  addMember(contact , button)
  {
    this.groupMembers.push(contact)
    button.innerHTML = 'Added'
  }
  removeMember(contact , button)
  {
    this.groupMembers.forEach((element , index)  => { 
      if(element == contact)
        this.groupMembers.splice(index , 1)
    });
    button.innerHTML = 'Add'
  }
  memberAdded(contact) : boolean
  {
    for(let i = 0 ; i < this.groupMembers.length ; i++)
    {
      if(this.groupMembers[i] == contact)
        return true
    }
    return false
  }
}
