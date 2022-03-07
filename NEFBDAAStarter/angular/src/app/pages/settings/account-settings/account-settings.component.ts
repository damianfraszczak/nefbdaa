import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {map} from 'rxjs/operators';
import {AppUserViewModel, ModelTypes} from '../../../generated-api/models';
import {ApiUsersService} from '../../../generated-api/services';
import {NbTokenService} from '@nebular/auth';
import {
  ApiCrudService,
  BaseDynamicFormComponent,
  DialogService, DynamicFormComponent, MODEL_MAPPER_SERVICE, ModelMapperService,
  RouterHelperService,
} from '../../../../../projects/nefbdaa-angular-nebular/src/public-api';
import {Observable} from 'rxjs';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'ngx-account-settings',
  templateUrl: './account-settings.component.html',
  styleUrls: ['./account-settings.component.scss'],
})
export class AccountSettingsComponent extends BaseDynamicFormComponent<AppUserViewModel>
  implements OnInit {

  modelType = ModelTypes.USER;
  @ViewChild(DynamicFormComponent, {static: false})
  ngxForm: DynamicFormComponent<AppUserViewModel>;
  formModel;
  constructor(@Inject(MODEL_MAPPER_SERVICE)
              protected readonly crudMapperApiService: ModelMapperService,
              protected readonly routeHelper: RouterHelperService,
              protected readonly tokenService: NbTokenService,
              protected readonly usersService: ApiUsersService,
              protected readonly dialogService: DialogService) {
    super(routeHelper, crudMapperApiService);
  }


  onChangePassword() {

    let id = 10;

    this.usersService.GetWithEditData({id: id}).subscribe((formModel) => {
      this.formModel = formModel;
    });
    this.dialogService.showInputEditorDialog({
      header: 'Change password',
      label: 'Password',
      type: 'input',
      input: '',
    }).onConfirm((model) => {
      this.usersService.ChangePassword({password: model.input}).subscribe(() => {
      });
    });
  }


  // overridneg form base component
  protected getIdToLoad(): Observable<any> {
    return this.tokenService.get().pipe(
      map(token => Number(token.getPayload()['sub'])),
    );
  }

  protected getService(): ApiCrudService<AppUserViewModel> {
    return this.usersService;
  }


  ngOnInit() {
    super.ngOnInit();

  }

  tabChanged() {
    const elem = this.ngxForm.form.get('dateOfBirth');
  }
}
