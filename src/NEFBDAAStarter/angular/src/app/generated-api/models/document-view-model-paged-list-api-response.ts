/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { DocumentViewModelPagedList } from './document-view-model-paged-list';
export interface DocumentViewModelPagedListApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: DocumentViewModelPagedList;
}
