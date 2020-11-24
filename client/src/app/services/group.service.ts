import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('token')})
  create(groupMembers , name)
  {
    return this.httpClient.post(API + 'Group/' + name , groupMembers , {headers : this.tokenHeader , responseType : 'text'})
  }

  constructor(private httpClient : HttpClient) { }
}
