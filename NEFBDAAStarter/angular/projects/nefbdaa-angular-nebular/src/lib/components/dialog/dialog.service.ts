import {Injectable} from '@angular/core';
import {NbDialogService} from '@nebular/theme';
import {TextEditorDialogComponent} from './text-editor-dialog/text-editor-dialog.component';
import {TextareaEditorDialogComponent} from './textarea-editor-dialog/textarea-editor-dialog.component';
import {MultiSelectDialogComponent} from './multi-select-dialog/multi-select-dialog.component';
import {DynamicEditDialogComponent, EditDialogData} from './dynamic-edit-dialog/dynamic-edit-dialog.component';
import {TextInputDialogComponent} from './text-input-dialog/text-input-dialog.component';
import {ConfirmationDialogComponent} from './confirmation-dialog/confirmation-dialog.component';
import {AlertDialogComponent} from './alert-dialog/alert-dialog.component';
import {PasswordDialogComponent} from './password-dialog/password-dialog.component';
import {TableSelectDialogComponent} from './table-select-dialog/table-select-dialog.component';
import {
  ConfiguredInputDialogConfigModel,
  DialogConfigModel,
  InputDialogConfigModel, ListSelectDialogConfigModel,
  SelectDialogConfigModel,
  TableSelectDialogModel,
} from './dialog-models';
import {DeleteItemDialogComponent} from './delete/delete-item-dialog.component';
import {MultiListSelectDialogComponent} from './multi-list-select-dialog/multi-list-select-dialog.component';

@Injectable()
export class DialogService {

  constructor(private readonly nbDialogService: NbDialogService) {
  }


  public showConfirmDeletionDialog(dialogContent: DialogConfigModel): DeleteItemDialogComponent {
    return this.nbDialogService
      .open(DeleteItemDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as DeleteItemDialogComponent;
  }

  public showTextEditorDialog(dialogContent: InputDialogConfigModel): TextEditorDialogComponent {
    return this.nbDialogService
      .open(TextEditorDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as TextEditorDialogComponent;
  }

  public showInputEditorDialog(dialogContent?: ConfiguredInputDialogConfigModel): TextInputDialogComponent {
    return this.nbDialogService
      .open(TextInputDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as TextInputDialogComponent;
  }

  public showTextareaEditorDialog(dialogContent: ConfiguredInputDialogConfigModel): TextareaEditorDialogComponent {
    return this.nbDialogService
      .open(TextareaEditorDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as TextareaEditorDialogComponent;
  }

  public showPasswordDialog(dialogContent: InputDialogConfigModel): PasswordDialogComponent {
    return this.nbDialogService
      .open(PasswordDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as PasswordDialogComponent;
  }

  public showSelectEditorDialog(dialogContent: SelectDialogConfigModel): MultiSelectDialogComponent {
    return this.nbDialogService
      .open(MultiSelectDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as MultiSelectDialogComponent;
  }

  public showSelectListEditorDialog(dialogContent: ListSelectDialogConfigModel): MultiListSelectDialogComponent {
    return this.nbDialogService
      .open(MultiListSelectDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as MultiListSelectDialogComponent;
  }

  public showTableSelectEditorDialog(dialogContent: TableSelectDialogModel): TableSelectDialogComponent {
    return this.nbDialogService
      .open(TableSelectDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as TableSelectDialogComponent;
  }

  public showDynamicEditorDialog(dialogContent: EditDialogData<any>): DynamicEditDialogComponent {
    if (dialogContent.formModel.formConfig.formGroups.length === 1) {
      dialogContent.formModel.formConfig.formGroupLayoutComponent = 'NONE';
    }

    return this.nbDialogService
      .open(DynamicEditDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as DynamicEditDialogComponent;
  }

  public showConfirmationDialog(dialogContent: DialogConfigModel): ConfirmationDialogComponent {
    return this.nbDialogService
      .open(ConfirmationDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as ConfirmationDialogComponent;
  }

  public showAlertDialog(dialogContent: DialogConfigModel): AlertDialogComponent {
    return this.nbDialogService
      .open(AlertDialogComponent, this.getDialogConfig(dialogContent))
      .componentRef.instance as AlertDialogComponent;
  }

  private getDialogConfig(dialogContent: any) {
    return {context: {dialogContent}, autoFocus: false};
  }
}

