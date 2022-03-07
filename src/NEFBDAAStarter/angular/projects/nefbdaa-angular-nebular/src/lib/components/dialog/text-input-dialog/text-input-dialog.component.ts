import {Component, OnInit} from '@angular/core';
import {Dialog} from '../dialog';
import {NbDialogRef} from '@nebular/theme';
import {ConfiguredInputDialogConfigModel} from '../dialog-models';


@Component({
  selector: 'ngx-text-input-dialog',
  templateUrl: './text-input-dialog.component.html',
  styles: [`

nb-card {
  min-width: 50vw;
  max-width: 90vw;
  max-height: 90vh;
  min-height: 30vh;

}
  `],
})
export class TextInputDialogComponent
  extends Dialog<TextInputDialogComponent, ConfiguredInputDialogConfigModel> implements OnInit {

  constructor(protected readonly dialogRef: NbDialogRef<TextInputDialogComponent>) {
    super(dialogRef);
  }

  ngOnInit() {
  }

}
