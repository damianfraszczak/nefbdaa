/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { AppLogViewModelPagedListWithOptionsResponse } from './app-log-view-model-paged-list-with-options-response';
export interface AppLogViewModelPagedListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: AppLogViewModelPagedListWithOptionsResponse;
}
