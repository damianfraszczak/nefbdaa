/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { CompanyViewModelPagedListWithOptionsResponse } from './company-view-model-paged-list-with-options-response';
export interface CompanyViewModelPagedListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: CompanyViewModelPagedListWithOptionsResponse;
}
