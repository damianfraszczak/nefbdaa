/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { AppLogViewModelListWithOptionsResponse } from './app-log-view-model-list-with-options-response';
export interface AppLogViewModelListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: AppLogViewModelListWithOptionsResponse;
}
