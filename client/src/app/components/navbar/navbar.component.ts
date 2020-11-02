import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UploadPhotoFormComponent } from '../upload-photo-form/upload-photo-form.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private matDialog : MatDialog) { }

  ngOnInit(): void {
  }

  openPhotoDialog()
  {
    
    var dialog = this.matDialog.open(UploadPhotoFormComponent)
    
  }

}
