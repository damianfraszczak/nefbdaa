/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { AppUserViewModelPagedListWithOptionsResponse } from './app-user-view-model-paged-list-with-options-response';
export interface AppUserViewModelPagedListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: AppUserViewModelPagedListWithOptionsResponse;
}
