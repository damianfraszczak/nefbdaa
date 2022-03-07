import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {FormControl} from '@angular/forms';
import {DefaultFilter} from 'ng2-smart-table';
import {debounceTime, distinctUntilChanged} from 'rxjs/operators';

@Component({
  selector: 'ngx-checkbox-filter',
  template: `

    <div class="div-check">
      <nb-checkbox
        [formControl]="inputControl"
      >
      </nb-checkbox>
    </div>
  `,
  styles: [
    'nb-checkbox { margin-right: 0.2rem}',
    '.div-check{    padding: 0.4375rem 1.125rem;}',
  ],
})
export class CheckboxFilterComponent extends DefaultFilter implements OnInit, OnChanges {

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
