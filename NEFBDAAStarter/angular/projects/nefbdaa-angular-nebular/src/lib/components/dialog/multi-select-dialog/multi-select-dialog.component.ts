import {Component} from '@angular/core';
import {Dialog} from '../dialog';
import {NbDialogRef} from '@nebular/theme';
import {SelectDialogConfigModel} from '../dialog-models';


@Component({
  selector: 'ngx-multi-select-dialog',
  templateUrl: './multi-select-dialog.component.html',
  styleUrls: ['./multi-select-dialog.component.scss'],
})
export class MultiSelectDialogComponent extends Dialog<MultiSelectDialogComponent, SelectDialogConfigModel> {

  constructor(protected readonly dialogRef: NbDialogRef<MultiSelectDialogComponent>) {
    super(dialogRef);
  }

}
