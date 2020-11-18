import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserContactsService {

  constructor(private httpClient : HttpClient) { }
  getUserContacts()
  {
    return this.httpClient.get<any>(API + 'userContacts' , {headers : {'Authorization' : 'Bearer ' + localStorage.getItem('token')}})
  }
  getUserContactById(id)
  {
    return this.httpClient.get(API + 'userContacts/getContact/' + id , {headers : {'Authorization' : 'Bearer ' + localStorage.getItem('token')}})
  } 
  findNewInterlocutors(email)
  {
    var tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('token')}) 
    return this.httpClient.get<any>(API + 'message/findInterlocutors/' + email , {headers : tokenHeader})
  }
}
