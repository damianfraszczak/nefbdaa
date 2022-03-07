/* tslint:disable */
import { CompanyViewModelPagedList } from './company-view-model-paged-list';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface CompanyViewModelPagedListWithOptionsResponse {
  result?: CompanyViewModelPagedList;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
