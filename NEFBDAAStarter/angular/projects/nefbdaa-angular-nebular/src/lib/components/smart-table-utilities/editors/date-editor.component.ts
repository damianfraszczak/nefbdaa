import {Component} from '@angular/core';
import {DefaultEditor} from 'ng2-smart-table';

@Component({
  selector: 'ngx-table-date-editor',
  template: `
    <owl-date-time #dp [pickerType]="'calendar'" ></owl-date-time>
    <input
      [ngClass]="inputClass" [owlDateTimeTrigger]="dp" [owlDateTime]="dp" autocomplete="off"
      [(ngModel)]="cell.newValue"
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
export class DateEditorComponent extends DefaultEditor {

  constructor() {
    super();
  }
}
