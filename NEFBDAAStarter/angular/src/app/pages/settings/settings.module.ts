import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AccountSettingsComponent} from './account-settings/account-settings.component';
import {UsersSettingsComponent} from './users-settings/users-settings.component';
import {NgxSettingsRoutingModule} from './settings-routing.module';
import {FormsModule} from '@angular/forms';
import {NebularModule} from '../../../../projects/nefbdaa-angular-nebular/src/lib/nebular/nebular.module';
import {NefbdaaAngularNebularModule} from '../../../../projects/nefbdaa-angular-nebular/src/lib/nefbdaa-angular-nebular.module';

@NgModule({
  declarations: [AccountSettingsComponent, UsersSettingsComponent],
  imports: [
    NgxSettingsRoutingModule,
    CommonModule,
    FormsModule,
    NebularModule,
    NefbdaaAngularNebularModule,
  ],
})
export class SettingsModule {
}
