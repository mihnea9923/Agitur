import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {
  groups
  @Output() emitter = new EventEmitter()
  constructor(private groupService : GroupService) 
  {
    this.getGroups()
  }
  ngOnInit(): void {
  }
  //TO DO
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
    })
  }

}
