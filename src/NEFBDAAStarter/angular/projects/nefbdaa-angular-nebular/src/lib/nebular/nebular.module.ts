import {ModuleWithProviders, NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ThemeModule} from './theme/theme.module';
import {ConfigModule} from './config/config.module';
import {NbAclOptions} from '@nebular/security';


export class NebularConfigOptions {
  nbSecurityOptions?: NbAclOptions = {
    accessControl: {
      Admin: {
        create: '*',
        edit: '*',
        remove: '*',
        list: '*',
        manage: '*',
      },
      SuperAdmin: {
        parent: 'admin',
        manage: '*',
      },
    },
  };
  securityName: string;
}

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ThemeModule,
    ConfigModule,

  ],
  exports: [
    ThemeModule,
    ConfigModule,
  ],
})
export class NebularModule {
  static forRoot(options: NebularConfigOptions): ModuleWithProviders {
    return <ModuleWithProviders>{
      ngModule: NebularModule,
      providers: [
        ThemeModule.forRoot().providers,
        ConfigModule.forRoot(options).providers,
      ],
    };
  }
}
