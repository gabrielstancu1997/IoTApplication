import { MonthHumidityComponent } from './humidity/month-humidity/month-humidity.component';
import { LastMonthHumidityComponent } from './humidity/last-month-humidity/last-month-humidity.component';
import { CurrentTodayTemperatureComponent } from './temperature/current-today-temperature/current-today-temperature.component';
import { LineChartComponent } from './line-chart/line-chart.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ChartsModule, ThemeService } from 'ng2-charts';
import { MonthTemperatureComponent } from './temperature/month-temperature/month-temperature.component';
import { LastMonthTemperatureComponent } from './temperature/last-month-temperature/last-month-temperature.component';
import { CurrentTodayHumidityComponent } from './humidity/current-today-humidity/current-today-humidity.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LineChartComponent,
    MonthTemperatureComponent,
    LastMonthTemperatureComponent,
    CurrentTodayTemperatureComponent,
    CurrentTodayHumidityComponent,
    LastMonthHumidityComponent,
    MonthHumidityComponent
   ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ChartsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'temperature', component: LineChartComponent},
      { path: 'months-temperature', component: MonthTemperatureComponent},
      { path: 'current-month-temperature', component: LastMonthTemperatureComponent},
      { path: 'current-today-temperature', component: CurrentTodayTemperatureComponent},
      { path: 'current-today-humidity', component: CurrentTodayHumidityComponent},
      { path: 'current-month-humidity', component: LastMonthHumidityComponent},
      { path: 'months-humidity', component: MonthHumidityComponent},
    ])
  ],
  providers: [ThemeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
