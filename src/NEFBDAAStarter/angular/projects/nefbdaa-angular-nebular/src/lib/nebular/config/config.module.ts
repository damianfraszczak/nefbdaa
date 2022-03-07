import {ModuleWithProviders, NgModule, Optional, SkipSelf} from '@angular/core';
import {CommonModule} from '@angular/common';
import {NbAuthJWTToken, NbAuthModule, NbPasswordAuthStrategy, NbPasswordAuthStrategyOptions} from '@nebular/auth';
import {NbRoleProvider, NbSecurityModule} from '@nebular/security';

import {HttpErrorResponse} from '@angular/common/http';
import {NebularConfigOptions} from '../nebular.module';
import {RoleProvider} from './auth/role-provider.service';

export function errorGetter(module: string, res: HttpErrorResponse, options: NbPasswordAuthStrategyOptions) {
  return [res.error.message];
}

export function throwIfAlreadyLoaded(parentModule: any, moduleName: string) {
  if (parentModule) {
    throw new Error(`${moduleName} has already been loaded. Import Core modules in the AppModule only.`);
  }
}


@NgModule({
  imports: [
    CommonModule,
  ],
  exports: [
    NbAuthModule,
  ],
  declarations: [],
})
export class ConfigModule {

  constructor(@Optional() @SkipSelf() parentModule: ConfigModule) {
    // throwIfAlreadyLoaded(parentModule, 'ConfigModule');
  }

  static forRoot(options: NebularConfigOptions): ModuleWithProviders {
    return <ModuleWithProviders>{
      ngModule: ConfigModule,
      providers: [
        ...NbAuthModule.forRoot({

          strategies: [
            NbPasswordAuthStrategy.setup({
              name: options.securityName,
              baseEndpoint: '/api',
              login: {
                endpoint: '/ApiAuthentication/Authenticate',
                method: 'post',
              },
              logout: {
                endpoint: '/ApiAuthentication/Logout',
                redirect: {
                  success: '/',
                  failure: '/',
                },
              },
              register: false,
              requestPass: {
                endpoint: '/ApiAuthentication/ResetPassword',
              },
              token: {
                class: NbAuthJWTToken,
                key: 'content', // this parameter tells where to look for the token
              },
              errors: {
                key: 'data.message',
                getter: errorGetter,
              },
            }),
          ],
          forms: {
            login: {
              strategy: options.securityName,
              redirectDelay: 500,
              showMessages: {     // show/not show success/error messages
                success: true,
                error: true,
              },
            },
            logout: {
              strategy: options.securityName,
            },
            register: {
              strategy: options.securityName,
            },
            requestPassword: {
              strategy: options.securityName,
            },
            validation: {},
          },
        }).providers,
        NbSecurityModule.forRoot(options.nbSecurityOptions).providers,
        {provide: NbRoleProvider, useClass: RoleProvider},
      ],
    };
  }


}
