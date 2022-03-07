/* tslint:disable */
import {EditFieldDefinitionModel} from './edit-field-definition-model';
import {FormLayout, FormMode, FormValidatorType, FormGroupLayoutComponent} from "./dynamic-field-enums";


export interface FormGroupModel {
  name?: string;
  displayName?: boolean;
  editFields?: Array<EditFieldDefinitionModel>;
  fieldsetClass?: string;
  formGroupField?: EditFieldDefinitionModel;
  formLayout?: FormLayout;
  visibilityExpression?: string;
  visibilityExpressionParams?: Array<string>;
  order?: number;
  formGroups?: Array<FormGroupModel>;
  visibleOnCreateForm?: boolean;
  visibleOnUpdateForm?: boolean;
}

export interface FormValidatorModel {
  validatorType?: FormValidatorType;
  min?: number;
  max?: number;
  regex?: string;

}

export interface FormConfig {
  modelType?:string;
  title?: string;
  formLayout?: FormLayout;
  formMode?: FormMode;
  printLabels?: boolean;
  validators?: { [key: string]: Array<FormValidatorModel> };
  formGroups?: Array<FormGroupModel>;
  formGroupLayoutComponent?: FormGroupLayoutComponent;
}

export interface FormModel<T> {
  formConfig?: FormConfig;
  object?: T;
}


