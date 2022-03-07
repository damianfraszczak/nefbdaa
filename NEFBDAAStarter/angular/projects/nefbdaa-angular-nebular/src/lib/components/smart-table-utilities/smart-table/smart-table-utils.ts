import {formatDateTime, formatTime} from '../../../utils/date-utils';
import {getSelectedOptionsForField, mapSelectedOptionsToTextList} from '../../../utils/field-definitions-utils';
import {equalsIgnoringCase, fillMissingPropertiesClones, getOrDefault} from '../../../utils/utils';
import {ConfigService} from '../../../services/config.service';
import {SmartTableDS} from '../../../utils/service-data-source';
import {filter, map} from 'rxjs/operators';
import {Observable} from 'rxjs';
import {DatePipe, DecimalPipe} from '@angular/common';
import {FieldDefinitionModel} from '../../../models/dynamic-form/field-definition-model';
import {OptionsFieldDefinitionModel} from '../../../models/dynamic-form/options-field-definition-model';
import {constants} from '../../../shared-constants';
import {DateEditorComponent} from '../editors/date-editor.component';
import {DateTimeEditorComponent} from '../editors/date-time-editor.component';
import {TimeEditorComponent} from '../editors/time-editor.component';
import {ObservableSelectEditorComponent} from '../editors/observable-select-editor.component';
import {InputType} from '../../../models/dynamic-form/input-type-model';
import {CheckboxEditorComponent} from '../editors/checkbox-editor.component';
import {CustomRenderComponent} from './custom-renderer.component';
import {EditFieldDefinitionModel} from '../../../models/dynamic-form/edit-field-definition-model';
import {Router} from '@angular/router';
import {FilterRuleModel} from '../../../models/dynamic-form/filter-rule-model';


export const DEFAULT_TABLE_SETTINGS = {
  actions: {
    add: false,
    position: 'right', // left|right
    view: true,
  },
  extraToolbarActions: {
    add: true,
    search: true,
    export: false,
  },
  hideSubHeader: true,
  add: {
    addButtonContent: '<i class="nb-plus" title="Add"></i>',
    createButtonContent: '<i class="nb-checkmark"></i>',
    cancelButtonContent: '<i class="nb-close" title="Close"></i>',
    confirmCreate: true,
  },
  edit: {
    editButtonContent: '<i class="nb-edit" title="Edit"></i>',
    saveButtonContent: '<i class="nb-checkmark"></i>',
    cancelButtonContent: '<i class="nb-close" title="Close"></i>',
    confirmSave: true,
  },
  delete: {
    deleteButtonContent: '<i class="nb-trash" title="Delete"></i>',
    confirmDelete: true,
  },
  // inline|external|click-to-edit
  mode: 'external',
  modelName: 'demo',
  showFilters: true,
  showBulkUpdate: true,
};


export interface ITableConfig {
  modelType: string;
  title: string;
  addButtonEnabled?: boolean;
  editButtonEnabled?: boolean;
  deleteButtonEnabled?: boolean;
  showFilters?: boolean;
  showBulkUpdate?: boolean;
  showActionsToolbar?: boolean;
  // it gets router and edited object as params
  editFunction?: (router: Router, data: any) => void;
  queryParamsFunction?: () => { [key: string]: any };
  editModes?: Array<'MODAL' | 'PAGE' | 'INLINE'>;
  customTableActions?: { name: string, title: string, isVisible?: Function }[];
  extraToolbarActions?: { name: string, icon: string, title: string }[];
  extraToolbarActionsInOppositeDirection?: { name: string, icon: string, title: string }[];
  additionalSettings?: { name: string, value: any }[];
  selectionMode?: 'single' | 'multi';
  actionsPosition?: 'left' | 'right';
  referencedProperties?: Array<{ value: any | Function, field: string, type: string, operator?: string }>;
  defaultFiltersFunction?: () => FilterRuleModel[];
  filterType?: 'form' | 'basic' | 'advanced';
  hiddenColumns?: Array<string>;
  visibleColumns?: Array<string>;
  filterableColumns?: Array<string>;
  exportableColumns?: Array<string>;
  dataChangedListener?: (type: 'ADDED' | 'EDITED' | 'BULK_EDITED' | 'DELETED' | 'BULK_DELETED', obj: any) => void;
}

export function getDefaultSimpleTableSettings(modelType: string) {
  return getDefaultTableSettingsFromConfig({modelType, showBulkUpdate: false, title: ''});
}

export function getDefaultTableSettings(
  modelType: string,
  addButtonEnabled: boolean = true,
  editButtonEnabled: boolean = true,
  deleteButtonEnabled: boolean = true,
  customTableActions: { name: string, title: string, isVisible?: Function }[] = null,
  extraToolbarActions: { name: string, icon: string, title: string }[] = null,
  additionalSettings: { name: string, value: any }[] = null) {
  return getDefaultTableSettingsFromConfig({
    modelType,
    addButtonEnabled,
    editButtonEnabled,
    deleteButtonEnabled,
    customTableActions,
    extraToolbarActions,
    additionalSettings,
    title: '',
  });
}

export function getDefaultTableSettingsFromConfig(
  {
    modelType,
    addButtonEnabled = true,
    editButtonEnabled = true,
    deleteButtonEnabled = true,
    customTableActions = [],
    extraToolbarActions = [],
    extraToolbarActionsInOppositeDirection = [],
    additionalSettings = [],
    showBulkUpdate = false,
    showFilters = true,
    editFunction = null,
    queryParamsFunction = null,
    defaultFiltersFunction = () => [],
    editModes = ['PAGE'],
    showActionsToolbar = true,
    selectionMode = 'multi',
    actionsPosition = 'right',
    referencedProperties = null,
    filterType = 'basic',
    title = '',
    hiddenColumns = null,
    visibleColumns = null,
    filterableColumns = null,
    exportableColumns = null,
    dataChangedListener = null,
  }:
    ITableConfig,
) {
  const settings = {
    actions: {
      add: addButtonEnabled,
      edit: editButtonEnabled,
      delete: deleteButtonEnabled,
      custom: [],
      position: actionsPosition,
      columnTitle: 'Actions',
    },
    extraToolbarActions: {
      add: addButtonEnabled,
      addByLink: false,
    },
    customExtraActions: extraToolbarActions,
    columns: {},
    modelType,
    pager: {
      display: true,
      perPage: 10,
      perPageSelect: [10, 20, 50, 100],
    },
    extraToolbarActionsInOppositeDirection,
    queryParamsFunction,
    selectMode: selectionMode, // single|multi
    editModes,
    hideSubHeader: true,
    add: {
      addButtonContent: '<i class="nb-plus" title="Add"></i>',
      createButtonContent: '<i class="nb-checkmark" title="Save"></i>',
      cancelButtonContent: '<i class="nb-close" title="Close"></i>',
      confirmCreate: true,
    },
    // https://github.com/akveo/nebular/issues/20
    // https://erics.world/nebular-icons-reference/
    edit: {
      editButtonContent: '<i class="fas fa-pen" title="Edit"></i>',
      saveButtonContent: '<i class="nb-checkmark" title="Save"></i>',
      cancelButtonContent: '<i class="nb-close" title="Close"></i>',
      confirmSave: true,
    },
    // when inline and remote delete not work
    delete: {
      deleteButtonContent: '<i class="fas fa-trash" title="Delete"></i>',
      confirmDelete: true,

    },
    mode: 'external',
    modelName: modelType,
    showBulkUpdate,
    showFilters,
    filterExcludeFields: <Array<string>>[],
    showActionsToolbar,
    referencedProperties,
    filterType,
    title,
    defaultFiltersFunction,
    visibleColumns,
    hiddenColumns,
    editFunction,
    filterableColumns,
    exportableColumns,
    dataChangedListener,
  };
  if (customTableActions != null) {
    settings.actions.custom = customTableActions;
  }
  if (additionalSettings) {
    additionalSettings.forEach(settin => settings[settin.name] = settin.value);
  }

  if (editButtonEnabled) {
    const editInline = editModes.indexOf('INLINE') > -1;
    const editByPage = editModes.indexOf('PAGE') > -1;
    if (editInline) {
      settings.actions.custom.push({name: 'editInline', title: `<i class="fas fa-edit" title="Edit inline"></i>`});

    }
    if (editByPage) {
      settings.actions.custom.push({name: 'editByLink', title: `<i class="fas fa-pen" title="Edit"></i>`});
      settings.editFunction = editFunction;
      // hide edit button by dialog
      settings.actions.edit = false;
    }
  }

  // // delete not working in this mode
  // if (editInline && deleteButtonEnabled) {
  //   settings.actions.delete = false;
  //   settings.actions.custom.push({name: 'delete', title: `<i class="fas fa-trash" title="Delete"></i>`});
  // }

  return settings;
}


export function formatValue(
  fieldDefinition: FieldDefinitionModel,
  value: any,
  cellOptions: OptionsFieldDefinitionModel[] = null): string {
  if (value === null) {
    return '';
  }
  const stringVal = value + '';
  switch (fieldDefinition.inputType) {
    case InputType.NUMBER:
      return new DecimalPipe(constants.defaultLocale).transform(value, constants.doubleFormat);
    case InputType.INTEGER:
      return new DecimalPipe(constants.defaultLocale).transform(value, constants.integerFormat);
    case InputType.DATE:
      return new DatePipe(constants.defaultLocale).transform(value, constants.dateFormat);
      break;
    case InputType.TIME:
      return formatTime(value);
      break;
    case InputType.DATE_TIME:
      return formatDateTime(value);
      break;
    case InputType.FILE:
      return `<a href='${value}'  target='_blank' class='previewIcon'
          [ngStyle]='{'background-image': 'url(' + ${getFileIcon(value)} + ')'}'></a>`;
      break;
    case InputType.TEXT:
    case InputType.TEXT_AREA:
    case InputType.PASSWORD:
    case InputType.EMAIL:
    case InputType.URL:
    case InputType.TEL:
      break;
    case  InputType.SELECT:
    case  InputType.SELECT_MULTIPLE:
    case  InputType.RADIO:
      return mapSelectedOptionsToTextList(getSelectedOptionsForField(fieldDefinition, cellOptions, stringVal));
    case  InputType.BOOLEAN:
      if (value === true) {
        return 'Yes';
      } else {
        return 'No';
      }
      break;


  }
  return stringVal;
}

export function getTypeForField(fieldDefinition: FieldDefinitionModel) {
  switch (fieldDefinition.inputType) {
    case InputType.FILE:
      return 'html';
  }
  return 'custom';
}

export function getTableFilterTypeForField(fieldDefinition: FieldDefinitionModel) {
  switch (fieldDefinition.inputType) {
    case InputType.BOOLEAN:
      return 'checkbox';
    case InputType.DATE:
      return 'date';
    case InputType.DATE_TIME:
      return 'dateTime';
    case InputType.TIME:
      return 'time';
    case InputType.SELECT_MULTIPLE:
    case InputType.SELECT:
    case InputType.RADIO:
      return 'observable-select-list';
  }
  return 'input';
}


export function getTableEditorForField<T>(dataSource: SmartTableDS<T>,
                                          fieldDefinition: FieldDefinitionModel,
                                          allSettings: any) {
  let result: any = {type: 'text'};
  switch (fieldDefinition.inputType) {
    case InputType.BOOLEAN:
      result = {type: 'custom', component: CheckboxEditorComponent};
      break;
    case InputType.TEXT_AREA:
      result = {type: 'textarea'};
      break;
    case InputType.DATE:
      result = {type: 'custom', component: DateEditorComponent};
      break;
    case InputType.DATE_TIME:
      result = {type: 'custom', component: DateTimeEditorComponent};
      break;
    case InputType.TIME:
      result = {type: 'custom', component: TimeEditorComponent};
      break;
    case InputType.SELECT:
    case InputType.SELECT_MULTIPLE:
      result = {
        type: 'custom', component: ObservableSelectEditorComponent, config: {
          selectText: fieldDefinition.placeholder,
          true: 'Yes',
          false: 'No',
          fieldDefinition,
          listProvider: () => dataSource.getOptionsForField(fieldDefinition).pipe(
            map(x => {
              return x.filter(y => !y.hideFromFilter);
            })),
          modelType: allSettings.modelType,
        },
      };
      break;
  }
  return result;
}


export function getFileIcon(url: string) {
  if (url == null) {
    return '';
  }
  if (!(url.endsWith('png') || url.endsWith('jpg') || url.endsWith('jpeg'))) {
    return '/assets/file-icon.gif';
  } else {
    return url;
  }
}


export function filterIsVisible(field: FieldDefinitionModel, settings: any): boolean {
  const isVisible = isFieldVisibleBasedOnSettings(field, settings);
  let canBeFitlered = settings.showFilters &&
    settings.filterExcludeFields.find(x => x === field.parameterName) == null &&
    isFilterable(field);
  if (settings && settings.filterableColumns) {
    canBeFitlered = settings.filterableColumns.filter(x => equalsIgnoringCase(x, field.parameterName)).length === 1;
  }

  return canBeFitlered;
}

export function isFilterable(field: EditFieldDefinitionModel): boolean {

  return field.filterable && !(
    field.inputType === InputType.FILE ||
    field.inputType === InputType.IMAGE ||
    field.inputType === InputType.HEART ||
    field.inputType === InputType.PASSWORD
  );
}

export function isEditable(field: EditFieldDefinitionModel): boolean {
  return field.editable && field.editableOnEditForm && field.displayOnUpdateForm && !(
    field.inputType === InputType.FILE ||
    field.inputType === InputType.IMAGE ||
    field.inputType === InputType.HEART ||
    field.inputType === InputType.PASSWORD
  );
}

function isFieldVisibleBasedOnSettings(field: FieldDefinitionModel, settings: any) {
  return isFieldVisible(field, getOrDefault(settings.visibleColumns, []), getOrDefault(settings.visibleColumns, []));
}

function isFieldVisible(field: FieldDefinitionModel, visibleColumns: Array<string>, hiddenColumns: Array<string>) {
  if (visibleColumns.length === 0 && hiddenColumns.length === 0) {
    return field.display;
  }
  const isInVisible = visibleColumns.length === 0 ? field.display : visibleColumns
    .find(x => equalsIgnoringCase(x, field.parameterName)) != null;
  const isInHidden = hiddenColumns.find(x => equalsIgnoringCase(x, field.parameterName)) != null;
  if (isInHidden) {
    return false;
  }
  return isInVisible;
}

export function initDataSource<T>(configService: ConfigService, dataSource: SmartTableDS<T>, allSettings: any):
  Observable<any> {
  const tableSettings: any = {
    columns: {},
  };
  return configService.getModelFieldDefinition(allSettings.modelType).pipe(map(fieldsDefinitions => {
    dataSource.fieldsDefinitions = fieldsDefinitions;
    const visibleColumns = getOrDefault(allSettings.visibleColumns, []);
    const hiddenColumns = getOrDefault(allSettings.hiddenColumns, []);
    // TODO move to model
    dataSource.fieldsDefinitions
      // .filter(x => x.inputType !== InputType.SELECT_MULTIPLE && (isFieldVisible(x, visibleColumns, hiddenColumns)))

      .forEach(field => {
        tableSettings.columns[field.parameterName] = {
          title: field.tableTitle,
          show: field.inputType !== InputType.SELECT_MULTIPLE &&
            (isFieldVisible(field, visibleColumns, hiddenColumns)),
          renderComponent: CustomRenderComponent,
          valuePrepareFunction: (value) => formatValue(field, value, dataSource.cellOptions),
          type: getTypeForField(field),
          sort: isFilterable(field),
          isFilterable: isFilterable(field),
          width: field.tableWidth,
          filter:
            filterIsVisible(field, allSettings) ? {
              type: getTableFilterTypeForField(field),
              config: {
                selectText: field.placeholder,
                true: 'Yes',
                false: 'No',
                listProvider: () => dataSource.getOptionsForField(field).pipe(
                  map(x => {
                    return x.filter(y => !y.hideFromFilter);
                  }),
                ),
                fieldDefinition: field,
                modelType: allSettings.modelType,
              },
            } : false,
          editor: getTableEditorForField(dataSource, field, allSettings),
          editable: isEditable(field),
          isEditable: isEditable(field),
        };
      });
    const result = fillMissingPropertiesClones(tableSettings, allSettings);
    if (result == null) {
      console.error('Error with initDataSource for ' + allSettings.modelType + '  ' + allSettings);
    }

    return result;
  }));
}
