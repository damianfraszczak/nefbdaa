/* tslint:disable */
import { EditFieldDefinitionViewModel } from './edit-field-definition-view-model';
import { FormLayout } from './form-layout';
export interface FormGroupViewModel {
  formGroupField?: EditFieldDefinitionViewModel;
  editFields?: Array<EditFieldDefinitionViewModel>;
  formGroups?: Array<FormGroupViewModel>;
  name?: string;
  displayName?: boolean;
  formLayout?: FormLayout;
  defaultFieldLayout?: FormLayout;
  visibilityExpression?: string;
  visibilityExpressionParams?: Array<string>;
  order?: number;
  visibleOnCreateForm?: boolean;
  visibleOnUpdateForm?: boolean;
}
