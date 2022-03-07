import {Observable} from 'rxjs';
import {BaseApiResponse} from '../../models/api/base-api-response';
import {EditDataParams} from '../../models/api/edit-data-params';
import {FormModel} from '../../models/dynamic-form/edit-object-model';
import {FilterRuleModel} from '../../models/dynamic-form/filter-rule-model';
import {PagedList} from '../../models/api/paged-list';
import {PagedDataWithFilterParams} from '../../models/api/paged-data-with-filter-params';
import {ListWithOptionsResponse} from '../../models/api/list-with-options-response';
import {PagedListWithOptions} from '../../models/api/paged-list-with-options';
import {PagedDataParams} from '../../models/api/paged-data-params';
import {DocumentModel} from '../../models/entities/document-model';
import {ExportDataParams} from '../../models/api/export-data-params';
import {BulkDataUpdateParams} from '../../models/api/bulk-data-update-params';
import {SelectFieldOptionModel} from '../../models/dynamic-form/select-field-option-model';
import {OptionsForEditModel} from '../../models/api/options-for-edit-model';

export interface ApiCrudService<T> {
  Get(): Observable<BaseApiResponse<T[]>>;

  Search(
    params: {
      term: string,
      modelType?: string,
      fieldName?: string,
    }): Observable<BaseApiResponse<SelectFieldOptionModel[]>>;


  Put(value?: T): Observable<BaseApiResponse<T>>;

  Post(value?: T): Observable<BaseApiResponse<T>>;

  AddOrUpdate(value?: T): Observable<BaseApiResponse<T>>;

  Delete(value?: T): Observable<BaseApiResponse<T>>;

  DeleteById(id: number): Observable<BaseApiResponse<T>>;

  GetWithEditData(params: EditDataParams): Observable<BaseApiResponse<FormModel<T>>>;

  GetWithFilter(rule?: FilterRuleModel): Observable<BaseApiResponse<Array<T>>>;

  GetPaged(params: PagedDataParams): Observable<BaseApiResponse<PagedList<T>>>;

  GetPagedWithFilter(params: PagedDataWithFilterParams): Observable<BaseApiResponse<PagedList<T>>>;

  GetWithOptions(): Observable<BaseApiResponse<ListWithOptionsResponse<T>>>;

  GetWithOptionsAndFilter(rule?: FilterRuleModel): Observable<BaseApiResponse<ListWithOptionsResponse<T>>>;

  GetPagedWithOptions(params: PagedDataParams): Observable<BaseApiResponse<PagedListWithOptions<T>>>;

  GetPagedWithOptionsAndFilter(params: PagedDataWithFilterParams): Observable<BaseApiResponse<PagedListWithOptions<T>>>;

  Export(params: ExportDataParams): Observable<BaseApiResponse<DocumentModel>>;

  BulkUpdate(params: BulkDataUpdateParams<T>): Observable<BaseApiResponse<Array<T>>>;

  BulkDelete(elementsToDelete?: Array<number>): Observable<BaseApiResponse<Array<T>>>;

  GetOptionsForEdit(body?: OptionsForEditModel): Observable<BaseApiResponse<Array<SelectFieldOptionModel>>>;
}
