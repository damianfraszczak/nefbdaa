/* tslint:disable */
import { DocumentViewModelPagedList } from './document-view-model-paged-list';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface DocumentViewModelPagedListWithOptionsResponse {
  result?: DocumentViewModelPagedList;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
