/* tslint:disable */
import { AppUserViewModel } from './app-user-view-model';
export interface AppUserViewModelPagedList {
  count?: number;
  list?: Array<AppUserViewModel>;
  index?: number;
  size?: number;
}
