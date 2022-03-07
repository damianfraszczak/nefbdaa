import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {PagesComponent} from '../pages.component';
import {AccountSettingsComponent} from './account-settings/account-settings.component';
import {UsersSettingsComponent} from './users-settings/users-settings.component';
import {DefaultTableComponentComponent} from '../../../../projects/nefbdaa-angular-nebular/src/lib/components/table-components/default-table-component/default-table-component.component';
import {ModelBasedFormComponent} from '../../../../projects/nefbdaa-angular-nebular/src/lib/components/dynamic-form/default-forms/model-based-form-component/model-based-form.component';

const routes: Routes = [{
  path: 'settings',
  component: PagesComponent,
  children: [
    {
      path: 'account',
      component: AccountSettingsComponent,
    },
    {
      path: 'user/list',
      component: UsersSettingsComponent,
    },
    {
      path: 'user/details/:id',
      component: UsersSettingsComponent,
    },
    {
      path: 'list/:model_type',
      component: DefaultTableComponentComponent,
    },
    {
      path: 'detail/:model_type/:id',
      component: ModelBasedFormComponent,
    },
  ],
}];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NgxSettingsRoutingModule {
}
