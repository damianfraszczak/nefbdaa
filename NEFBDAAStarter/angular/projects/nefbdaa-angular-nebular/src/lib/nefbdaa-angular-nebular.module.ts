import {ModuleWithProviders, NgModule} from '@angular/core';
import {NebularConfigOptions, NebularModule} from './nebular/nebular.module';
import {ComponentsModule} from './components/components.module';
import {CommonModule} from '@angular/common';
import {RequiredIfDirective} from './directives/required-if-directive';
import {BackgroundDirective} from './directives/background.directive';
import {ShowAuthedDirective} from './directives/show-authenticated.directive';
import {ScrollIntoViewDirective} from './directives/scroll-into-view.directive';
import {ServicesModule} from './services/services.module';

const DIRECTIVES = [
  ScrollIntoViewDirective,
  RequiredIfDirective,
  ShowAuthedDirective,
  BackgroundDirective,
];

@NgModule({
  declarations: [
    DIRECTIVES,
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    NebularModule,

  ],
  exports: [
    ComponentsModule,
    NebularModule,
    DIRECTIVES,
  ],
})
export class NefbdaaAngularNebularModule {
  static forRoot(options: NebularConfigOptions): ModuleWithProviders {
    return <ModuleWithProviders>{
      ngModule: NefbdaaAngularNebularModule,
      providers: [
        ...ServicesModule.forRoot().providers,
        ...ComponentsModule.forRoot().providers,
        ...NebularModule.forRoot(options).providers,
      ],
    };
  }
}
