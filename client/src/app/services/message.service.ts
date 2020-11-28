import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {


  constructor(private httpClient: HttpClient) { }

  tokenHeader = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('token') })
  sendMessage(newMessage: string, id: any) {
    return this.httpClient.post(API + 'message', { 'Text': newMessage, 'RecipientId': id }, { headers: this.tokenHeader, responseType: 'text' })
  }
  getMessages(interlocutorId: any) {
    return this.httpClient.get<any>(API + 'message/conversation/' + interlocutorId, { headers: this.tokenHeader })
  }

  markMessageAsRead(id: any) {
    return this.httpClient.put(API + 'message/' + id, {}, { headers: this.tokenHeader, responseType: 'text' })
  }

  getGroupMessages(groupId: any) {
    return this.httpClient.get<any>(API + 'groupMessage/' + groupId, { headers: this.tokenHeader })
  }
  sendGroupMessage(newMessage: string, id: any) {
    return this.httpClient.post(API + 'groupMessage' , {'Text' : newMessage , 'GroupId' : id} , {headers : this.tokenHeader , responseType : 'text'})
  }
}
