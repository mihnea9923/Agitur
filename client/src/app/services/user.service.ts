import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { API } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
@Injectable({
  providedIn: 'root'
})
export class UserService {
 
  constructor(private httpClient : HttpClient) { }
  register(user)
  {
    return this.httpClient.post<any>(API + 'user/Register' , user)
  }
  login(user)
  {
    return this.httpClient.post<any>(API + 'user/Login' , user)
  }
  
  getUserProfilePhoto()
  {
    var tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('token')})
    return this.httpClient.get<any>(API + 'userprofile/profilePhoto' ,{headers : tokenHeader}) 
  }
  updateUserProfilePhoto(photoForm: FormData)
  {
    var tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('token') })
    return this.httpClient.post(API + 'userProfile/uploadProfilePhoto' , photoForm ,{headers : tokenHeader , responseType: 'text'} )
  }

}
