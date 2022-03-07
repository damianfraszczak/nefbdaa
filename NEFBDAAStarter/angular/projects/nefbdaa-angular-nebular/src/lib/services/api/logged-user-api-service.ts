import {BaseApiResponse} from '../../models/api/base-api-response';
import {Observable} from 'rxjs';
import {LoggedUserModel} from '../../models/entities/logged-user-model';

export interface LoggedUserApiService {
  GetMyAccount(): Observable<BaseApiResponse<LoggedUserModel>>;
}
