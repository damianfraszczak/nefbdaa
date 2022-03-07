import {ApiCrudService} from './api-crud-service';
import {Observable} from 'rxjs';
import {DialogConfigModel} from '../../components/dialog/dialog-models';

export interface ModelMapperService {
  getCrudServiceForModel(modelName: string): ApiCrudService<any>;

  showEditDialog(modelName: string, additionalParams: { [key: string]: string },
                 dialogConfig: DialogConfigModel): Observable<any>;

  navigateToList(modelName: string);

  navigateToDetails(modelName: string, id: any, params: { [key: string]: string });

  getAllCrudServices(): { [key: string]: ApiCrudService<any> };
}
