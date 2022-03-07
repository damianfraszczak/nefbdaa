/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { FieldDefinitionViewModelIEnumerableApiResponse } from '../models/field-definition-view-model-ienumerable-api-response';
@Injectable({
  providedIn: 'root',
})
class ApiEntityFieldsDefinitionsService extends __BaseService {
  static readonly GetModelFieldsDefinitionsPath = '/api/ApiEntityFieldsDefinitions/GetModelFieldsDefinitions';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param modelType undefined
   * @return Success
   */
  GetModelFieldsDefinitionsResponse(modelType?: string): __Observable<__StrictHttpResponse<FieldDefinitionViewModelIEnumerableApiResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (modelType != null) __params = __params.set('modelType', modelType.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/ApiEntityFieldsDefinitions/GetModelFieldsDefinitions`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<FieldDefinitionViewModelIEnumerableApiResponse>;
      })
    );
  }
  /**
   * @param modelType undefined
   * @return Success
   */
  GetModelFieldsDefinitions(modelType?: string): __Observable<FieldDefinitionViewModelIEnumerableApiResponse> {
    return this.GetModelFieldsDefinitionsResponse(modelType).pipe(
      __map(_r => _r.body as FieldDefinitionViewModelIEnumerableApiResponse)
    );
  }
}

module ApiEntityFieldsDefinitionsService {
}

export { ApiEntityFieldsDefinitionsService }
