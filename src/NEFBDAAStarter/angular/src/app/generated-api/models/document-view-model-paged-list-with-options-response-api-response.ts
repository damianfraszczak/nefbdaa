/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { DocumentViewModelPagedListWithOptionsResponse } from './document-view-model-paged-list-with-options-response';
export interface DocumentViewModelPagedListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: DocumentViewModelPagedListWithOptionsResponse;
}
