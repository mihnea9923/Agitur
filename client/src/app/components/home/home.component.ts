import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private userService: UserService) { }
 
  ngOnInit(): void {
    this.userService.getUserProfile().subscribe(data => {
      console.log(data)
    })
    ,error => {
      console.log(error)
    }
  }

}
