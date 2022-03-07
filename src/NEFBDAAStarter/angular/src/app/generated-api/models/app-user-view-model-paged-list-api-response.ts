/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { AppUserViewModelPagedList } from './app-user-view-model-paged-list';
export interface AppUserViewModelPagedListApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: AppUserViewModelPagedList;
}
