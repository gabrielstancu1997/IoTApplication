import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ChartDataSets } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { LastMonthAverageValue } from 'src/app/LastMonthAverageValue';

@Component({
  selector: 'app-last-month-humidity',
  templateUrl: './last-month-humidity.component.html',
  styleUrls: ['./last-month-humidity.component.scss']
})
export class LastMonthHumidityComponent {


  listDateMonthAverageValue: LastMonthAverageValue[];
  listDateMonthAverageValueParse: LastMonthAverageValue[] = [];
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

    http.get<LastMonthAverageValue[]>(baseUrl + 'Values/current-month-humidity').subscribe(result => {
      this.listDateMonthAverageValue = result;

      this.listDateMonthAverageValue.forEach(
        r => {
          this.listDateMonthAverageValueParse.push(JSON.parse(JSON.stringify(r)));
        }
      );

      this.listDateMonthAverageValueParse.forEach(
        r => {
          this.listAverageMonths.push(r.AvgDay);
          this.lineChartLabels.push(r.DayDescription);
        }
      );

      this.lineChartData = [
        { data: this.listAverageMonths, label: 'Humidity of the month' },
      ];

      this.chartReady = true;
    }, error => console.error(error));
  }
}
