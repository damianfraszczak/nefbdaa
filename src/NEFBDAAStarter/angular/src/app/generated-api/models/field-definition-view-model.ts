/* tslint:disable */
import { InputType } from './input-type';
export interface FieldDefinitionViewModel {
  placeholder?: string;
  tableTitle?: string;
  helpText?: string;
  groupName?: string;
  parameterName?: string;
  display?: boolean;
  filterable?: boolean;
  editable?: boolean;
  editableOnCreateForm?: boolean;
  editableOnEditForm?: boolean;
  exportable?: boolean;
  order?: number;
  filterOrder?: number;
  exportOrder?: number;
  inputType?: InputType;
  queryType?: string;
  modelType?: string;
  filterPropertyName?: string;
  filterSelectMultiple?: boolean;
  tableWidth?: string;
  min?: string;
  max?: string;
}
