import {Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {DialogService} from '../../dialog/dialog.service';
import {SmartTableDS} from '../../../utils/service-data-source';
import {fillMissingPropertiesClones} from '../../../utils/utils';
import {EditDialogData} from '../../dialog/dynamic-edit-dialog/dynamic-edit-dialog.component';
import {ConfigService} from '../../../services/config.service';
import {FormMode} from '../../../models/dynamic-form/dynamic-field-enums';
import {addIfNotExists, removeIfExists} from '../../../utils/filters';
import {DEFAULT_TABLE_SETTINGS, initDataSource} from './smart-table-utils';
import {ɵbo as Ng2SmartTableComponent} from 'ng2-smart-table';

@Component({
  selector: 'ngx-smart-table',
  templateUrl: './smart-table.component.html',
  styleUrls: ['./smart-table.component.scss'],
})
export class SmartTableComponent<T> {

  allSettings: any;
  tableVisible = true;
  showActionsToolbar = true;
  selectedRows = Array<T>();
  globalSelectedRows = Array<T>();
  @Input()
  validator: (value: T) => Promise<T>;
  @Output()
  custom = new EventEmitter();
  @Output()
  selectedItems = new EventEmitter<Array<T>>();
  private settingsInitialized: boolean;

  @ViewChild('table', {static: false})
  table: Ng2SmartTableComponent;

  constructor(
    private readonly configService: ConfigService,
    private readonly dialogService: DialogService,
  ) {
  }

  get settings() {
    return this.allSettings;
  }

  @Input()
  set settings(settings: any) {
    this.settingsInitialized = true;
    this.allSettings = fillMissingPropertiesClones(settings, DEFAULT_TABLE_SETTINGS);
    this.initTableSettings();
  }

  _dataSource: SmartTableDS<T>;

  get dataSource() {
    return this._dataSource;
  }

  @Input()
  set dataSource(dataSource: SmartTableDS<T>) {
    this._dataSource = dataSource;
    this.initTableSettings();
  }


  public refresh() {
    this.tableVisible = false;
    setTimeout(() => this.tableVisible = true);
  }

  showCreationForm() {
    // load additional params to object from query

    this.showDynamicForm({}, FormMode.ADD, (dialogData) => {
      this.dataSource.add(dialogData.formModel.object).then(() => this.dataSource.refresh());
    });
  }

  showUpdateForm(elementToUpdate: T) {
    this.showDynamicForm(elementToUpdate, FormMode.EDIT, (dialogData) => {
      this.dataSource.update(elementToUpdate, dialogData.formModel.object).then(() => this.dataSource.refresh());
    });
  }

  validateRow({confirm, newData}): void {
    confirm.resolve();
  }

  showViewForm(elementToView: T) {
    this.showDynamicForm(elementToView, FormMode.VIEW, (editData) => {
    });
  }

  editInline(data: any) {
    const row = this.table.grid.dataSet.findRowByData(data);
    this.table.grid.edit(row);
  }

  showDeleteForm(elementToDelete: T) {
    this.dialogService.showConfirmDeletionDialog({
      message: elementToDelete['optionText'],
    }).onConfirm(() => {
      this.dataSource.remove(elementToDelete).then(() => this.dataSource.refresh());
    });
  }

  showDynamicForm(object: any, formMode: FormMode, action: (dialogData: EditDialogData<any>) => void) {
    let dialogName = '';
    switch (formMode) {
      case FormMode.ADD:
        dialogName = 'Create';
        break;
      case FormMode.EDIT:
        dialogName = 'Edit';
        break;
      case FormMode.VIEW:
        dialogName = 'View';
        break;
    }

    this.dataSource.provider.editor(object).subscribe(formModel => {
      this.dialogService.showDynamicEditorDialog({
        formModel,
        header: dialogName,
      }).onConfirm(action);

    });
  }

  userRowSelect(selected: Array<T>) {
    // user clear all
    if (selected.length === 0) {
      this.selectedRows.forEach(x => removeIfExists(x, this.globalSelectedRows));
    }
    this.selectedRows.length = 0;
    this.selectedRows.push(...selected);
    this.selectedItems.emit(this.selectedRows);
  }


  private initTableSettings() {
    if (this.dataSource != null && this.settingsInitialized) {
      const initDataSourceResult = initDataSource(this.configService, this.dataSource, this.allSettings);
      if (initDataSourceResult == null) {
        console.log('Error with initTableSettings ' + this.allSettings.modelType);
      } else {
        initDataSourceResult.subscribe((settings) => {
          this.allSettings = settings;
          this.showActionsToolbar = settings['showActionsToolbar'] == null ? true : settings['showActionsToolbar'];
          this.dataSource.loadingSubject$.subscribe(() => {
            this.selectedRows.forEach(x => addIfNotExists(x, this.globalSelectedRows));
          });
          this.refresh();
        });
      }
    }
  }
}
