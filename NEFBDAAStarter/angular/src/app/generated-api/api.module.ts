/* tslint:disable */
import { NgModule, ModuleWithProviders } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ApiConfiguration, ApiConfigurationInterface } from './api-configuration';

import { ApiAppLogsService } from './services/api-app-logs.service';
import { ApiAuthenticationService } from './services/api-authentication.service';
import { ApiCompaniesService } from './services/api-companies.service';
import { ApiConfigService } from './services/api-config.service';
import { ApiDocumentsService } from './services/api-documents.service';
import { ApiEntityFieldsDefinitionsService } from './services/api-entity-fields-definitions.service';
import { ApiLanguagesService } from './services/api-languages.service';
import { ApiNotesService } from './services/api-notes.service';
import { ApiUsersService } from './services/api-users.service';

/**
 * Provider for all Api services, plus ApiConfiguration
 */
@NgModule({
  imports: [
    HttpClientModule
  ],
  exports: [
    HttpClientModule
  ],
  declarations: [],
  providers: [
    ApiConfiguration,
    ApiAppLogsService,
    ApiAuthenticationService,
    ApiCompaniesService,
    ApiConfigService,
    ApiDocumentsService,
    ApiEntityFieldsDefinitionsService,
    ApiLanguagesService,
    ApiNotesService,
    ApiUsersService
  ],
})
export class ApiModule {
  static forRoot(customParams: ApiConfigurationInterface): ModuleWithProviders {
    return {
      ngModule: ApiModule,
      providers: [
        {
          provide: ApiConfiguration,
          useValue: {rootUrl: customParams.rootUrl}
        }
      ]
    }
  }
}
