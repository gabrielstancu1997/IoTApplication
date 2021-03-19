import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ChartDataSets } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { DateValueToday } from 'src/app/DateValueToday';

@Component({
  selector: 'app-current-today-temperature',
  templateUrl: './current-today-temperature.component.html',
  styleUrls: ['./current-today-temperature.component.scss']
})
export class CurrentTodayTemperatureComponent {

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
      backgroundColor: 'rgba(255,165,0,0.28)',
    },
  ];

  lineChartLegend = true;
  lineChartPlugins = [];
  lineChartType = 'line';
  chartReady: boolean;
  http: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<DateValueToday[]>(baseUrl + 'Values/current-today-temperature').subscribe(result => {
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
        { data: this.listAverageMonths, label: 'Temperature of the day' },
      ];

      this.chartReady = true;
    }, error => console.error(error));
  }


}
