/* tslint:disable */
import { ModelTypes } from './model-types';
import { ModelFieldDefinitionViewModel } from './model-field-definition-view-model';
export interface ConfigAppViewModel {
  version?: string;
  allModelTypes?: Array<ModelTypes>;
  models?: Array<ModelFieldDefinitionViewModel>;
}
