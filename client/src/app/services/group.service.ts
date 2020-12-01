import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  constructor(private httpClient: HttpClient) { }
  
  tokenHeader = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('token') })
  getGroups() {
    return this.httpClient.get(API + 'Group', { headers: this.tokenHeader })
  }
  setPhoto(formData: FormData, groupId) {
    return this.httpClient.post(API + 'Group/photo/' + groupId, formData, { headers: this.tokenHeader, responseType: 'text' })
  }

  create(groupMembers, name) {
    return this.httpClient.post(API + 'Group/' + name, groupMembers, { headers: this.tokenHeader })
  }
  leaveGroup(id: any) {
    return this.httpClient.put(API + 'Group/leaveGroup/' + id , {} , {headers : this.tokenHeader , responseType : 'text'})
  }
}
