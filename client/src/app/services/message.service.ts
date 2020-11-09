import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private httpClient : HttpClient) { }
  
  messageNewInterlocutor(email)
  {
    var tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('token')}) 
    return this.httpClient.get<any>(API + 'message/findInterlocutors/' + email , {headers : tokenHeader})
  }
}
