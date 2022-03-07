/* tslint:disable */
import { NoteViewModelPagedList } from './note-view-model-paged-list';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface NoteViewModelPagedListWithOptionsResponse {
  result?: NoteViewModelPagedList;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
