import { LastYearHumidityComponent } from './humidity/last-year-humidity/last-year-humidity.component';
import { LastYearTemperatureComponent } from './temperature/last-year-temperature/last-year-temperature.component';
import { MonthHumidityComponent } from './humidity/month-humidity/month-humidity.component';
import { LastMonthHumidityComponent } from './humidity/last-month-humidity/last-month-humidity.component';
import { CurrentTodayTemperatureComponent } from './temperature/current-today-temperature/current-today-temperature.component';
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
import { SecretService } from './service/secret.service';
import { AuthService } from './service/auth.service';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AppRoutingModule } from './app-routing.module';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    MonthTemperatureComponent,
    LastMonthTemperatureComponent,
    CurrentTodayTemperatureComponent,
    CurrentTodayHumidityComponent,
    LastMonthHumidityComponent,
    MonthHumidityComponent,
    LastYearTemperatureComponent,
    LastYearHumidityComponent,
    RegisterComponent,
    LoginComponent
   ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ChartsModule,
    AppRoutingModule
  ],
  providers: [ThemeService, SecretService, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
