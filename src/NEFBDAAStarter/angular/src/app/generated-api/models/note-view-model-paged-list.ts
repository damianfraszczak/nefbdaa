/* tslint:disable */
import { NoteViewModel } from './note-view-model';
export interface NoteViewModelPagedList {
  count?: number;
  list?: Array<NoteViewModel>;
  index?: number;
  size?: number;
}
