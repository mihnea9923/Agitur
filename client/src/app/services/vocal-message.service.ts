import { HttpClient, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VocalMessageService {
  

  constructor(private httpClient : HttpClient) { }
  tokenHeader = new HttpHeaders( {'Authorization' : 'Bearer ' + localStorage.getItem('token')})
  add(formData: FormData, recipientId: any) {
    return this.httpClient.post(API + 'VocalMessage/' + recipientId , formData , {headers : this.tokenHeader , responseType : 'text'})
  }
  get(recipientId)
  {
    return this.httpClient.get<any>(API + 'vocalMessage/' + recipientId , {headers : this.tokenHeader })
  }
  getUrlSource(vocalId: any) {
    return this.httpClient.get(API + 'vocalMessage/urlSource/' + vocalId , {headers : this.tokenHeader , responseType : 'blob'})
  }
}
