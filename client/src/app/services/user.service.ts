import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { API } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService : HttpClient) { }
  register(user)
  {
    return this.httpService.post<any>(API + 'user/Register' , user)
  }
}
