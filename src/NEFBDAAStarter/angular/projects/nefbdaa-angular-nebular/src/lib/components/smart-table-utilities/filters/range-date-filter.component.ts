import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {FormControl} from '@angular/forms';
import {DefaultFilter} from 'ng2-smart-table';
import {debounceTime, distinctUntilChanged} from 'rxjs/operators';

@Component({
  selector: 'ngx-range-date-filter',
  template: `
    <owl-date-time #dp [pickerType]="'calendar'"></owl-date-time>
    <input
      [ngClass]="inputClass" [owlDateTimeTrigger]="dp" [owlDateTime]="dp" autocomplete="off"
      [formControl]="inputControl" [selectMode]="'single'"
      class="form-control"
      type="text"
      placeholder="From {{ column.title }}"/>

    <owl-date-time #dp2 [pickerType]="'calendar'"></owl-date-time>
    <input
      [ngClass]="inputClass" [owlDateTimeTrigger]="dp2" [owlDateTime]="dp2" autocomplete="off"
      [formControl]="inputControl2" [selectMode]="'single'"
      class="form-control"
      type="text"
      placeholder="To {{ column.title }}"/>

  `,
  styles: [],
})
export class RangeDateFilterComponent extends DefaultFilter implements OnInit, OnChanges {

  inputControl = new FormControl();
  inputControl2 = new FormControl();
  query: any;

  constructor() {
    super();
  }

  ngOnInit() {
    if (this.query) {
      this.bindQuery();
    }
    this.controlsSubscribe(this.inputControl);
    this.controlsSubscribe(this.inputControl2);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.query) {
      this.bindQuery();
    }
  }

  private controlsSubscribe(inputControl: FormControl) {
    inputControl.valueChanges
      .pipe(
        distinctUntilChanged(),
        debounceTime(this.delay),
      )
      .subscribe((value: string) => {
        this.query = [this.inputControl.value, this.inputControl2.value];
        this.setFilter();
      });
  }

  private bindQuery() {
    if (Array.isArray(this.query)) {
      this.inputControl.setValue(this.query[0]);
      if (this.query.length > 1) {
      }
      this.inputControl2.setValue(this.query[1]);
    }

  }
}
