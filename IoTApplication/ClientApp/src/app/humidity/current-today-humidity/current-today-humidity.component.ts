import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ChartDataSets } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { DateValueToday } from 'src/app/DateValueToday';

@Component({
  selector: 'app-current-today-humidity',
  templateUrl: './current-today-humidity.component.html',
  styleUrls: ['./current-today-humidity.component.scss']
})
export class CurrentTodayHumidityComponent {

  listDateMonthAverageValue: DateValueToday[];
  listDateMonthAverageValueParse: DateValueToday[] = [];
  listAverageMonths: number[] = [];
  lineChartData: ChartDataSets[] = [];
  lineChartLabels: Label[] = [];
  numbers: number[] = [];

  lineChartOptions = {
    responsive: true,
  };

  lineChartColors: Color[] = [
    {
      borderColor: 'black',
      backgroundColor: 'rgba(41,185,190,0.28)',
    },
  ];

  lineChartLegend = true;
  lineChartPlugins = [];
  lineChartType = 'line';
  chartReady: boolean;
  http: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<DateValueToday[]>(baseUrl + 'Values/current-today-humidity').subscribe(result => {
      this.listDateMonthAverageValue = result;

      this.listDateMonthAverageValue.forEach(
        r => {
          this.listDateMonthAverageValueParse.push(JSON.parse(JSON.stringify(r)));
        }
      );

      this.listDateMonthAverageValueParse.forEach(
        r => {
          this.listAverageMonths.push(r.AvgHour);
          this.lineChartLabels.push(r.HourDescription);
        }
      );

      this.lineChartData = [
        { data: this.listAverageMonths, label: 'Humidity of the day' },
      ];

      this.chartReady = true;
    }, error => console.error(error));
  }


}
