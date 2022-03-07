/* tslint:disable */
import { CompanyViewModel } from './company-view-model';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface CompanyViewModelListWithOptionsResponse {
  result?: Array<CompanyViewModel>;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
