import {Component} from '@angular/core';
import {Dialog} from '../dialog';
import {NbDialogRef} from '@nebular/theme';
import {InputDialogConfigModel} from '../dialog-models';

@Component({
  selector: 'ngx-password-dialog',
  templateUrl: './password-dialog.component.html',
  styles: ['.error-message {\n' +
  '  color: red;\n' +
  '}',
      `
nb-card {
  min-width: 50vw;
  max-width: 90vw;
  max-height: 90vh;
  min-height: 30vh;

}`,
  ],
})
export class PasswordDialogComponent extends Dialog<PasswordDialogComponent, InputDialogConfigModel> {

  confirmPassword = '';

  constructor(
    protected readonly dialogRef: NbDialogRef<PasswordDialogComponent>) {
    super(dialogRef);
  }

}

