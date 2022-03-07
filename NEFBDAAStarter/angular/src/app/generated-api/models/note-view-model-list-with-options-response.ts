/* tslint:disable */
import { NoteViewModel } from './note-view-model';
import { OptionsFieldDefinitionViewModel } from './options-field-definition-view-model';
export interface NoteViewModelListWithOptionsResponse {
  result?: Array<NoteViewModel>;
  options?: Array<OptionsFieldDefinitionViewModel>;
}
