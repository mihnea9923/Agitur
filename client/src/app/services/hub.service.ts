import { Injectable } from '@angular/core';
import { API, hubURL } from 'src/environments/environment';
import * as signalR from '@aspnet/signalr';


@Injectable({
  providedIn: 'root'
})
export class HubService {
  connection = new signalR.HubConnectionBuilder().withUrl(hubURL + "chat").build()
  constructor() {
  }
  startConnection()
  {
    this.connection.start();

  }
}
