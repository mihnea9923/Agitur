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
  putGroupFirst(group)
  {

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
