import {InjectionToken} from '@angular/core';
import {ApiCrudService} from './services/api/api-crud-service';
import {NoteModel} from './models/entities/note-model';
import {DocumentModel} from './models/entities/document-model';
import {ConfigModel} from './models/entities/config-model';
import {LoggedUserApiService} from './services/api/logged-user-api-service';
import {ModelMapperService} from './services/api/model-mapper-service';

export const NOTES_CRUD_SERVICE =
  new InjectionToken<ApiCrudService<NoteModel>>('notes_crud_service');
export const DOCUMENTS_CRUD_SERVICE =
  new InjectionToken<ApiCrudService<DocumentModel>>('documents_crud_service');
export const CONFIG_CRUD_SERVICE =
  new InjectionToken<ApiCrudService<ConfigModel>>('config_crud_service');
export const LOGGED_USER_SERVICE =
  new InjectionToken<LoggedUserApiService>('logged_user_api_service');

export const MODEL_MAPPER_SERVICE =
  new InjectionToken<ModelMapperService>('model_mapper_service');

export const constants = {
  cacheKey: 'app_cache',
  cacheMaxAge: 30000,
  defaultID: 0,
  securityStrategyName: 'app-security-strategy',
  dateFormat: 'dd/MM/yyyy',
  timeFormat: 'HH:mm',
  dateTimeFormat: 'dd/MM/yyyy HH:mm',
  apiDateTimeFormat: 'yyyy-MM-ddTHH:mm:ss',
  doubleFormat: '1.1-3',
  integerFormat: '1.0-0',
  defaultLocale: 'en-US',
};

export const PERMISSION_CREATE = 'create';
export const PERMISSION_EDIT = 'edit';
export const PERMISSION_REMOVE = 'remove';
export const PERMISSION_LIST = 'list';
