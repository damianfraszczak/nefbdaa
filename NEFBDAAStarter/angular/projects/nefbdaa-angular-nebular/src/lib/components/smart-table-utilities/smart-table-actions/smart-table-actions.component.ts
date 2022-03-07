import {ChangeDetectorRef, Component, EventEmitter, Inject, Input, Output} from '@angular/core';
import {DialogService} from '../../dialog/dialog.service';
import {IDataSourceDataProvider} from '../../../utils/service-data-source';
import {FormModel} from '../../../models/dynamic-form/edit-object-model';
import {FilterRuleModel} from '../../../models/dynamic-form/filter-rule-model';
import {removeIfExists} from '../../../utils/filters';
import {Router} from '@angular/router';
import {MODEL_MAPPER_SERVICE} from '../../../shared-constants';
import {ModelMapperService} from '../../../services/api/model-mapper-service';
import {getFunctionResultOrVal} from '../../dynamic-form/expression-utils';

// @ts-ignore
@Component({
  selector: 'ngx-smart-table-actions',
  templateUrl: './smart-table-actions.component.html',
  styleUrls: ['./smart-table-actions.component.scss'],
})
export class SmartTableActionsComponent {

  @Input()
  table;
  @Input()
  custom = new EventEmitter();
  @Input()
  selectedRows: Array<any>;
  @Input()
  globalSelectedRows: Array<any>;
  @Output()
  createNewClick = new EventEmitter();
  filterModel: FormModel<any>;
  bulkUpdateModel: FormModel<any>;

  filtersVisible = false;

  constructor(
    private readonly dialogService: DialogService,
    private cdRef: ChangeDetectorRef,
    private router: Router,
    @Inject(MODEL_MAPPER_SERVICE)
    private modelMapperService: ModelMapperService) {

  }

  get settings() {
    return this.table && this.table.grid && this.table.grid.settings || {};
  }

  toggleCreationForm() {
    if (this.settings.editModes.indexOf('PAGE') > -1) {
      const queryParams = this.settings.queryParamsFunction ? this.settings.queryParamsFunction() : {};
      if (this.settings.referencedProperties) {
        this.settings.referencedProperties.forEach(referencedProperty => {
          queryParams[referencedProperty.field] = getFunctionResultOrVal(referencedProperty.value);
        });
      }

      if (this.settings.editFunction) {
        this.settings.editFunction(this.router, {});
      } else {
        this.modelMapperService.navigateToDetails(this.settings.modelType,
          -1, queryParams,
        );
      }
    } else if (this.createNewClick != null) {
      this.createNewClick.emit();
    } else {
      this.table.grid.createFormShown = !this.table.grid.createFormShown;
    }
  }


  toggleFilters() {
    this.filtersVisible = !this.filtersVisible;
  }

  executeQuery(rule: FilterRuleModel) {
    this.table.source.config = rule;
    this.table.source.refresh();
  }

  bulkUpdate() {
    this.dialogService.showTableSelectEditorDialog({
      status: 'primary',
      message: 'Are you sure to update all visible rows',
      header: 'Confirmation required',
      modelType: this.settings.modelType,
      selected: [],
      cellOptions: this.table.source.cellOptions,
      data: this.getSelectedRows(),
    }).onConfirm((result) => {
      this.table.source.bulkUpdate(this.bulkUpdateModel.object, result.selected);
      this.clearSelection();
    });
  }

  showSelectedRows() {
    this.dialogService.showTableSelectEditorDialog({
      status: 'primary',
      header: 'Selected rows',
      modelType: this.settings.modelType,
      selected: [],
      cellOptions: this.table.source.cellOptions,
      data: this.getSelectedRows(),
      deleteButtonHidden: true,
    }).onConfirm((result) => {

    });
  }

  bulkDelete() {
    this.dialogService.showTableSelectEditorDialog({
      status: 'danger',
      message: 'Are you sure to delete all visible rows',
      header: 'Confirmation required',
      modelType: this.settings.modelType,
      selected: [],
      cellOptions: this.table.source.cellOptions,
      data: this.getSelectedRows(),
    }).onConfirm((result) => {
      this.table.source.bulkDelete(this.getSelectedRows());
      this.clearSelection();
    });
  }

  exportData() {
    const selectedRows = this.getSelectedRows();
    if (selectedRows.length === 0) {
      this.table.source.exportData('CSV', [], this.table.settings);
    } else {
      this.dialogService.showTableSelectEditorDialog({
        status: 'primary',
        message: 'Following rows will be exported',
        header: 'Confirmation required',
        modelType: this.settings.modelType,
        selected: [],
        cellOptions: this.table.source.cellOptions,
        data: selectedRows,
      }).onConfirm((result) => {
        this.table.source.exportData('CSV', result.selected, this.table.settings);
        this.clearSelection();
      });
    }

  }

  loadFormModel() {
    if (this.filterModel == null) {
      const provider = this.table.source.provider as IDataSourceDataProvider<any>;
      provider.editor({}).subscribe(formModel => {
        formModel.formConfig.formGroups.forEach(x => x.editFields.forEach(field => field.validators = []));
        this.filterModel = JSON.parse(JSON.stringify(formModel));
        this.bulkUpdateModel = this.prepareBulkUpdateModel(JSON.parse(JSON.stringify(formModel)));
        this.cdRef.detectChanges();
      });
    }
  }

  private getSelectedRows(): any[] {
    return [...this.globalSelectedRows, ...this.selectedRows];
  }

  private clearSelection() {
    this.globalSelectedRows.length = 0;
    this.selectedRows.length = 0;
  }

  private prepareBulkUpdateModel(formModel: FormModel<any>) {
    formModel.formConfig.formGroups.forEach(fg => {
      const toRemove = [];
      fg.editFields.forEach(field => {
        if (!field.editable && !field.filterable) {
          toRemove.push(field);
        }
        field.editable = true;
      });
      toRemove.forEach(field => removeIfExists(field, fg.editFields));
    });

    return formModel;
  }

  getFilterColumns() {
    if (this.table && this.table.grid) {
      const columns = this.table.grid.getColumns().filter(c => c.filter);
      columns.sort((x, y) => {
        const configX = x.getFilterConfig();
        const configY = y.getFilterConfig();
        return configX.fieldDefinition.filterOrder - configY.fieldDefinition.filterOrder;
      });
      return columns;
    }


    return null;
  }
}
