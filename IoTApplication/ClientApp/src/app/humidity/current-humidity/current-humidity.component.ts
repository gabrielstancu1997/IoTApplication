import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-current-humidity',
  templateUrl: './current-humidity.component.html',
  styleUrls: ['./current-humidity.component.scss']
})
export class CurrentHumidityComponent {

  current_humidity: number;
  http: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<any>(baseUrl + 'Values/current-humidity').subscribe(result => {

      this.current_humidity = result;

    }, error => console.error(error));
  }

}
