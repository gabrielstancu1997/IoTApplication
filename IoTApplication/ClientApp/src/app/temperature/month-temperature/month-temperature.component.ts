import { DateMonthAverageValue } from './../../DateMonthAverageValue';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject} from '@angular/core';
import { ChartDataSets } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import * as pattern from 'patternomaly';

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
      backgroundColor: 'rgba(63, 195, 128, 1)'
    },
  ];

  lineChartLegend = true;
  lineChartPlugins = [];
  lineChartType = 'bar';
  chartReady: boolean;
  http: any;

  listAverageMonthsPrediction: number[] = [];
  lineChartDataPrediction: ChartDataSets[] = [];
  lineChartLabelsPrediction: Label[] = [];
  lineChartOptionsPrediction = {
    responsive: true,
  };

  lineChartColorsPrediction: Color[] = [
    {
      borderColor: 'black',
      backgroundColor: 'rgba(242, 120, 75, 1)',
    },
  ];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<DateMonthAverageValue[]>(baseUrl + 'Values/months-temperature-values/2021').subscribe(result => {
      this.listDateMonthAverageValue = result;

      this.listDateMonthAverageValue.forEach(
        r => {
          this.listDateMonthAverageValueParse.push(JSON.parse(JSON.stringify(r)));
        }
      );

      for (let index = 0; index < this.listDateMonthAverageValueParse.length / 2; index++) {
        this.listAverageMonths.push(this.listDateMonthAverageValueParse[index].AvgMonth);
        this.lineChartLabels.push(this.listDateMonthAverageValueParse[index].DateMonthName);
      }

      for (let index = this.listDateMonthAverageValueParse.length / 2 ; index < this.listDateMonthAverageValueParse.length; index++ ) {
        this.listAverageMonthsPrediction.push(this.listDateMonthAverageValueParse[index].AvgMonth);
        this.lineChartLabelsPrediction.push(this.listDateMonthAverageValueParse[index].DateMonthName);
      }

      this.lineChartData = [
        { data: this.listAverageMonths, label: 'Temperature of the Year' },
      ];

      this.lineChartDataPrediction = [
        {data : this.listAverageMonthsPrediction, label: 'Prediction for the next months'}
      ];

      this.chartReady = true;
    }, error => console.error(error));
  }
}


