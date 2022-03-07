import {Component, OnInit} from '@angular/core';
import {ApiAuthenticationService, ApiConfigService, ApiUsersService} from '../../../generated-api/services';
import {NbAuthJWTToken, NbTokenService} from '@nebular/auth';
import {ModelType} from '../../../constants';
import {
  DialogService,
  extractContent,
  ITableConfig,
} from '../../../../../projects/nefbdaa-angular-nebular/src/public-api';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../environments/environment';

@Component({
  selector: 'ngx-users-settings',
  template: '<ngx-config-based-table-component [tableConfig]="tableConfig" ' +
    '(custom)="onCustomAction($event)"></ngx-config-based-table-component>',
})
export class UsersSettingsComponent implements OnInit {

  tableConfig: ITableConfig = {
    title: 'Users',
    filterType: "advanced",
    modelType: ModelType[ModelType.USER],
    editModes: ['INLINE', 'PAGE'],
    showBulkUpdate: false,
    queryParamsFunction: () => {
      return {'text': 1};
    },
    customTableActions: [
      {name: 'password', title: '<i class="fas fa-key fa-xs" title="Change password"></i>'},
      {name: 'login-as', title: '<i class="fas fa-sign-in-alt fa-xs" title="Login as"></i>'},
    ],
    extraToolbarActionsInOppositeDirection: [{name: 'test', title: 'Test', icon: 'clock-outline'}],
    extraToolbarActions: [],
    visibleColumns: ['roles'],
    dataChangedListener: (type, obj) => console.log(type + ' ' + obj),

    // defaultFiltersFunction: () => [{condition: "and", operator: '=', field: 'languageId', value: '1', type: 'integer'}],
    // filterableColumns: ['languageId', 'userStaticoptions', 'addressVisible', 'languageName', 'status', 'dateOfBirth',
    //   'companiesCompanyId'],
    // exportableColumns: ['firstName', 'languageId'],
  };


  constructor(
    readonly service: ApiUsersService,
    readonly authService: ApiAuthenticationService,
    readonly tokenService: NbTokenService,
    readonly dialogService: DialogService,
    readonly configService: ApiConfigService,
    readonly httpService: HttpClient) {
  }

  onCustomAction(event) {
    if (event.action === 'password') {
      this.dialogService.showPasswordDialog({}).onConfirm((dialogConfig) => {
        this.service.ChangePassword({password: dialogConfig.input, userId: event.data.id}).subscribe(() => {
        });
      });
    } else if (event.action === 'login-as') {
      this.authService.LoginAsOtherUser(event.data.id)
        .pipe(extractContent())
        .subscribe(newToken => {
          this.tokenService.set(new NbAuthJWTToken(newToken, environment.securityStrategyName));
          window.location.reload();
        });

    }

  }

  // , super admin, finaliser, planner, edit only
  ngOnInit(): void {

  }

}
