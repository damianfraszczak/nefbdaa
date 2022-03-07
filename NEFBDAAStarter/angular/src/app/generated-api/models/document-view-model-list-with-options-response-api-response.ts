/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { DocumentViewModelListWithOptionsResponse } from './document-view-model-list-with-options-response';
export interface DocumentViewModelListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: DocumentViewModelListWithOptionsResponse;
}
