/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { CompanyViewModelListWithOptionsResponse } from './company-view-model-list-with-options-response';
export interface CompanyViewModelListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: CompanyViewModelListWithOptionsResponse;
}
