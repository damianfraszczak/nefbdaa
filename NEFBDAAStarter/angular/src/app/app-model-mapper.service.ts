import {Injectable} from '@angular/core';
import {ModelMapperService} from '../../projects/nefbdaa-angular-nebular/src/lib/services/api/model-mapper-service';
import {ApiCrudService} from '../../projects/nefbdaa-angular-nebular/src/lib/services/api/api-crud-service';
import {
  ApiCompaniesService,
  ApiDocumentsService,
  ApiLanguagesService,
  ApiNotesService,
  ApiUsersService,
} from './generated-api/services';
import {Observable} from 'rxjs';
import {flatMap} from 'rxjs/operators';
import {
  DialogService,
  extractContent,
  getDefaultId,
  getFromDictIgnoreCase,
  PERMISSION_CREATE,
  PERMISSION_EDIT,
  PERMISSION_LIST,
} from '../../projects/nefbdaa-angular-nebular/src/public-api';
import {Router} from '@angular/router';
import {DialogConfigModel} from '../../projects/nefbdaa-angular-nebular/src/lib/components/dialog/dialog-models';
import {EditDialogData} from '../../projects/nefbdaa-angular-nebular/src/lib/components/dialog/dynamic-edit-dialog/dynamic-edit-dialog.component';
import {NbAccessChecker} from '@nebular/security';


const DETAILS_COMPONENT_ROUTE = 'detail/';
const LIST_COMPONENT_ROUTE = 'list/';
const DEFAULT_LIST_COMPONENT_ROUTE = 'pages/settings/' + LIST_COMPONENT_ROUTE;
const DEFAULT_DETAILS_COMPONENT_ROUTE = 'pages/settings/' + DETAILS_COMPONENT_ROUTE;

@Injectable({
  providedIn: 'root',
})
export class AppModelMapperService implements ModelMapperService {

  servicesMapping: { [id: string]: ApiCrudService<any>; };
  modelPrefixes: { [id: string]: string } = {
    'LANGUAGE': 'page/settings/',
  };


  constructor(
    private usersService: ApiUsersService,
    private companyService: ApiCompaniesService,
    private languageService: ApiLanguagesService,
    private dialogService: DialogService,
    private documentsService: ApiDocumentsService,
    private notesService: ApiNotesService,
    private router: Router,
    private accessChecker: NbAccessChecker,
  ) {

    this.servicesMapping = {
      'LANGUAGE': this.languageService as ApiCrudService<any>,
      'USER': this.usersService as ApiCrudService<any>,
      'COMPANY': this.companyService as ApiCrudService<any>,
      'DOCUMENT': this.documentsService as ApiCrudService<any>,
      'NOTE': this.notesService as ApiCrudService<any>,
    };
  }


  getCrudServiceForModel(modelType: string): ApiCrudService<any> {
    return getFromDictIgnoreCase(modelType, this.servicesMapping);

  }

  getAllCrudServices(): { [key: string]: ApiCrudService<any> } {
    return this.servicesMapping;
  }

  async navigateToList(modelType: string) {
    const prefixForModel = getFromDictIgnoreCase(modelType, this.modelPrefixes);
    const isAllowed = await this.accessChecker.isGranted(PERMISSION_LIST, modelType);
    if (!isAllowed) {
      await this.router.navigate(['/']);
    }
    if (!prefixForModel) {
      await this.router.navigate([DEFAULT_LIST_COMPONENT_ROUTE], {queryParams: {'model_type': modelType}});
    } else {
      await this.router.navigate([prefixForModel + LIST_COMPONENT_ROUTE]);
    }
  }

  async navigateToDetails(modelType: string, id: any, queryParams: { [key: string]: string }) {
    const prefixForModel = getFromDictIgnoreCase(modelType, this.modelPrefixes);
    const objectId = getDefaultId(id);
    let isAllowed;
    if (objectId > 0) {
      isAllowed = await this.accessChecker.isGranted(PERMISSION_EDIT, modelType);
    } else {
      isAllowed = await this.accessChecker.isGranted(PERMISSION_CREATE, modelType);
    }
    if (!isAllowed) {
      await this.router.navigate(['/']);
    }
    if (!prefixForModel) {
      const modelTypeForUrl = modelType.toLowerCase().replace(' ', '-');
      await this.router.navigate([DEFAULT_DETAILS_COMPONENT_ROUTE + modelTypeForUrl + '/' + objectId],
        {queryParams: Object.assign(queryParams, {'model_type': modelType, 'id': objectId})});
    } else {
      await this.router.navigate([prefixForModel + DETAILS_COMPONENT_ROUTE + objectId],
        {queryParams: Object.assign(queryParams, {'id': objectId})});
    }
  }

  showEditDialog(modelName: string, additionalParams: { [key: string]: string } = {},
                 dialogConfig: DialogConfigModel = null): Observable<any> {
    const crudService = this.getCrudServiceForModel(modelName);
    if (crudService != null) {
      const editData = crudService.GetWithEditData(additionalParams).pipe(
        extractContent(),
        flatMap(form => {
          const config: EditDialogData<any> = {formModel: form};
          if (dialogConfig) {
            Object.assign(config, dialogConfig);
          }
          return this.dialogService.showDynamicEditorDialog(config).confirm;
        }),
        flatMap(result => crudService.AddOrUpdate(result.formModel.object)),
        extractContent(),
      );
      return editData;
    }
    return null;
  }
}
