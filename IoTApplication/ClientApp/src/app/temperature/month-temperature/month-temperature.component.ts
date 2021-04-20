import { DateMonthAverageValue } from './../../DateMonthAverageValue';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject} from '@angular/core';
import { ChartDataSets } from 'chart.js';
import { Color, Label } from 'ng2-charts';

@Component({
  selector: 'app-month-temperature',
  templateUrl: './month-temperature.component.html',
  styleUrls: ['./month-temperature.component.scss']
})
export class MonthTemperatureComponent {

  listDateMonthAverageValue: DateMonthAverageValue[];
  listDateMonthAverageValueParse: DateMonthAverageValue[] = [];
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
  lineChartType = 'bar';
  chartReady: boolean;
  http: any;


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<DateMonthAverageValue[]>(baseUrl + 'Values/months-temperature-values/2021').subscribe(result => {
      this.listDateMonthAverageValue = result;

      this.listDateMonthAverageValue.forEach(
        r => {
          this.listDateMonthAverageValueParse.push(JSON.parse(JSON.stringify(r)));
        }
      );

      this.listDateMonthAverageValueParse.forEach(
        r => {
          this.listAverageMonths.push(r.AvgMonth);
          this.lineChartLabels.push(r.DateMonthName);
        }
      );

      this.lineChartData = [
        { data: this.listAverageMonths, label: 'Temperature of the Year' },
      ];

      this.chartReady = true;
    }, error => console.error(error));
  }


}

