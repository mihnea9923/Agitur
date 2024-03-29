import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-upload-photo-form',
  templateUrl: './upload-photo-form.component.html',
  styleUrls: ['./upload-photo-form.component.scss']
})
export class UploadPhotoFormComponent implements OnInit {

  constructor(private dialogRef : MatDialogRef<UploadPhotoFormComponent> , private userService : UserService , private router : Router) { }
  photoForm = new FormData()
  ngOnInit(): void {
  }
  setPhoto(event)
  {
    var photo = event.target.files[0]
    this.photoForm.append('photo' ,photo)
    
  }

  closeDialog()
  {
    this.dialogRef.close()
  }
  sendPhoto()
  {
    this.userService.updateUserProfilePhoto(this.photoForm).subscribe(data =>
      {
        this.closeDialog()
        this.router.navigate(['/user/login'])
      })
    }
}
