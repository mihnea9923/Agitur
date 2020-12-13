import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './components/user/user.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { UserService } from './services/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialsModule } from './materials/materials/materials.module';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { UserContactsService } from './services/user-contacts.service';
import { NavbarComponent } from './components/navbar/navbar.component';
import { UploadPhotoFormComponent } from './components/upload-photo-form/upload-photo-form.component';
import { NewMessageComponent } from './components/new-message/new-message.component';
import { MessagesComponent } from './components/messages/messages.component';
import { NewGroupComponent } from './new-group/new-group.component';
import { GroupsComponent } from './components/groups/groups.component';
import { ContactOptionsComponent } from './components/contact-options/contact-options.component';


@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    UserComponent,
    LoginComponent,
    HomeComponent,
    NavbarComponent,
    UploadPhotoFormComponent,
    NewMessageComponent,
    MessagesComponent,
    NewGroupComponent,
    GroupsComponent,
    ContactOptionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialsModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [UserService , UserContactsService],
  bootstrap: [AppComponent]

})
export class AppModule { }
