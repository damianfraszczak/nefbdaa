import {NgModule} from '@angular/core';

import {
  OWL_DATE_TIME_FORMATS,
  OWL_DATE_TIME_LOCALE,
  OwlDateTimeModule,
  OwlNativeDateTimeModule,
} from 'ng-pick-datetime';


export const DATE_NATIVE_FORMATS = {
  fullPickerInput: {year: 'numeric', month: 'numeric', day: 'numeric', hour: 'numeric', minute: 'numeric'},
  datePickerInput: {year: 'numeric', month: 'numeric', day: 'numeric'},
  timePickerInput: {hour: 'numeric', minute: 'numeric'},
  monthYearLabel: {year: 'numeric', month: 'short'},
  dateA11yLabel: {year: 'numeric', month: 'long', day: 'numeric'},
  monthYearA11yLabel: {year: 'numeric', month: 'long'},
};


@NgModule({
  declarations: [],
  imports: [
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
  ], providers: [
    {provide: OWL_DATE_TIME_LOCALE, useValue: 'en-GB'},
    {provide: OWL_DATE_TIME_FORMATS, useValue: DATE_NATIVE_FORMATS},
  ],
  exports: [
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
  ],
})
export class DatePickerModule {
}
