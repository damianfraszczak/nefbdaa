/* tslint:disable */
import { FilterRule } from './filter-rule';
export interface ExportDataModel {
  type?: string;
  filename?: string;
  selectedElements?: Array<{}>;
  exportableFields?: Array<string>;
  rule?: FilterRule;
}
