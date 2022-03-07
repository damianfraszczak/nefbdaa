import {Component, OnInit} from '@angular/core';
import {DefaultEditor} from 'ng2-smart-table';
import {FormControl} from '@angular/forms';
import {distinctUntilChanged} from 'rxjs/operators';
import {formatDateTimeForApi} from '../../../utils/date-utils';

@Component({
  selector: 'ngx-table-time-editor',
  template: `
    <owl-date-time #tp [pickerType]="'timer'"></owl-date-time>
    <input
      [ngClass]="inputClass" [owlDateTimeTrigger]="tp" [owlDateTime]="tp" autocomplete="off"
      [formControl]="inputControl"
      class="form-control"
      type="text"
      [placeholder]="cell.getTitle()"
      [disabled]="!cell.isEditable()"
      (click)="onClick.emit($event)"
      (keydown.enter)="onEdited.emit($event)"
      (keydown.esc)="onStopEditing.emit()"/>
  `,
  styles: [],
})
export class TimeEditorComponent extends DefaultEditor implements OnInit {

  inputControl = new FormControl();

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.inputControl.valueChanges
      .pipe(
        distinctUntilChanged(),
      )
      .subscribe((value: any) => {
        this.cell.newValue = formatDateTimeForApi(value)
      });
    this.inputControl.setValue(this.cell.newValue);
  }
}
