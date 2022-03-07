import {Component, forwardRef} from '@angular/core';
import {AbstractField} from '../abstract-field';
import {NG_VALUE_ACCESSOR} from '@angular/forms';

@Component({
  selector: 'ngx-date-time-field',
  templateUrl: './date-time-field.component.html',
  styleUrls: ['./date-time-field.component.scss'],
  providers: [
    {provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => DateTimeFieldComponent), multi: true},
  ],
})
export class DateTimeFieldComponent extends AbstractField<Date> {

}
