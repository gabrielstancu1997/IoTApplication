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
      backgroundColor: 'rgba(145, 61, 136, 1)',
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

    http.get<DateValueToday[]>(baseUrl + 'Values/current-today-humidity').subscribe(result => {
    this.listDateMonthAverageValue = result;

    this.listDateMonthAverageValue.forEach(
      r => {
        this.listDateMonthAverageValueParse.push(JSON.parse(JSON.stringify(r)));
      }
    );

    for (let index = 0; index < this.listDateMonthAverageValueParse.length / 2; index++) {
      this.listAverageMonths.push(this.listDateMonthAverageValueParse[index].AvgHour);
      this.lineChartLabels.push(this.listDateMonthAverageValueParse[index].HourDescription);
    }

    for (let index = this.listDateMonthAverageValueParse.length / 2 ; index < this.listDateMonthAverageValueParse.length; index++ ) {
      this.listAverageMonthsPrediction.push(this.listDateMonthAverageValueParse[index].AvgHour);
      this.lineChartLabelsPrediction.push(this.listDateMonthAverageValueParse[index].HourDescription);
    }

    this.lineChartData = [
      { data: this.listAverageMonths, label: 'Humidity of the last 24 hours' },
    ];

    this.lineChartDataPrediction = [
      {data : this.listAverageMonthsPrediction, label: 'Prediction for the next 24 hours'}
    ];

      this.chartReady = true;
    }, error => console.error(error));
  }


}
