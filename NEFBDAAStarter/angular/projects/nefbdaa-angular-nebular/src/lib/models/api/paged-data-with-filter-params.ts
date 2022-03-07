import {FilterRuleModel} from '../dynamic-form/filter-rule-model';
import {PagedDataParams} from './paged-data-params';

export interface PagedDataWithFilterParams {
  rule?: FilterRuleModel;
  pagingParams?: PagedDataParams;
}
