import {PagedList} from './paged-list';
import {OptionsFieldDefinitionModel} from '../dynamic-form/options-field-definition-model';

export interface PagedListWithOptions<T> {
  result?: PagedList<T>;
  options?: Array<OptionsFieldDefinitionModel>;
}
