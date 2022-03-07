import {FilterRuleModel} from '../dynamic-form/filter-rule-model';

export interface OptionsForEditModel {
  editedObject?: {};
  additionalRule?: FilterRuleModel;
  fieldName?: string;
  modelType?: string;
}
