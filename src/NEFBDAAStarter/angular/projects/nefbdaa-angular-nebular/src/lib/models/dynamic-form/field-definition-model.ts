/* tslint:disable */
import {FormLayout} from './dynamic-field-enums';
import {InputType} from './input-type-model';

export interface FieldDefinitionModel {
  placeholder?: string;
  tableTitle?: string;
  tableWidth?: string;
  parameterName?: string;
  filterPropertyName?: string;
  filterSelectMultiple: boolean;
  filterQueryOperator?: string;
  display?: boolean;
  order?: number;
  filterOrder?: number;
  exportOrder?: number;
  inputType?: InputType;
  queryType?: string;
  groupName?: string;
  modelType?: string;
  filterable?: boolean;
  editable?: boolean;
  layout: FormLayout;
  editableOnCreateForm?: boolean;
  editableOnEditForm?: boolean;
}
