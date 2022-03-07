import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {FormControl} from '@angular/forms';
import {DefaultFilter} from 'ng2-smart-table';
import {debounceTime, distinctUntilChanged} from 'rxjs/operators';

@Component({
  selector: 'ngx-date-filter',
  template: `
    <owl-date-time #dp [pickerType]="'calendar'"></owl-date-time>
    <input
      [ngClass]="inputClass" [owlDateTimeTrigger]="dp" [owlDateTime]="dp" autocomplete="off"
      [formControl]="inputControl"
      class="form-control"
      type="text"
      placeholder="{{ column.title }}"/>
  `,
  styles: [],
})
export class DateFilterComponent extends DefaultFilter implements OnInit, OnChanges {


  inputControl = new FormControl();

  constructor() {
    super();
  }

  ngOnInit() {
    if (this.query) {
      this.inputControl.setValue(this.query);
    }
    this.inputControl.valueChanges
      .pipe(
        distinctUntilChanged(),
        debounceTime(this.delay),
      )
      .subscribe((value: string) => {
        this.query = this.inputControl.value;
        this.setFilter();
      });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.query) {
      this.inputControl.setValue(this.query);
    }
  }
}
