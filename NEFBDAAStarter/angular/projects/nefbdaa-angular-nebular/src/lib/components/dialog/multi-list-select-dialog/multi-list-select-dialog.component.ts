import {Component} from '@angular/core';
import {Dialog} from '../dialog';
import {NbDialogRef} from '@nebular/theme';
import {ListSelectDialogConfigModel} from '../dialog-models';
import {SelectFieldOptionModel} from '../../../models/dynamic-form/select-field-option-model';


@Component({
  selector: 'ngx-multi-list-select-dialog',
  templateUrl: './multi-list-select-dialog.component.html',
  styleUrls: ['./multi-list-select-dialog.component.scss'],
})
export class MultiListSelectDialogComponent
  extends Dialog<MultiListSelectDialogComponent, ListSelectDialogConfigModel> {

  constructor(protected readonly dialogRef: NbDialogRef<MultiListSelectDialogComponent>) {
    super(dialogRef);
  }

  getName(item: SelectFieldOptionModel) {
    return this.getFromItem(item, this.dialogContent.namePropertyName) || item.optionText;
  }

  getTitle(item: SelectFieldOptionModel) {
    return this.getFromItem(item, this.dialogContent.titlePropertyName) || item.optionText;
  }

  isSelected(item: SelectFieldOptionModel): boolean {
    if (Array.isArray(this.dialogContent.selected)) {
      return this.dialogContent.selected.indexOf(item.optionId) > -1;
    } else {
      return this.dialogContent.selected === item.optionId;
    }
  }

  toogleSelection(item: SelectFieldOptionModel) {
    if (Array.isArray(this.dialogContent.selected)) {
      const currentIndex = this.dialogContent.selected.indexOf(item.optionId);
      if (currentIndex > -1) {
        this.dialogContent.selected.splice(currentIndex, 1);
      } else {
        this.dialogContent.selected.push(item.optionId);
      }
    } else {
      this.dialogContent.selected = item.optionId;
    }


  }

  private getFromItem(item: SelectFieldOptionModel, propName: string) {
    if (propName) {
      let title = item[propName];
      if (!title) {
        title = item.optionAdditionalInfo[propName];
      }
      return title;
    } else {
      return null;
    }

  }
}
