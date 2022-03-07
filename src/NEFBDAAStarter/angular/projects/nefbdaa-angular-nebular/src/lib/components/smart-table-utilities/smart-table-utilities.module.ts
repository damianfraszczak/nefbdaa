import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SmartTableComponent} from './smart-table/smart-table.component';
import {SmartTableActionsComponent} from './smart-table-actions/smart-table-actions.component';
import {DialogModule} from '../dialog/dialog.module';
import {ServicesModule} from '../../services/services.module';
import {ThemeModule} from '../../nebular/theme/theme.module';
import {QueryBuilderModule} from 'angular2-query-builder';
import {DatePickerModule} from '../date-picker/date-picker.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {SmartTableFilersComponent} from './smart-table-filers/smart-table-filers.component';
import {DateFilterComponent} from './filters/date-filter.component';
import {TimeFilterComponent} from './filters/time-filter.component';
import {DateTimeFilterComponent} from './filters/date-time-filter.component';
import {ObservableSelectFilterComponent} from './filters/observable-select-filter.component';
import {DynamicFormModule} from '../dynamic-form/dynamic-form.module';
import {FiltersModule} from '../filters/filters.module';
import {DateEditorComponent} from './editors/date-editor.component';
import {DateTimeEditorComponent} from './editors/date-time-editor.component';
import {TimeEditorComponent} from './editors/time-editor.component';
import {ObservableSelectEditorComponent} from './editors/observable-select-editor.component';
import {RouterModule} from '@angular/router';
import {CheckboxEditorComponent} from './editors/checkbox-editor.component';
import {CheckboxFilterComponent} from './filters/checkbox-filter.component';
import {CustomRenderComponent} from './smart-table/custom-renderer.component';
import {NgSelectModule} from '@ng-select/ng-select';
import {RangeDateFilterComponent} from './filters/range-date-filter.component';
import {NgPipesModule} from 'ngx-pipes';

const EDITORS = [
  DateEditorComponent,
  DateTimeEditorComponent,
  TimeEditorComponent,
  ObservableSelectEditorComponent,
  CheckboxEditorComponent,
];

@NgModule({
  declarations: [
    SmartTableComponent,
    SmartTableActionsComponent,
    SmartTableFilersComponent,
    DateFilterComponent,
    RangeDateFilterComponent,
    TimeFilterComponent,
    DateTimeFilterComponent,
    ObservableSelectFilterComponent,
    CheckboxFilterComponent,
    ...EDITORS,
    CustomRenderComponent,
  ],
  imports: [
    CommonModule,
    DialogModule,
    ServicesModule,
    ThemeModule,
    FormsModule,
    ReactiveFormsModule,
    QueryBuilderModule,
    DatePickerModule,
    DynamicFormModule,
    FiltersModule,
    RouterModule,
    NgSelectModule,
    NgPipesModule,
  ],
  exports: [
    SmartTableComponent,
    SmartTableActionsComponent,
    SmartTableFilersComponent,
    DateFilterComponent,
    RangeDateFilterComponent,
    TimeFilterComponent,
    DateTimeFilterComponent,
    ObservableSelectFilterComponent,
    CheckboxFilterComponent,
    CustomRenderComponent,
    ...EDITORS,
  ],
  entryComponents: [...EDITORS, CustomRenderComponent],
})
export class SmartTableUtilitiesModule {
}
