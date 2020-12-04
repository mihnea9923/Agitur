import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { GroupService } from 'src/app/services/group.service';
import jwt_decode from 'jwt-decode'
@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {
  groups
  @Output() emitter = new EventEmitter()
  filteredGroups: any;
  userId = jwt_decode(localStorage.getItem('token')).UserId;
  constructor(private groupService : GroupService) 
  {
    this.getGroups()
  }
  ngOnInit(): void {
  }
  putGroupFirst(id)
  {
      for (let i = 0; i < this.groups.length; i++) {
        if (this.groups[i].id == id) {
          for (let j = i - 1; j >= 0; j--) {
            [this.groups[j], this.groups[j + 1]] = [this.groups[j + 1], this.groups[j]]
          }
          return i
        }
      }
      return -1
  }

  sendGroupInfo(group)
  {
    this.emitter.emit(group)
  }
  getGroups()
  {
    this.groupService.getGroups().subscribe(data => {
      this.groups = data
      this.filteredGroups = this.groups
      this.setLastMessage()
    })
  }
  updateGroupLastMessage(groupId , text , time , read : boolean)
  {
    for(let i = 0 ; i < this.groups.length ; i++)
    {
      if(groupId == this.groups[i].id)
      {
        this.groups[i].lastMessage = text
        this.groups[i].lastMessageTime = time
        this.groups[i].read = read
        break;
      }
    }
  }
  setLastMessage()
  {
    for(let i = 0 ; i < this.groups.length ; i++)
    {
      for(let j = 0 ; j < this.groups[i].lastMessageRead.length ; j++)
      {
        if(this.groups[i].lastMessageRead[j].userId == this.userId)
        {
          this.groups[i].read = this.groups[i].lastMessageRead[j].read
        }
      }
    }
  }
  filterGroups(value: string) {
    if (value == '') {
      this.filteredGroups = this.groups
      return;
    }
    this.filteredGroups = []
    for (let i = 0; i < this.groups.length; i++) {
      if (this.groups[i].name.toLowerCase().includes(value.toLowerCase())) {
        this.filteredGroups.push(this.groups[i])
      }
    }
  }
  markGroupLastMessageAsRead(groupId)
  {
    if(this.getGroupById(groupId).read == false)
    this.groupService.markGroupLastMessageAsRead(groupId).subscribe(data => {
      this.getGroupById(groupId).read = true
    })
  }
  getGroupById(groupId)
  {
    for(let i = 0 ; i < this.groups.length ; i++)
    {
      if(this.groups[i].id == groupId)
      {
        return this.groups[i]
      }
    }
  }
}
