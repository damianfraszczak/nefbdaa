/* tslint:disable */
import { LanguageViewModel } from './language-view-model';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface LanguageViewModelListWithOptionsResponse {
  result?: Array<LanguageViewModel>;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
