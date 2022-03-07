import {Component} from '@angular/core';
import {Dialog} from '../dialog';
import {NbDialogRef} from '@nebular/theme';
import {ConfiguredInputDialogConfigModel} from '../dialog-models';

@Component({
  selector: 'ngx-textarea-editor-dialog',
  templateUrl: './textarea-editor-dialog.component.html',
  styles: [`

nb-card {
  min-width: 50vw;
  max-width: 90vw;
  max-height: 90vh;
  min-height: 30vh;

}
  `],
})
export class TextareaEditorDialogComponent
  extends Dialog<TextareaEditorDialogComponent, ConfiguredInputDialogConfigModel> {

  constructor(protected readonly dialogRef: NbDialogRef<TextareaEditorDialogComponent>) {
    super(dialogRef);
  }

}
