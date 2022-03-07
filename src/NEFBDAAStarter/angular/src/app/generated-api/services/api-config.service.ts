/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { ConfigAppViewModelApiResponse } from '../models/config-app-view-model-api-response';
import { ConfigViewModelApiResponse } from '../models/config-view-model-api-response';
import { ObjectFormModelApiResponse } from '../models/object-form-model-api-response';
import { ConfigViewModel } from '../models/config-view-model';
import { ConfigViewModelIEnumerableApiResponse } from '../models/config-view-model-ienumerable-api-response';
import { Int64ConfigViewModelBulkUpdateParams } from '../models/int-64config-view-model-bulk-update-params';
import { ConfigViewModelFormModelApiResponse } from '../models/config-view-model-form-model-api-response';
import { Int64GetWithEditDataParams } from '../models/int-64get-with-edit-data-params';
import { ISelectFieldOptionViewModelIEnumerableApiResponse } from '../models/iselect-field-option-view-model-ienumerable-api-response';
import { FilterRule } from '../models/filter-rule';
import { ConfigViewModelPagedListApiResponse } from '../models/config-view-model-paged-list-api-response';
import { GetPagingWithFitlerParams } from '../models/get-paging-with-fitler-params';
import { ConfigViewModelListWithOptionsResponseApiResponse } from '../models/config-view-model-list-with-options-response-api-response';
import { OptionsForEdit } from '../models/options-for-edit';
import { ConfigViewModelPagedListWithOptionsResponseApiResponse } from '../models/config-view-model-paged-list-with-options-response-api-response';
import { DocumentViewModelApiResponse } from '../models/document-view-model-api-response';
import { ExportDataModel } from '../models/export-data-model';
@Injectable({
  providedIn: 'root',
})
class ApiConfigService extends __BaseService {
  static readonly ConfigPath = '/api/ApiConfig/Config';
  static readonly GetByAppKeyPath = '/api/ApiConfig/GetByAppKey';
  static readonly GetEditFormModelPath = '/api/ApiConfig/GetEditFormModel';
  static readonly PostPath = '/api/ApiConfig';
  static readonly PutPath = '/api/ApiConfig';
  static readonly DeletePath = '/api/ApiConfig';
  static readonly GetPath = '/api/ApiConfig';
  static readonly BulkCreatePath = '/api/ApiConfig/BulkCreate';
  static readonly AddOrUpdatePath = '/api/ApiConfig/AddOrUpdate';
  static readonly BulkUpdatePath = '/api/ApiConfig/BulkUpdate';
  static readonly BulkAddOrUpdatePath = '/api/ApiConfig/BulkAddOrUpdate';
  static readonly Delete_1Path = '/api/ApiConfig/{id}';
  static readonly DeleteByIdPath = '/api/ApiConfig/DeleteById';
  static readonly BulkDeletePath = '/api/ApiConfig/BulkDelete';
  static readonly GetWithEditDataPath = '/api/ApiConfig/GetWithEditData';
  static readonly SearchPath = '/api/ApiConfig/Search';
  static readonly GetWithFilterPath = '/api/ApiConfig/GetWithFilter';
  static readonly GetPagedPath = '/api/ApiConfig/GetPaged';
  static readonly GetPagedWithFilterPath = '/api/ApiConfig/GetPagedWithFilter';
  static readonly GetWithOptionsPath = '/api/ApiConfig/GetWithOptions';
  static readonly GetWithOptionsAndFilterPath = '/api/ApiConfig/GetWithOptionsAndFilter';
  static readonly GetOptionsForEditPath = '/api/ApiConfig/GetOptionsForEdit';
  static readonly GetPagedWithOptionsPath = '/api/ApiConfig/GetPagedWithOptions';
  static readonly GetPagedWithOptionsAndFilterPath = '/api/ApiConfig/GetPagedWithOptionsAndFilter';
  static readonly ExportPath = '/api/ApiConfig/Export';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @return Success
   */
  ConfigResponse(): __Observable<__StrictHttpResponse<ConfigAppViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiConfig/Config`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigAppViewModelApiResponse>;
      })
    );
  }
  /**
   * @return Success
   */
  Config(): __Observable<ConfigAppViewModelApiResponse> {
    return this.ConfigResponse().pipe(
      __map(_r => _r.body as ConfigAppViewModelApiResponse)
    );
  }

  /**
   * @param appKey undefined
   * @return Success
   */
  GetByAppKeyResponse(appKey?: string): __Observable<__StrictHttpResponse<ConfigViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (appKey != null) __params = __params.set('appKey', appKey.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiConfig/GetByAppKey`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelApiResponse>;
      })
    );
  }
  /**
   * @param appKey undefined
   * @return Success
   */
  GetByAppKey(appKey?: string): __Observable<ConfigViewModelApiResponse> {
    return this.GetByAppKeyResponse(appKey).pipe(
      __map(_r => _r.body as ConfigViewModelApiResponse)
    );
  }

  /**
   * @param params The `ApiConfigService.GetEditFormModelParams` containing the following parameters:
   *
   * - `modelType`:
   *
   * - `id`:
   *
   * @return Success
   */
  GetEditFormModelResponse(params: ApiConfigService.GetEditFormModelParams): __Observable<__StrictHttpResponse<ObjectFormModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.modelType != null) __params = __params.set('modelType', params.modelType.toString());
    if (params.id != null) __params = __params.set('id', params.id.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiConfig/GetEditFormModel`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ObjectFormModelApiResponse>;
      })
    );
  }
  /**
   * @param params The `ApiConfigService.GetEditFormModelParams` containing the following parameters:
   *
   * - `modelType`:
   *
   * - `id`:
   *
   * @return Success
   */
  GetEditFormModel(params: ApiConfigService.GetEditFormModelParams): __Observable<ObjectFormModelApiResponse> {
    return this.GetEditFormModelResponse(params).pipe(
      __map(_r => _r.body as ObjectFormModelApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  PostResponse(body?: ConfigViewModel): __Observable<__StrictHttpResponse<ConfigViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  Post(body?: ConfigViewModel): __Observable<ConfigViewModelApiResponse> {
    return this.PostResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  PutResponse(body?: ConfigViewModel): __Observable<__StrictHttpResponse<ConfigViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/ApiConfig`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  Put(body?: ConfigViewModel): __Observable<ConfigViewModelApiResponse> {
    return this.PutResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  DeleteResponse(body?: ConfigViewModel): __Observable<__StrictHttpResponse<ConfigViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/ApiConfig`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  Delete(body?: ConfigViewModel): __Observable<ConfigViewModelApiResponse> {
    return this.DeleteResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelApiResponse)
    );
  }

  /**
   * @return Success
   */
  GetResponse(): __Observable<__StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiConfig`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @return Success
   */
  Get(): __Observable<ConfigViewModelIEnumerableApiResponse> {
    return this.GetResponse().pipe(
      __map(_r => _r.body as ConfigViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  BulkCreateResponse(body?: Array<ConfigViewModel>): __Observable<__StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig/BulkCreate`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  BulkCreate(body?: Array<ConfigViewModel>): __Observable<ConfigViewModelIEnumerableApiResponse> {
    return this.BulkCreateResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  AddOrUpdateResponse(body?: ConfigViewModel): __Observable<__StrictHttpResponse<ConfigViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig/AddOrUpdate`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  AddOrUpdate(body?: ConfigViewModel): __Observable<ConfigViewModelApiResponse> {
    return this.AddOrUpdateResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  BulkUpdateResponse(body?: Int64ConfigViewModelBulkUpdateParams): __Observable<__StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/ApiConfig/BulkUpdate`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  BulkUpdate(body?: Int64ConfigViewModelBulkUpdateParams): __Observable<ConfigViewModelIEnumerableApiResponse> {
    return this.BulkUpdateResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  BulkAddOrUpdateResponse(body?: Array<ConfigViewModel>): __Observable<__StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/ApiConfig/BulkAddOrUpdate`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  BulkAddOrUpdate(body?: Array<ConfigViewModel>): __Observable<ConfigViewModelIEnumerableApiResponse> {
    return this.BulkAddOrUpdateResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  Delete_1Response(id: number): __Observable<__StrictHttpResponse<ConfigViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/ApiConfig/${id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelApiResponse>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  Delete_1(id: number): __Observable<ConfigViewModelApiResponse> {
    return this.Delete_1Response(id).pipe(
      __map(_r => _r.body as ConfigViewModelApiResponse)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  DeleteByIdResponse(id?: number): __Observable<__StrictHttpResponse<ConfigViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (id != null) __params = __params.set('id', id.toString());
    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/ApiConfig/DeleteById`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelApiResponse>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  DeleteById(id?: number): __Observable<ConfigViewModelApiResponse> {
    return this.DeleteByIdResponse(id).pipe(
      __map(_r => _r.body as ConfigViewModelApiResponse)
    );
  }

  /**
   * @param elementsToDeleteIds undefined
   * @return Success
   */
  BulkDeleteResponse(elementsToDeleteIds?: Array<number>): __Observable<__StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    (elementsToDeleteIds || []).forEach(val => {if (val != null) __params = __params.append('elementsToDeleteIds', val.toString())});
    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/ApiConfig/BulkDelete`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param elementsToDeleteIds undefined
   * @return Success
   */
  BulkDelete(elementsToDeleteIds?: Array<number>): __Observable<ConfigViewModelIEnumerableApiResponse> {
    return this.BulkDeleteResponse(elementsToDeleteIds).pipe(
      __map(_r => _r.body as ConfigViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetWithEditDataResponse(body?: Int64GetWithEditDataParams): __Observable<__StrictHttpResponse<ConfigViewModelFormModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig/GetWithEditData`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelFormModelApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetWithEditData(body?: Int64GetWithEditDataParams): __Observable<ConfigViewModelFormModelApiResponse> {
    return this.GetWithEditDataResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelFormModelApiResponse)
    );
  }

  /**
   * @param params The `ApiConfigService.SearchParams` containing the following parameters:
   *
   * - `term`:
   *
   * - `modelType`:
   *
   * - `fieldName`:
   *
   * @return Success
   */
  SearchResponse(params: ApiConfigService.SearchParams): __Observable<__StrictHttpResponse<ISelectFieldOptionViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.term != null) __params = __params.set('term', params.term.toString());
    if (params.modelType != null) __params = __params.set('modelType', params.modelType.toString());
    if (params.fieldName != null) __params = __params.set('fieldName', params.fieldName.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiConfig/Search`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ISelectFieldOptionViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param params The `ApiConfigService.SearchParams` containing the following parameters:
   *
   * - `term`:
   *
   * - `modelType`:
   *
   * - `fieldName`:
   *
   * @return Success
   */
  Search(params: ApiConfigService.SearchParams): __Observable<ISelectFieldOptionViewModelIEnumerableApiResponse> {
    return this.SearchResponse(params).pipe(
      __map(_r => _r.body as ISelectFieldOptionViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetWithFilterResponse(body?: FilterRule): __Observable<__StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig/GetWithFilter`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetWithFilter(body?: FilterRule): __Observable<ConfigViewModelIEnumerableApiResponse> {
    return this.GetWithFilterResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param params The `ApiConfigService.GetPagedParams` containing the following parameters:
   *
   * - `page.Size`:
   *
   * - `page.Index`:
   *
   * - `order.Property`:
   *
   * - `order.IsAscending`:
   *
   * @return Success
   */
  GetPagedResponse(params: ApiConfigService.GetPagedParams): __Observable<__StrictHttpResponse<ConfigViewModelPagedListApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.pageSize != null) __params = __params.set('page.Size', params.pageSize.toString());
    if (params.pageIndex != null) __params = __params.set('page.Index', params.pageIndex.toString());
    if (params.orderProperty != null) __params = __params.set('order.Property', params.orderProperty.toString());
    if (params.orderIsAscending != null) __params = __params.set('order.IsAscending', params.orderIsAscending.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiConfig/GetPaged`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelPagedListApiResponse>;
      })
    );
  }
  /**
   * @param params The `ApiConfigService.GetPagedParams` containing the following parameters:
   *
   * - `page.Size`:
   *
   * - `page.Index`:
   *
   * - `order.Property`:
   *
   * - `order.IsAscending`:
   *
   * @return Success
   */
  GetPaged(params: ApiConfigService.GetPagedParams): __Observable<ConfigViewModelPagedListApiResponse> {
    return this.GetPagedResponse(params).pipe(
      __map(_r => _r.body as ConfigViewModelPagedListApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetPagedWithFilterResponse(body?: GetPagingWithFitlerParams): __Observable<__StrictHttpResponse<ConfigViewModelPagedListApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig/GetPagedWithFilter`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelPagedListApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetPagedWithFilter(body?: GetPagingWithFitlerParams): __Observable<ConfigViewModelPagedListApiResponse> {
    return this.GetPagedWithFilterResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelPagedListApiResponse)
    );
  }

  /**
   * @return Success
   */
  GetWithOptionsResponse(): __Observable<__StrictHttpResponse<ConfigViewModelListWithOptionsResponseApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiConfig/GetWithOptions`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelListWithOptionsResponseApiResponse>;
      })
    );
  }
  /**
   * @return Success
   */
  GetWithOptions(): __Observable<ConfigViewModelListWithOptionsResponseApiResponse> {
    return this.GetWithOptionsResponse().pipe(
      __map(_r => _r.body as ConfigViewModelListWithOptionsResponseApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetWithOptionsAndFilterResponse(body?: FilterRule): __Observable<__StrictHttpResponse<ConfigViewModelListWithOptionsResponseApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig/GetWithOptionsAndFilter`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelListWithOptionsResponseApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetWithOptionsAndFilter(body?: FilterRule): __Observable<ConfigViewModelListWithOptionsResponseApiResponse> {
    return this.GetWithOptionsAndFilterResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelListWithOptionsResponseApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetOptionsForEditResponse(body?: OptionsForEdit): __Observable<__StrictHttpResponse<ISelectFieldOptionViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig/GetOptionsForEdit`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ISelectFieldOptionViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetOptionsForEdit(body?: OptionsForEdit): __Observable<ISelectFieldOptionViewModelIEnumerableApiResponse> {
    return this.GetOptionsForEditResponse(body).pipe(
      __map(_r => _r.body as ISelectFieldOptionViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param params The `ApiConfigService.GetPagedWithOptionsParams` containing the following parameters:
   *
   * - `page.Size`:
   *
   * - `page.Index`:
   *
   * - `order.Property`:
   *
   * - `order.IsAscending`:
   *
   * @return Success
   */
  GetPagedWithOptionsResponse(params: ApiConfigService.GetPagedWithOptionsParams): __Observable<__StrictHttpResponse<ConfigViewModelPagedListWithOptionsResponseApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.pageSize != null) __params = __params.set('page.Size', params.pageSize.toString());
    if (params.pageIndex != null) __params = __params.set('page.Index', params.pageIndex.toString());
    if (params.orderProperty != null) __params = __params.set('order.Property', params.orderProperty.toString());
    if (params.orderIsAscending != null) __params = __params.set('order.IsAscending', params.orderIsAscending.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiConfig/GetPagedWithOptions`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelPagedListWithOptionsResponseApiResponse>;
      })
    );
  }
  /**
   * @param params The `ApiConfigService.GetPagedWithOptionsParams` containing the following parameters:
   *
   * - `page.Size`:
   *
   * - `page.Index`:
   *
   * - `order.Property`:
   *
   * - `order.IsAscending`:
   *
   * @return Success
   */
  GetPagedWithOptions(params: ApiConfigService.GetPagedWithOptionsParams): __Observable<ConfigViewModelPagedListWithOptionsResponseApiResponse> {
    return this.GetPagedWithOptionsResponse(params).pipe(
      __map(_r => _r.body as ConfigViewModelPagedListWithOptionsResponseApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetPagedWithOptionsAndFilterResponse(body?: GetPagingWithFitlerParams): __Observable<__StrictHttpResponse<ConfigViewModelPagedListWithOptionsResponseApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig/GetPagedWithOptionsAndFilter`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ConfigViewModelPagedListWithOptionsResponseApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetPagedWithOptionsAndFilter(body?: GetPagingWithFitlerParams): __Observable<ConfigViewModelPagedListWithOptionsResponseApiResponse> {
    return this.GetPagedWithOptionsAndFilterResponse(body).pipe(
      __map(_r => _r.body as ConfigViewModelPagedListWithOptionsResponseApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  ExportResponse(body?: ExportDataModel): __Observable<__StrictHttpResponse<DocumentViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiConfig/Export`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  Export(body?: ExportDataModel): __Observable<DocumentViewModelApiResponse> {
    return this.ExportResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelApiResponse)
    );
  }
}

module ApiConfigService {

  /**
   * Parameters for GetEditFormModel
   */
  export interface GetEditFormModelParams {
    modelType?: string;
    id?: string;
  }

  /**
   * Parameters for Search
   */
  export interface SearchParams {
    term?: string;
    modelType?: string;
    fieldName?: string;
  }

  /**
   * Parameters for GetPaged
   */
  export interface GetPagedParams {
    pageSize?: number;
    pageIndex?: number;
    orderProperty?: string;
    orderIsAscending?: boolean;
  }

  /**
   * Parameters for GetPagedWithOptions
   */
  export interface GetPagedWithOptionsParams {
    pageSize?: number;
    pageIndex?: number;
    orderProperty?: string;
    orderIsAscending?: boolean;
  }
}

export { ApiConfigService }
