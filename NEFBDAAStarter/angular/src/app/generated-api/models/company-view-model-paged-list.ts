/* tslint:disable */
import { CompanyViewModel } from './company-view-model';
export interface CompanyViewModelPagedList {
  count?: number;
  list?: Array<CompanyViewModel>;
  index?: number;
  size?: number;
}
