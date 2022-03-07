/* tslint:disable */
import { AppLogViewModel } from './app-log-view-model';
export interface AppLogViewModelPagedList {
  count?: number;
  list?: Array<AppLogViewModel>;
  index?: number;
  size?: number;
}
