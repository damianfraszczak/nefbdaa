import {Component} from '@angular/core';
import {Dialog} from '../dialog';
import {NbDialogRef} from '@nebular/theme';

import '../../../utils/ckeditor.loader';
import 'ckeditor';
import {InputDialogConfigModel} from '../dialog-models';

@Component({
  selector: 'ngx-text-editor-dialog',
  templateUrl: './text-editor-dialog.component.html',
  styles: [`

nb-card {
  min-width: 50vw;
  max-width: 90vw;
  max-height: 90vh;
  min-height: 30vh;

}
  `],
})
export class TextEditorDialogComponent extends Dialog<TextEditorDialogComponent, InputDialogConfigModel> {

  constructor(protected readonly dialogRef: NbDialogRef<TextEditorDialogComponent>) {
    super(dialogRef);
  }

}
