/* tslint:disable */
import { AppLogViewModel } from './app-log-view-model';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface AppLogViewModelListWithOptionsResponse {
  result?: Array<AppLogViewModel>;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
