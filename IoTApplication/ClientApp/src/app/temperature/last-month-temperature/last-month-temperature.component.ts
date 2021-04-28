import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ChartDataSets } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { LastMonthAverageValue } from 'src/app/LastMonthAverageValue';

@Component({
  selector: 'app-last-month-temperature',
  templateUrl: './last-month-temperature.component.html',
  styleUrls: ['./last-month-temperature.component.scss']
})
export class LastMonthTemperatureComponent {

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
      backgroundColor: 'rgba(255,165,0,0.28)',
    },
  ];

  lineChartLegend = true;
  lineChartPlugins = [];
  lineChartType = 'line';
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

    http.get<LastMonthAverageValue[]>(baseUrl + 'Values/current-month-temperature').subscribe(result => {
      this.listDateMonthAverageValue = result;

      this.listDateMonthAverageValue.forEach(
        r => {
          this.listDateMonthAverageValueParse.push(JSON.parse(JSON.stringify(r)));
        }
      );

      for (let index = 0; index < this.listDateMonthAverageValueParse.length / 2; index++) {
        this.listAverageMonths.push(this.listDateMonthAverageValueParse[index].AvgDay);
        this.lineChartLabels.push(this.listDateMonthAverageValueParse[index].DayDescription);
      }

      for (let index = this.listDateMonthAverageValueParse.length / 2 ; index < this.listDateMonthAverageValueParse.length; index++ ) {
        this.listAverageMonthsPrediction.push(this.listDateMonthAverageValueParse[index].AvgDay);
        this.lineChartLabelsPrediction.push(this.listDateMonthAverageValueParse[index].DayDescription);
      }

      this.lineChartData = [
        { data: this.listAverageMonths, label: 'Temperature for the days of the previous month' },
      ];

      this.lineChartDataPrediction = [
        {data : this.listAverageMonthsPrediction, label: 'Prediction for the next days'}
      ];

      this.chartReady = true;
    }, error => console.error(error));
  }

}
