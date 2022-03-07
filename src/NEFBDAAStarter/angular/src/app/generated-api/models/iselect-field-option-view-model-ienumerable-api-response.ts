/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { ISelectFieldOptionViewModel } from './iselect-field-option-view-model';
export interface ISelectFieldOptionViewModelIEnumerableApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: Array<ISelectFieldOptionViewModel>;
}
