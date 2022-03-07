/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
export interface ErrorApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: {};
}
