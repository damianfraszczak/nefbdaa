/* tslint:disable */
import { DocumentViewModel } from './document-view-model';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface DocumentViewModelListWithOptionsResponse {
  result?: Array<DocumentViewModel>;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
