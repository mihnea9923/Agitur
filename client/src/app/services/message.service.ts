import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  
  constructor(private httpClient : HttpClient) { }

  sendMessage(newMessage: string, id: any) {
    var tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('token')})
    return this.httpClient.post(API + 'message' , {'Text' : newMessage , 'RecipientId' : id} , {headers : tokenHeader , responseType : 'text'})
  }
  getMessages(interlocutorId: any) {
    var tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('token')})
    return this.httpClient.get<any>(API + 'message/conversation/' + interlocutorId , {headers : tokenHeader})
  }

   
  
  markMessageAsRead(id: any) {
    return this.httpClient.put(API + 'message/' + id , {} , {headers : {'Authorization' : 'Bearer ' + localStorage.getItem('token')} , responseType : 'text'})
  }
}
