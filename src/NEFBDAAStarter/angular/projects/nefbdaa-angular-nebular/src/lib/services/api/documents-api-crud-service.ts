import {UploadFileModel} from '../../models/api/upload-file-model';
import {ApiCrudService} from './api-crud-service';
import {DocumentModel} from '../../models/entities/document-model';
import {BaseApiResponse} from '../../models/api/base-api-response';
import {Observable} from 'rxjs';

export interface DocumentsApiCrudService extends ApiCrudService<DocumentModel> {
  GeForOwner(guid?: string): Observable<BaseApiResponse<DocumentModel>>;

  Upload(params: UploadFileModel): Observable<BaseApiResponse<DocumentModel>>;
}
