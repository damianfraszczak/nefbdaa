import {FieldDefinitionModel} from './field-definition-model';

export interface ModelFieldDefinitionModel {
  modelType?: string;
  fieldDefinitions?: Array<FieldDefinitionModel>;
}
