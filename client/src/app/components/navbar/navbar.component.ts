import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { UploadPhotoFormComponent } from '../upload-photo-form/upload-photo-form.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private matDialog : MatDialog , private router : Router , private userService : UserService) { }
  profilePhoto
  photoExists = false
  ngOnInit(): void {
    this.userService.getUserProfilePhoto().subscribe(data => {
      this.profilePhoto = data
      if(this.profilePhoto.profilePhoto != null)
      this.photoExists = true
      
    })
  }
  
  openPhotoDialog()
  {
    
    var dialog = this.matDialog.open(UploadPhotoFormComponent)
    
  }
  logOut()
  {
    localStorage.removeItem('token')
    this.router.navigateByUrl('/user/login')
  }

}
