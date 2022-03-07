/**
 * @license
 * Copyright Akveo. All Rights Reserved.
 * Licensed under the MIT License. See License.txt in the project root for license information.
 */
import {APP_BASE_HREF} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {APP_INITIALIZER, ErrorHandler, NgModule, Provider} from '@angular/core';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';
import {environment} from '../environments/environment';
import {ApiConfiguration} from './generated-api/api-configuration';
import {LogoutInterceptor} from './auth/logout/logout.interceptor';
import {AuthGuard} from './auth/auth-guard';
import {LoggerModule, NgxLoggerLevel} from 'ngx-logger';
import {AgmCoreModule} from '@agm/core';
import {HttpErrorHandler} from '../../projects/nefbdaa-angular-nebular/src/lib/services/http-error.handler';
import {ApiConfigService, ApiDocumentsService, ApiNotesService, ApiUsersService} from './generated-api/services';
import {
  CONFIG_CRUD_SERVICE,
  DOCUMENTS_CRUD_SERVICE,
  LOGGED_USER_SERVICE,
  MODEL_MAPPER_SERVICE,
  NOTES_CRUD_SERVICE,
} from '../../projects/nefbdaa-angular-nebular/src/lib/shared-constants';
import {PagesModule} from './pages/pages.module';
import {NefbdaaAngularNebularModule} from '../../projects/nefbdaa-angular-nebular/src/lib/nefbdaa-angular-nebular.module';
import {AuthInterceptor} from '../../projects/nefbdaa-angular-nebular/src/lib/nebular/config/auth/auth.interceptor';
import {AppModelMapperService} from './app-model-mapper.service';


export function initApiConfiguration(config: ApiConfiguration): Function {
  return () => {
    if (!environment.production) {
      config.rootUrl = 'http://localhost:4201';
    }
  };
}


export const INIT_API_CONFIGURATION: Provider = {
  provide: APP_INITIALIZER,
  useFactory: initApiConfiguration,
  deps: [ApiConfiguration],
  multi: true,
};

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    PagesModule,
    AgmCoreModule.forRoot({
      apiKey: '',
    }),
    LoggerModule.forRoot({
      // serverLoggingUrl: '/api/ApiAppLogs',
      level: NgxLoggerLevel.DEBUG,
      // serverLogLevel: NgxLoggerLevel.ERROR,
      disableConsoleLogging: environment.production,
    }),
    NefbdaaAngularNebularModule.forRoot({
      securityName: environment.securityStrategyName,
      nbSecurityOptions: {
        accessControl: {
          guest: {
            view: '*',
          },
          Admin: {
            parent: 'guest',
            create: '*',
            edit: '*',
            remove: '*',
          },

        },
      },
    }),
  ],
  bootstrap: [AppComponent],
  providers: [
    {provide: APP_BASE_HREF, useValue: '/'},
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: NOTES_CRUD_SERVICE,
      useClass: ApiNotesService,
      multi: false,
    },
    {
      provide: DOCUMENTS_CRUD_SERVICE,
      useClass: ApiDocumentsService,
      multi: false,
    },
    {
      provide: CONFIG_CRUD_SERVICE,
      useClass: ApiConfigService,
      multi: false,
    },
    {
      provide: LOGGED_USER_SERVICE,
      useClass: ApiUsersService,
      multi: false,
    },
    {
      provide: MODEL_MAPPER_SERVICE,
      useClass: AppModelMapperService,
      multi: false,
    },
    {
      provide: ErrorHandler,
      useClass: HttpErrorHandler,
    },

    AuthGuard,
    INIT_API_CONFIGURATION,
  ],


})
export class AppModule {
}
