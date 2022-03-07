/* tslint:disable */
import { DocumentViewModel } from './document-view-model';
export interface DocumentViewModelPagedList {
  count?: number;
  list?: Array<DocumentViewModel>;
  index?: number;
  size?: number;
}
