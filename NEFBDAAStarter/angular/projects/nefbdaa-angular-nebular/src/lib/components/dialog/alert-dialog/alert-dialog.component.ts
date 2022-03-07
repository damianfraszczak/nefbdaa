import {Component} from '@angular/core';
import {NbDialogRef} from '@nebular/theme';
import {Dialog} from '../dialog';
import {DialogConfigModel} from '../dialog-models';

@Component({
  selector: 'ngx-alert-dialog',
  templateUrl: './alert-dialog.component.html',
  styles: [`

    nb-card {
      min-width: 50vw;
      max-width: 90vw;
      max-height: 90vh;
      min-height: 30vh;

    }
  `],
})

export class AlertDialogComponent extends Dialog<AlertDialogComponent, DialogConfigModel> {

  constructor(
    protected readonly dialogRef: NbDialogRef<AlertDialogComponent>) {
    super(dialogRef);
  }

}

