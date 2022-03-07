import {ErrorHandler, ModuleWithProviders, NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ConfigService} from './config.service';
import {HttpLoaderService} from './http-loader.service';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {ServiceSuccessInterceptor} from './interceptors/service-success.interceptor';
import {HttpErrorHandler} from './http-error.handler';
import {HttpLoaderInterceptor} from './interceptors/http-loader.interceptor';
import {CachingInterceptor} from './interceptors/caching.interceptor';
import {OnlineOfflineService} from './online-offline.service';
import {RequestCacheService} from './request-cache.service';
import {LoggedUserService} from './logged-user.service';
import {RouterHelperService} from './router-helper.service';


const SERVICES = [
  ConfigService,
  HttpLoaderService,
  OnlineOfflineService,
  RequestCacheService,
  LoggedUserService,
  RouterHelperService,
  {
    provide: HTTP_INTERCEPTORS,
    useClass: ServiceSuccessInterceptor,
    multi: true,
  },
  {
    provide: ErrorHandler,
    useClass: HttpErrorHandler,
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpLoaderInterceptor,
    multi: true,
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: CachingInterceptor,
    multi: true,
  },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
  ],
  exports: [],
  providers: [],
})
export class ServicesModule {
  static forRoot(): ModuleWithProviders {
    return <ModuleWithProviders>{
      ngModule: ServicesModule,
      providers: [SERVICES],
    };
  }
}
