/* tslint:disable */
export interface FilterRule {
  condition?: string;
  field?: string;
  id?: string;
  input?: string;
  operator?: string;
  rules?: Array<FilterRule>;
  type?: string;
  value?: {};
}
