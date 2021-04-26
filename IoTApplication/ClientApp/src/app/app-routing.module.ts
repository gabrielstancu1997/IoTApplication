import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { CurrentTodayHumidityComponent } from './humidity/current-today-humidity/current-today-humidity.component';
import { LastMonthHumidityComponent } from './humidity/last-month-humidity/last-month-humidity.component';
import { MonthHumidityComponent } from './humidity/month-humidity/month-humidity.component';
import { LineChartComponent } from './line-chart/line-chart.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthguardService } from './service/authguard.service';
import { CurrentTodayTemperatureComponent } from './temperature/current-today-temperature/current-today-temperature.component';
import { LastMonthTemperatureComponent } from './temperature/last-month-temperature/last-month-temperature.component';
import { MonthTemperatureComponent } from './temperature/month-temperature/month-temperature.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' , canActivate: [AuthguardService]},
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent , canActivate: [AuthguardService]},
  { path: 'register', component: RegisterComponent},
  { path: 'counter', component: CounterComponent, canActivate: [AuthguardService] },
  { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthguardService] },
  { path: 'temperature', component: LineChartComponent, canActivate: [AuthguardService]},
  { path: 'months-temperature', component: MonthTemperatureComponent, canActivate: [AuthguardService]},
  { path: 'current-month-temperature', component: LastMonthTemperatureComponent, canActivate: [AuthguardService]},
  { path: 'current-today-temperature', component: CurrentTodayTemperatureComponent, canActivate: [AuthguardService]},
  { path: 'current-today-humidity', component: CurrentTodayHumidityComponent, canActivate: [AuthguardService]},
  { path: 'current-month-humidity', component: LastMonthHumidityComponent, canActivate: [AuthguardService]},
  { path: 'months-humidity', component: MonthHumidityComponent, canActivate: [AuthguardService]},
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
