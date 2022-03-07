/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { DocumentViewModelIEnumerableApiResponse } from '../models/document-view-model-ienumerable-api-response';
import { DocumentViewModelApiResponse } from '../models/document-view-model-api-response';
import { DocumentViewModel } from '../models/document-view-model';
import { Int64DocumentViewModelBulkUpdateParams } from '../models/int-64document-view-model-bulk-update-params';
import { DocumentViewModelFormModelApiResponse } from '../models/document-view-model-form-model-api-response';
import { Int64GetWithEditDataParams } from '../models/int-64get-with-edit-data-params';
import { ISelectFieldOptionViewModelIEnumerableApiResponse } from '../models/iselect-field-option-view-model-ienumerable-api-response';
import { FilterRule } from '../models/filter-rule';
import { DocumentViewModelPagedListApiResponse } from '../models/document-view-model-paged-list-api-response';
import { GetPagingWithFitlerParams } from '../models/get-paging-with-fitler-params';
import { DocumentViewModelListWithOptionsResponseApiResponse } from '../models/document-view-model-list-with-options-response-api-response';
import { OptionsForEdit } from '../models/options-for-edit';
import { DocumentViewModelPagedListWithOptionsResponseApiResponse } from '../models/document-view-model-paged-list-with-options-response-api-response';
import { ExportDataModel } from '../models/export-data-model';
@Injectable({
  providedIn: 'root',
})
class ApiDocumentsService extends __BaseService {
  static readonly GetForOwnerPath = '/api/ApiDocuments/GetForOwner';
  static readonly GetForOwnerAndTypePath = '/api/ApiDocuments/GetForOwnerAndType';
  static readonly UploadPath = '/api/ApiDocuments/Upload';
  static readonly DownloadPath = '/api/ApiDocuments/Download';
  static readonly PostPath = '/api/ApiDocuments';
  static readonly PutPath = '/api/ApiDocuments';
  static readonly DeletePath = '/api/ApiDocuments';
  static readonly GetPath = '/api/ApiDocuments';
  static readonly BulkCreatePath = '/api/ApiDocuments/BulkCreate';
  static readonly AddOrUpdatePath = '/api/ApiDocuments/AddOrUpdate';
  static readonly BulkUpdatePath = '/api/ApiDocuments/BulkUpdate';
  static readonly BulkAddOrUpdatePath = '/api/ApiDocuments/BulkAddOrUpdate';
  static readonly Delete_1Path = '/api/ApiDocuments/{id}';
  static readonly DeleteByIdPath = '/api/ApiDocuments/DeleteById';
  static readonly BulkDeletePath = '/api/ApiDocuments/BulkDelete';
  static readonly GetWithEditDataPath = '/api/ApiDocuments/GetWithEditData';
  static readonly SearchPath = '/api/ApiDocuments/Search';
  static readonly GetWithFilterPath = '/api/ApiDocuments/GetWithFilter';
  static readonly GetPagedPath = '/api/ApiDocuments/GetPaged';
  static readonly GetPagedWithFilterPath = '/api/ApiDocuments/GetPagedWithFilter';
  static readonly GetWithOptionsPath = '/api/ApiDocuments/GetWithOptions';
  static readonly GetWithOptionsAndFilterPath = '/api/ApiDocuments/GetWithOptionsAndFilter';
  static readonly GetOptionsForEditPath = '/api/ApiDocuments/GetOptionsForEdit';
  static readonly GetPagedWithOptionsPath = '/api/ApiDocuments/GetPagedWithOptions';
  static readonly GetPagedWithOptionsAndFilterPath = '/api/ApiDocuments/GetPagedWithOptionsAndFilter';
  static readonly ExportPath = '/api/ApiDocuments/Export';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param guid undefined
   * @return Success
   */
  GetForOwnerResponse(guid?: string): __Observable<__StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (guid != null) __params = __params.set('guid', guid.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiDocuments/GetForOwner`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param guid undefined
   * @return Success
   */
  GetForOwner(guid?: string): __Observable<DocumentViewModelIEnumerableApiResponse> {
    return this.GetForOwnerResponse(guid).pipe(
      __map(_r => _r.body as DocumentViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param params The `ApiDocumentsService.GetForOwnerAndTypeParams` containing the following parameters:
   *
   * - `type`:
   *
   * - `guid`:
   *
   * @return Success
   */
  GetForOwnerAndTypeResponse(params: ApiDocumentsService.GetForOwnerAndTypeParams): __Observable<__StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.type != null) __params = __params.set('type', params.type.toString());
    if (params.guid != null) __params = __params.set('guid', params.guid.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiDocuments/GetForOwnerAndType`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param params The `ApiDocumentsService.GetForOwnerAndTypeParams` containing the following parameters:
   *
   * - `type`:
   *
   * - `guid`:
   *
   * @return Success
   */
  GetForOwnerAndType(params: ApiDocumentsService.GetForOwnerAndTypeParams): __Observable<DocumentViewModelIEnumerableApiResponse> {
    return this.GetForOwnerAndTypeResponse(params).pipe(
      __map(_r => _r.body as DocumentViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param params The `ApiDocumentsService.UploadParams` containing the following parameters:
   *
   * - `ownerGuid`:
   *
   * - `ownerDocumentType`:
   *
   * - `file`:
   *
   * @return Success
   */
  UploadResponse(params: ApiDocumentsService.UploadParams): __Observable<__StrictHttpResponse<DocumentViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let __formData = new FormData();
    __body = __formData;
    if (params.ownerGuid != null) __params = __params.set('ownerGuid', params.ownerGuid.toString());
    if (params.ownerDocumentType != null) __params = __params.set('ownerDocumentType', params.ownerDocumentType.toString());
    if (params.file != null) { __formData.append('file', params.file as string | Blob);}
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiDocuments/Upload`,
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
   * @param params The `ApiDocumentsService.UploadParams` containing the following parameters:
   *
   * - `ownerGuid`:
   *
   * - `ownerDocumentType`:
   *
   * - `file`:
   *
   * @return Success
   */
  Upload(params: ApiDocumentsService.UploadParams): __Observable<DocumentViewModelApiResponse> {
    return this.UploadResponse(params).pipe(
      __map(_r => _r.body as DocumentViewModelApiResponse)
    );
  }

  /**
   * @param id undefined
   */
  DownloadResponse(id?: number): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (id != null) __params = __params.set('id', id.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiDocuments/Download`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param id undefined
   */
  Download(id?: number): __Observable<null> {
    return this.DownloadResponse(id).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  PostResponse(body?: DocumentViewModel): __Observable<__StrictHttpResponse<DocumentViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiDocuments`,
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
  Post(body?: DocumentViewModel): __Observable<DocumentViewModelApiResponse> {
    return this.PostResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  PutResponse(body?: DocumentViewModel): __Observable<__StrictHttpResponse<DocumentViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/ApiDocuments`,
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
  Put(body?: DocumentViewModel): __Observable<DocumentViewModelApiResponse> {
    return this.PutResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  DeleteResponse(body?: DocumentViewModel): __Observable<__StrictHttpResponse<DocumentViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/ApiDocuments`,
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
  Delete(body?: DocumentViewModel): __Observable<DocumentViewModelApiResponse> {
    return this.DeleteResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelApiResponse)
    );
  }

  /**
   * @return Success
   */
  GetResponse(): __Observable<__StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiDocuments`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @return Success
   */
  Get(): __Observable<DocumentViewModelIEnumerableApiResponse> {
    return this.GetResponse().pipe(
      __map(_r => _r.body as DocumentViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  BulkCreateResponse(body?: Array<DocumentViewModel>): __Observable<__StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiDocuments/BulkCreate`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  BulkCreate(body?: Array<DocumentViewModel>): __Observable<DocumentViewModelIEnumerableApiResponse> {
    return this.BulkCreateResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  AddOrUpdateResponse(body?: DocumentViewModel): __Observable<__StrictHttpResponse<DocumentViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiDocuments/AddOrUpdate`,
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
  AddOrUpdate(body?: DocumentViewModel): __Observable<DocumentViewModelApiResponse> {
    return this.AddOrUpdateResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  BulkUpdateResponse(body?: Int64DocumentViewModelBulkUpdateParams): __Observable<__StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/ApiDocuments/BulkUpdate`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  BulkUpdate(body?: Int64DocumentViewModelBulkUpdateParams): __Observable<DocumentViewModelIEnumerableApiResponse> {
    return this.BulkUpdateResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  BulkAddOrUpdateResponse(body?: Array<DocumentViewModel>): __Observable<__StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/ApiDocuments/BulkAddOrUpdate`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  BulkAddOrUpdate(body?: Array<DocumentViewModel>): __Observable<DocumentViewModelIEnumerableApiResponse> {
    return this.BulkAddOrUpdateResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  Delete_1Response(id: number): __Observable<__StrictHttpResponse<DocumentViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/ApiDocuments/${id}`,
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
   * @param id undefined
   * @return Success
   */
  Delete_1(id: number): __Observable<DocumentViewModelApiResponse> {
    return this.Delete_1Response(id).pipe(
      __map(_r => _r.body as DocumentViewModelApiResponse)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  DeleteByIdResponse(id?: number): __Observable<__StrictHttpResponse<DocumentViewModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (id != null) __params = __params.set('id', id.toString());
    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/ApiDocuments/DeleteById`,
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
   * @param id undefined
   * @return Success
   */
  DeleteById(id?: number): __Observable<DocumentViewModelApiResponse> {
    return this.DeleteByIdResponse(id).pipe(
      __map(_r => _r.body as DocumentViewModelApiResponse)
    );
  }

  /**
   * @param elementsToDeleteIds undefined
   * @return Success
   */
  BulkDeleteResponse(elementsToDeleteIds?: Array<number>): __Observable<__StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    (elementsToDeleteIds || []).forEach(val => {if (val != null) __params = __params.append('elementsToDeleteIds', val.toString())});
    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/ApiDocuments/BulkDelete`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param elementsToDeleteIds undefined
   * @return Success
   */
  BulkDelete(elementsToDeleteIds?: Array<number>): __Observable<DocumentViewModelIEnumerableApiResponse> {
    return this.BulkDeleteResponse(elementsToDeleteIds).pipe(
      __map(_r => _r.body as DocumentViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetWithEditDataResponse(body?: Int64GetWithEditDataParams): __Observable<__StrictHttpResponse<DocumentViewModelFormModelApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiDocuments/GetWithEditData`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelFormModelApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetWithEditData(body?: Int64GetWithEditDataParams): __Observable<DocumentViewModelFormModelApiResponse> {
    return this.GetWithEditDataResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelFormModelApiResponse)
    );
  }

  /**
   * @param params The `ApiDocumentsService.SearchParams` containing the following parameters:
   *
   * - `term`:
   *
   * - `modelType`:
   *
   * - `fieldName`:
   *
   * @return Success
   */
  SearchResponse(params: ApiDocumentsService.SearchParams): __Observable<__StrictHttpResponse<ISelectFieldOptionViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.term != null) __params = __params.set('term', params.term.toString());
    if (params.modelType != null) __params = __params.set('modelType', params.modelType.toString());
    if (params.fieldName != null) __params = __params.set('fieldName', params.fieldName.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiDocuments/Search`,
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
   * @param params The `ApiDocumentsService.SearchParams` containing the following parameters:
   *
   * - `term`:
   *
   * - `modelType`:
   *
   * - `fieldName`:
   *
   * @return Success
   */
  Search(params: ApiDocumentsService.SearchParams): __Observable<ISelectFieldOptionViewModelIEnumerableApiResponse> {
    return this.SearchResponse(params).pipe(
      __map(_r => _r.body as ISelectFieldOptionViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetWithFilterResponse(body?: FilterRule): __Observable<__StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiDocuments/GetWithFilter`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetWithFilter(body?: FilterRule): __Observable<DocumentViewModelIEnumerableApiResponse> {
    return this.GetWithFilterResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelIEnumerableApiResponse)
    );
  }

  /**
   * @param params The `ApiDocumentsService.GetPagedParams` containing the following parameters:
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
  GetPagedResponse(params: ApiDocumentsService.GetPagedParams): __Observable<__StrictHttpResponse<DocumentViewModelPagedListApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.pageSize != null) __params = __params.set('page.Size', params.pageSize.toString());
    if (params.pageIndex != null) __params = __params.set('page.Index', params.pageIndex.toString());
    if (params.orderProperty != null) __params = __params.set('order.Property', params.orderProperty.toString());
    if (params.orderIsAscending != null) __params = __params.set('order.IsAscending', params.orderIsAscending.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiDocuments/GetPaged`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelPagedListApiResponse>;
      })
    );
  }
  /**
   * @param params The `ApiDocumentsService.GetPagedParams` containing the following parameters:
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
  GetPaged(params: ApiDocumentsService.GetPagedParams): __Observable<DocumentViewModelPagedListApiResponse> {
    return this.GetPagedResponse(params).pipe(
      __map(_r => _r.body as DocumentViewModelPagedListApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetPagedWithFilterResponse(body?: GetPagingWithFitlerParams): __Observable<__StrictHttpResponse<DocumentViewModelPagedListApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiDocuments/GetPagedWithFilter`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelPagedListApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetPagedWithFilter(body?: GetPagingWithFitlerParams): __Observable<DocumentViewModelPagedListApiResponse> {
    return this.GetPagedWithFilterResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelPagedListApiResponse)
    );
  }

  /**
   * @return Success
   */
  GetWithOptionsResponse(): __Observable<__StrictHttpResponse<DocumentViewModelListWithOptionsResponseApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiDocuments/GetWithOptions`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelListWithOptionsResponseApiResponse>;
      })
    );
  }
  /**
   * @return Success
   */
  GetWithOptions(): __Observable<DocumentViewModelListWithOptionsResponseApiResponse> {
    return this.GetWithOptionsResponse().pipe(
      __map(_r => _r.body as DocumentViewModelListWithOptionsResponseApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetWithOptionsAndFilterResponse(body?: FilterRule): __Observable<__StrictHttpResponse<DocumentViewModelListWithOptionsResponseApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiDocuments/GetWithOptionsAndFilter`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelListWithOptionsResponseApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetWithOptionsAndFilter(body?: FilterRule): __Observable<DocumentViewModelListWithOptionsResponseApiResponse> {
    return this.GetWithOptionsAndFilterResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelListWithOptionsResponseApiResponse)
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
      this.rootUrl + `/api/ApiDocuments/GetOptionsForEdit`,
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
   * @param params The `ApiDocumentsService.GetPagedWithOptionsParams` containing the following parameters:
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
  GetPagedWithOptionsResponse(params: ApiDocumentsService.GetPagedWithOptionsParams): __Observable<__StrictHttpResponse<DocumentViewModelPagedListWithOptionsResponseApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.pageSize != null) __params = __params.set('page.Size', params.pageSize.toString());
    if (params.pageIndex != null) __params = __params.set('page.Index', params.pageIndex.toString());
    if (params.orderProperty != null) __params = __params.set('order.Property', params.orderProperty.toString());
    if (params.orderIsAscending != null) __params = __params.set('order.IsAscending', params.orderIsAscending.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiDocuments/GetPagedWithOptions`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelPagedListWithOptionsResponseApiResponse>;
      })
    );
  }
  /**
   * @param params The `ApiDocumentsService.GetPagedWithOptionsParams` containing the following parameters:
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
  GetPagedWithOptions(params: ApiDocumentsService.GetPagedWithOptionsParams): __Observable<DocumentViewModelPagedListWithOptionsResponseApiResponse> {
    return this.GetPagedWithOptionsResponse(params).pipe(
      __map(_r => _r.body as DocumentViewModelPagedListWithOptionsResponseApiResponse)
    );
  }

  /**
   * @param body undefined
   * @return Success
   */
  GetPagedWithOptionsAndFilterResponse(body?: GetPagingWithFitlerParams): __Observable<__StrictHttpResponse<DocumentViewModelPagedListWithOptionsResponseApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/ApiDocuments/GetPagedWithOptionsAndFilter`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<DocumentViewModelPagedListWithOptionsResponseApiResponse>;
      })
    );
  }
  /**
   * @param body undefined
   * @return Success
   */
  GetPagedWithOptionsAndFilter(body?: GetPagingWithFitlerParams): __Observable<DocumentViewModelPagedListWithOptionsResponseApiResponse> {
    return this.GetPagedWithOptionsAndFilterResponse(body).pipe(
      __map(_r => _r.body as DocumentViewModelPagedListWithOptionsResponseApiResponse)
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
      this.rootUrl + `/api/ApiDocuments/Export`,
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

module ApiDocumentsService {

  /**
   * Parameters for GetForOwnerAndType
   */
  export interface GetForOwnerAndTypeParams {
    type?: string;
    guid?: string;
  }

  /**
   * Parameters for Upload
   */
  export interface UploadParams {
    ownerGuid?: string;
    ownerDocumentType?: string;
    file?: string;
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

export { ApiDocumentsService }
