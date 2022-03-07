/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { DocumentViewModelFormModel } from './document-view-model-form-model';
export interface DocumentViewModelFormModelApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: DocumentViewModelFormModel;
}
