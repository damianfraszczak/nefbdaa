import {Component} from '@angular/core';
import {DefaultEditor} from 'ng2-smart-table';

@Component({
  selector: 'ngx-checkbox-editor',
  template: `
    <nb-checkbox
      [(ngModel)]="cell.newValue"

      (click)="onClick.emit($event)"
      (keydown.enter)="onEdited.emit($event)"
      (keydown.esc)="onStopEditing.emit()"
    >
    </nb-checkbox>
  `,
  styles: [
    'nb-checkbox { text-align: center;display: block; }',
  ],
})
export class CheckboxEditorComponent extends DefaultEditor {

  constructor() {
    super();
  }


}
