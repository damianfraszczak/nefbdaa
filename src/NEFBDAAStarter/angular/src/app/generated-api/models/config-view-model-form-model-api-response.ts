/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { ConfigViewModelFormModel } from './config-view-model-form-model';
export interface ConfigViewModelFormModelApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: ConfigViewModelFormModel;
}
