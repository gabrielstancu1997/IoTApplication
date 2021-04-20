/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LastMonthHumidityComponent } from './last-month-humidity.component';

describe('LastMonthHumidityComponent', () => {
  let component: LastMonthHumidityComponent;
  let fixture: ComponentFixture<LastMonthHumidityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LastMonthHumidityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LastMonthHumidityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
