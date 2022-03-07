import {Component, OnInit, ViewChild} from '@angular/core';
import {Dialog} from '../dialog';
import {NbDialogRef} from '@nebular/theme';
import {FormModel} from '../../../models/dynamic-form/edit-object-model';
import {DynamicFormComponent} from '../../dynamic-form/dynamic-form/dynamic-form.component';
import {FormMode} from '../../../models/dynamic-form/dynamic-field-enums';
import {equalsIgnoringCase} from '../../../utils/utils';
import {FormArray, FormGroup} from '@angular/forms';
import {DialogConfigModel} from '../dialog-models';

@Component({
  selector: 'ngx-dynamic-edit-dialog',
  templateUrl: './dynamic-edit-dialog.component.html',
  styleUrls: ['./dynamic-edit-dialog.component.scss'],
})
export class DynamicEditDialogComponent
  extends Dialog<DynamicEditDialogComponent, EditDialogData<any>> implements OnInit {
  @ViewChild(DynamicFormComponent, {static: false})
  ngxForm: DynamicFormComponent<any>;

  constructor(protected readonly dialogRef: NbDialogRef<DynamicEditDialogComponent>) {
    super(dialogRef);
  }

  ngOnInit() {

  }

  isEditMode(): boolean {
    return !equalsIgnoringCase(this.dialogContent.formModel.formConfig.formMode, FormMode.VIEW);
  }

  getAllErrors(form: FormGroup | FormArray): { [key: string]: any; } | null {
    let hasError = false;
    const result = Object.keys(form.controls).reduce((acc, key) => {
      const control = form.get(key);
      const errors = (control instanceof FormGroup || control instanceof FormArray)
        ? this.getAllErrors(control)
        : control.errors;
      if (errors) {
        acc[key] = errors;
        hasError = true;
      }
      return acc;
    }, {} as { [key: string]: any; });
    return hasError ? result : null;
  }
}

export interface EditDialogData<T> extends DialogConfigModel {
  formModel: FormModel<T>;
}


