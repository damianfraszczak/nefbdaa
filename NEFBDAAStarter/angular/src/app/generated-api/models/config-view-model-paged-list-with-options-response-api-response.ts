/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { ConfigViewModelPagedListWithOptionsResponse } from './config-view-model-paged-list-with-options-response';
export interface ConfigViewModelPagedListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: ConfigViewModelPagedListWithOptionsResponse;
}
