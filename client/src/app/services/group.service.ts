import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
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

  constructor(private httpClient: HttpClient) { }
}
