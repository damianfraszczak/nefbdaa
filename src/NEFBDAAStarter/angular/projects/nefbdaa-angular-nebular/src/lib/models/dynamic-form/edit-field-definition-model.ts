/* tslint:disable */
import {SelectFieldOptionModel} from './select-field-option-model';
import {FormValidatorModel} from './edit-object-model';
import {InputType} from './input-type-model';
import {FormLayout} from './dynamic-field-enums';
import {QueryOperatorEnum} from "../other/query-operator-enum";


export interface AddInlineConfig {
  referencedPropertyName?: string;
  referencedPropertyValue?: string;
}

export interface ComputedConfig {
  onFieldChanged?: string;
  setField?: string;
  fromProp?: string;
}

export interface FilteredOptionsConfig {
  // validatorType?: FormValidatorType;
  onFieldChanged?: string;
  operator?: QueryOperatorEnum;
  fieldToCompare?: string;
}

export interface EditFieldDefinitionModel {
  parameterName?: string;
  editable?: boolean;
  displayOnUpdateForm?: boolean;
  editableOnCreateForm?: boolean;
  editableOnEditForm?: boolean;
  validators?: Array<FormValidatorModel>;
  options?: Array<SelectFieldOptionModel>;
  placeholder?: string;
  tableTitle?: string;
  helpText?: string;
  modelType?: string;
  displayOnCreationForm?: boolean;
  display?: boolean;
  filterable?: boolean;
  order?: number;
  filterOrder?: number;
  exportOrder?: number;
  inputType?: InputType;
  queryType?: string;
  groupName?: string;
  visibilityExpressionParams?: Array<string>;
  visibilityExpression?: string;
  formLayout?: FormLayout;
  computedOn?: ComputedConfig;
  optionsFilteredOn?: FilteredOptionsConfig;
  addInlineConfig?: AddInlineConfig;
  extraClassList?: string[];
  max?: string;
  min?: string;
}
