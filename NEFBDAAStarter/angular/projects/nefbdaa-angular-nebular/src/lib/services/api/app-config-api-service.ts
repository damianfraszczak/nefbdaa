import {ApiCrudService} from './api-crud-service';
import {Observable} from 'rxjs';
import {AppConfigModel} from '../../models/entities/app-config-model';
import {ConfigModel} from '../../models/entities/config-model';
import {BaseApiResponse} from '../../models/api/base-api-response';

export interface AppConfigApiService extends ApiCrudService<ConfigModel> {
  GetByAppKey(appKey?: string): Observable<ConfigModel>;

  Config(): Observable<BaseApiResponse<AppConfigModel>>;
}
