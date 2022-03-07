/* tslint:disable */
export interface FilterRuleModel {
  condition?: string;
  field?: string;
  id?: string;
  input?: string;
  operator?: string;
  rules?: Array<FilterRuleModel>;
  type?: string;
  value?: {};
}
