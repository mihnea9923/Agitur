import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NewGroupComponent } from 'src/app/new-group/new-group.component';
import { UserService } from 'src/app/services/user.service';
import { NewMessageComponent } from '../new-message/new-message.component';
import { UploadPhotoFormComponent } from '../upload-photo-form/upload-photo-form.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private matDialog: MatDialog, private router: Router, private userService: UserService) { }

  profilePhoto
  photoExists = false
  @Output() sendInterlocutor = new EventEmitter<any>()
  ngOnInit(): void {
    this.userService.getUserProfilePhoto().subscribe(data => {
      this.profilePhoto = data
      if (this.profilePhoto.profilePhoto != null)
        this.photoExists = true

    })

  }

  openPhotoDialog() {
    var dialog = this.matDialog.open(UploadPhotoFormComponent)

  }
  logOut() {
    localStorage.removeItem('token')
    this.router.navigateByUrl('/user/login')
  }
  openNewMessageDialog() {
    var dialog = this.matDialog.open(NewMessageComponent)
    dialog.afterClosed().subscribe(data => {
      if (data != null)
        this.sendInterlocutor.emit(data)
    })
  }
  openNewGroupDialog()
  {
    var dialog = this.matDialog.open(NewGroupComponent , {panelClass : 'custom-dialog-container'})
  }


}
