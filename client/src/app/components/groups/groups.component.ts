import { Component, OnInit } from '@angular/core';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {
  groups
  constructor(private groupService : GroupService) 
  {
    this.groupService.getGroups().subscribe(data => {
      this.groups = data
      console.log(data)
    })
  }

  ngOnInit(): void {
  }

}
