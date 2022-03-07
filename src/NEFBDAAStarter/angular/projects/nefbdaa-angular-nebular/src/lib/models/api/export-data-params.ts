import {FilterRuleModel} from '../dynamic-form/filter-rule-model';

export interface ExportDataParams {
  type?: string;
  selectedElements?: Array<{}>;
  exportableFields?: Array<string>;
  rule?: FilterRuleModel;
}
