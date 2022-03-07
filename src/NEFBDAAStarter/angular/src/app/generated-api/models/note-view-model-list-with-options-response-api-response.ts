/* tslint:disable */
import { ExceptionUiConfig } from './exception-ui-config';
import { ValidationError } from './validation-error';
import { NoteViewModelListWithOptionsResponse } from './note-view-model-list-with-options-response';
export interface NoteViewModelListWithOptionsResponseApiResponse {
  success?: boolean;
  message?: string;
  detail?: string;
  uiConfig?: ExceptionUiConfig;
  errors?: Array<ValidationError>;
  content?: NoteViewModelListWithOptionsResponse;
}
