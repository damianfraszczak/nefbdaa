import {ModelFieldDefinitionModel} from '../dynamic-form/model-field-definition-model';

export interface AppConfigModel {
  version?: string;
  allModelTypes?: Array<string>;
  models?: Array<ModelFieldDefinitionModel>;
}
