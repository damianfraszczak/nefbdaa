/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { AppLogViewModelFormModel } from './app-log-view-model-form-model';
export interface AppLogViewModelFormModelApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: AppLogViewModelFormModel;
}
