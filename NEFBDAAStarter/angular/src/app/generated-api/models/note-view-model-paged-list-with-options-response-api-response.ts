/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { NoteViewModelPagedListWithOptionsResponse } from './note-view-model-paged-list-with-options-response';
export interface NoteViewModelPagedListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: NoteViewModelPagedListWithOptionsResponse;
}
