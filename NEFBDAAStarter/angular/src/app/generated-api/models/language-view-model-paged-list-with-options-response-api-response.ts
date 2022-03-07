/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { LanguageViewModelPagedListWithOptionsResponse } from './language-view-model-paged-list-with-options-response';
export interface LanguageViewModelPagedListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: LanguageViewModelPagedListWithOptionsResponse;
}
