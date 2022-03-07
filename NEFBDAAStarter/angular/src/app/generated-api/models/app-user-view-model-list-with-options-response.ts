/* tslint:disable */
import { AppUserViewModel } from './app-user-view-model';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface AppUserViewModelListWithOptionsResponse {
  result?: Array<AppUserViewModel>;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
