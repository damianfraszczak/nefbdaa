import {ModuleWithProviders, NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ThemeModule} from '../nebular/theme/theme.module';
import {ServicesModule} from '../services/services.module';
import {DialogModule} from './dialog/dialog.module';
import {ngfModule} from 'angular-file';
import {QueryBuilderModule} from 'angular2-query-builder';
import {FormsModule} from '@angular/forms';
import {DatePickerModule} from './date-picker/date-picker.module';
import {SmartTableUtilitiesModule} from './smart-table-utilities/smart-table-utilities.module';
import {DynamicFormModule} from './dynamic-form/dynamic-form.module';
import {FiltersModule} from './filters/filters.module';
import {DocumentsComponent} from './table-components/documents/documents.component';
import {TableComponent} from './table-components/table-component/table.component';
import {NotesComponent} from './table-components/notes/notes.component';
import {AppLoggingComponent} from './table-components/app-logging/app-logging.component';
import {ConfigListComponent} from './table-components/config-list/config-list.component';
import {ConfigBasedTableComponent} from './table-components/config-based-table-component/config-based-table.component';
import { DefaultTableComponentComponent } from './table-components/default-table-component/default-table-component.component';

const SHARED_COMPONENTS = [
  DocumentsComponent,
  TableComponent,
  NotesComponent,
  AppLoggingComponent,
  ConfigListComponent,
  ConfigBasedTableComponent,
];

@NgModule({
  declarations: [SHARED_COMPONENTS, DefaultTableComponentComponent],
  imports: [
    CommonModule,
    DialogModule,
    ServicesModule,
    ThemeModule,
    ngfModule,
    QueryBuilderModule,
    FormsModule,
    DatePickerModule,
    SmartTableUtilitiesModule,
    DynamicFormModule,
    FiltersModule,

  ],
  exports: [
    SHARED_COMPONENTS,
    QueryBuilderModule,
    DatePickerModule,
    SmartTableUtilitiesModule,
    DynamicFormModule,
    DialogModule,
    FiltersModule,
  ],
  providers: [],

})
export class ComponentsModule {
  static forRoot(): ModuleWithProviders {
    return <ModuleWithProviders>{
      ngModule: ComponentsModule,
      providers: [
        ...DialogModule.forRoot().providers,
      ],
    };
  }
}
