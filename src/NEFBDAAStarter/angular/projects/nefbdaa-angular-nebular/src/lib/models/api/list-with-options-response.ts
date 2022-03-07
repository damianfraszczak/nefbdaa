import {OptionsFieldDefinitionModel} from '../dynamic-form/options-field-definition-model';

export interface ListWithOptionsResponse<T> {
  result?: Array<T>;
  options?: Array<OptionsFieldDefinitionModel>;
}
