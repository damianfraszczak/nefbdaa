import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import {DatePickerModule} from '../date-picker/date-picker.module';
import {ThemeModule} from '../../nebular/theme/theme.module';
import {CKEditorModule} from 'ng2-ckeditor';
import {DynamicFormComponent} from './dynamic-form/dynamic-form.component';
import {AgmCoreModule} from '@agm/core';
import {MarkerLocationFieldComponent} from './custom-fields/marker-location-field/marker-location-field.component';
import {DynamicFieldComponent} from './dynamic-field/dynamic-field.component';
import {AngularDualListBoxModule} from 'angular-dual-listbox';
import {NgPipesModule} from 'ngx-pipes';
import {CardFormGroupComponent} from './card-form-group/card-form-group.component';
import {AccordionFormGroupComponent} from './accordion-form-group/accordion-form-group.component';
import {SimpleFormGroupComponent} from './simple-form-group/simple-form-group.component';
import {FieldsetFormGroupComponent} from './fieldset-form-group/fieldset-form-group.component';
import {TabsFormGroupComponent} from './tabs-form-group/tabs-form-group.component';
import {NoneFormGroupComponent} from './none-form-group/none-form-group.component';
import {BaseFormGroupComponent} from './base-form-group/base-form-group.component';
import {DateFieldComponent} from './custom-fields/date-field/date-field.component';
import {TimeFieldComponent} from './custom-fields/time-field/time-field.component';
import {DateTimeFieldComponent} from './custom-fields/date-time-field/date-time-field.component';
import {FileInputComponent} from './custom-fields/file-input/file-input.component';
import {ngfModule} from 'angular-file';
import {FormErrorsComponent} from './form-errors/form-errors.component';
import {NgSelectModule} from '@ng-select/ng-select';
import {PhotoFieldComponent} from './custom-fields/photo-field/photo-field.component';
import { DefaultTabsComponentComponent } from './default-forms/default-tabs-component/default-tabs-component.component';
import { ModelBasedFormComponent } from './default-forms/model-based-form-component/model-based-form.component';

const DYNAMIC_COMPONENTS = [
  DynamicFormComponent,
  MarkerLocationFieldComponent,
  DynamicFieldComponent,
  FileInputComponent,
  PhotoFieldComponent,
];

@NgModule({
  declarations: [
    DYNAMIC_COMPONENTS,
    CardFormGroupComponent,
    AccordionFormGroupComponent,
    SimpleFormGroupComponent,
    FieldsetFormGroupComponent,
    TabsFormGroupComponent,
    NoneFormGroupComponent,
    BaseFormGroupComponent,
    DateFieldComponent,
    TimeFieldComponent,
    DateTimeFieldComponent,
    FileInputComponent,
    FormErrorsComponent,
    DefaultTabsComponentComponent,
    ModelBasedFormComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DatePickerModule,
    ThemeModule,
    CKEditorModule,
    FormsModule,
    ReactiveFormsModule,
    DatePickerModule,
    AgmCoreModule,
    AngularDualListBoxModule,
    NgPipesModule,
    ngfModule,
    NgSelectModule,
  ],
    exports: [
        DYNAMIC_COMPONENTS,
        DefaultTabsComponentComponent,
    ],
})
export class DynamicFormModule {
}
