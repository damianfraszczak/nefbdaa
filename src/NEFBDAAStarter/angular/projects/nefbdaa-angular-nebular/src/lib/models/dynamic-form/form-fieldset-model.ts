import {EditFieldDefinitionModel} from './edit-field-definition-model';

export interface FormFieldsetModel {
  name?: string;
  editFields?: Array<EditFieldDefinitionModel>;
}
