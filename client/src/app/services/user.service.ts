import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { API } from 'src/environments/environment';
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
  
  getUserProfile()
  {
    var tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('token')})
    return this.httpClient.get<any>(API + 'userprofile' , {headers : tokenHeader})
  }
  updateUserProfilePhoto(photoForm: FormData)
  {
    var tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('token')})
    return this.httpClient.post<any>(API + 'userProfile/uploadProfilePhoto' , photoForm ,{headers : tokenHeader})
  }

}
