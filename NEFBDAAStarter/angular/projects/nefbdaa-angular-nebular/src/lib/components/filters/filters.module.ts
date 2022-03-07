import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AdvancedFilterComponent} from './advanced-filter/advanced-filter.component';
import {FormBasedFilterComponent} from './form-based-filter/form-based-filter.component';
import {ThemeModule} from '../../nebular/theme/theme.module';
import {QueryBuilderModule} from 'angular2-query-builder';
import {DatePickerModule} from '../date-picker/date-picker.module';
import {DynamicFormModule} from '../dynamic-form/dynamic-form.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';


const FILTERS_COMPONENTS = [
  AdvancedFilterComponent,
  FormBasedFilterComponent,
];

@NgModule({
  declarations: [
    FILTERS_COMPONENTS,
  ],
  imports: [
    CommonModule,
    ThemeModule,
    QueryBuilderModule,
    DynamicFormModule,
    DatePickerModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports: [
    FILTERS_COMPONENTS,
  ],
})
export class FiltersModule {
}
