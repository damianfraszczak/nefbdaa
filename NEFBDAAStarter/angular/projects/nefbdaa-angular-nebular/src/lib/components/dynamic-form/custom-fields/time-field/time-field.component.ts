import {Component, forwardRef} from '@angular/core';
import {AbstractField} from '../abstract-field';
import {NG_VALUE_ACCESSOR} from '@angular/forms';

@Component({
  selector: 'ngx-time-field',
  templateUrl: './time-field.component.html',
  styleUrls: ['./time-field.component.scss'],
  providers: [
    {provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => TimeFieldComponent), multi: true},
  ],
})
export class TimeFieldComponent extends AbstractField<Date> {

}
