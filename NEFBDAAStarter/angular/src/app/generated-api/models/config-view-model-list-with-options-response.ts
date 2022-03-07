/* tslint:disable */
import { ConfigViewModel } from './config-view-model';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface ConfigViewModelListWithOptionsResponse {
  result?: Array<ConfigViewModel>;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
