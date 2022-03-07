import {Component} from '@angular/core';
import {NbDialogRef} from '@nebular/theme';
import {Dialog} from '../dialog';
import {DialogConfigModel} from '../dialog-models';

@Component({
  selector: 'ngx-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styles: [`

.perfect-scroll {
  min-width: 50vw;
  max-width: 90vw;
  max-height: 90vh;
  min-height: 30vh;

}
  `],
})
export class ConfirmationDialogComponent extends Dialog<ConfirmationDialogComponent, DialogConfigModel> {

  constructor(
    protected readonly dialogRef: NbDialogRef<ConfirmationDialogComponent>) {
    super(dialogRef);
  }

}
