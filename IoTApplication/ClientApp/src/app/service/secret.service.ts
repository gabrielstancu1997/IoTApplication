import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SecretService {

  constructor(private http: HttpClient) { }
  // tslint:disable-next-line: typedef
  getHttpOptions() {
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + localStorage.getItem('message'),
      }),
    };
    return httpOptions;
  }

}
