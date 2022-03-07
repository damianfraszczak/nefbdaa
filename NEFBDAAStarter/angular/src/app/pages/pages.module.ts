import {NgModule} from '@angular/core';

import {PagesComponent} from './pages.component';
import {PagesRoutingModule} from './pages-routing.module';
import {SettingsModule} from './settings/settings.module';
import {ThemeModule} from '../../../projects/nefbdaa-angular-nebular/src/public-api';
import {NefbdaaAngularNebularModule} from '../../../projects/nefbdaa-angular-nebular/src/public-api';

const PAGES_COMPONENTS = [
  PagesComponent,
];

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    SettingsModule,
    NefbdaaAngularNebularModule,
  ],
  declarations: [
    ...PAGES_COMPONENTS,
  ],
})
export class PagesModule {
}
