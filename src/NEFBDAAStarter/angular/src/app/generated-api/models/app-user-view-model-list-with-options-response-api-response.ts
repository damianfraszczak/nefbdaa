/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { AppUserViewModelListWithOptionsResponse } from './app-user-view-model-list-with-options-response';
export interface AppUserViewModelListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: AppUserViewModelListWithOptionsResponse;
}
