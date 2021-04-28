import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  current_temperature: number;
  current_humidity: number;
  http: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<number>(baseUrl + 'Values/current-humidity').subscribe(result => {

      this.current_humidity = result;

    }, error => console.error(error));

    http.get<number>(baseUrl + 'Values/current-temperature').subscribe(result => {

      this.current_temperature = result;

    }, error => console.error(error));
  }

}
