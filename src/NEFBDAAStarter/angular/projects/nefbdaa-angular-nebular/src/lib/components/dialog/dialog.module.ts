import {ModuleWithProviders, NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DeleteItemDialogComponent} from './delete/delete-item-dialog.component';
import {TextEditorDialogComponent} from './text-editor-dialog/text-editor-dialog.component';
import {ConfirmationDialogComponent} from './confirmation-dialog/confirmation-dialog.component';
import {DynamicEditDialogComponent} from './dynamic-edit-dialog/dynamic-edit-dialog.component';
import {ThemeModule} from '../../nebular/theme/theme.module';
import {DialogService} from './dialog.service';
import {CKEditorModule} from 'ng2-ckeditor';
import {TextareaEditorDialogComponent} from './textarea-editor-dialog/textarea-editor-dialog.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MultiSelectDialogComponent} from './multi-select-dialog/multi-select-dialog.component';
import {PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface, PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {TextInputDialogComponent} from './text-input-dialog/text-input-dialog.component';
import {AlertDialogComponent} from './alert-dialog/alert-dialog.component';
import {DynamicFormModule} from '../dynamic-form/dynamic-form.module';
import {PasswordDialogComponent} from './password-dialog/password-dialog.component';
import {TableSelectDialogComponent} from './table-select-dialog/table-select-dialog.component';
import {NgSelectModule} from '@ng-select/ng-select';
import {MultiListSelectDialogComponent} from './multi-list-select-dialog/multi-list-select-dialog.component';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
};

const ENTRY_COMPONENTS = [
  DeleteItemDialogComponent,
  TextEditorDialogComponent,
  TextareaEditorDialogComponent,
  MultiSelectDialogComponent,
  DynamicEditDialogComponent,
  ConfirmationDialogComponent,
  TextInputDialogComponent,
  AlertDialogComponent,
  PasswordDialogComponent,
  TableSelectDialogComponent,
  MultiListSelectDialogComponent,
];

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    FormsModule,
    ReactiveFormsModule,
    PerfectScrollbarModule,
    DynamicFormModule,
    CKEditorModule,
    NgSelectModule,
  ],
  declarations: [
    ...ENTRY_COMPONENTS,
  ],
  exports: [],
  providers: [],
  entryComponents: [
    ...ENTRY_COMPONENTS,
  ],
})
export class DialogModule {
  static forRoot(): ModuleWithProviders {
    return <ModuleWithProviders>{
      ngModule: DialogModule,
      providers: [DialogService,
        {
          provide: PERFECT_SCROLLBAR_CONFIG,
          useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG,
        }],
    };
  }
}
