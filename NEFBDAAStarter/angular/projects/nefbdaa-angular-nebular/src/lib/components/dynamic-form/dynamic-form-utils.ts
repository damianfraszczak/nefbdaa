import {FormGroupModel, FormModel} from '../../models/dynamic-form/edit-object-model';
import {EditFieldDefinitionModel} from '../../models/dynamic-form/edit-field-definition-model';
import {AbstractControl, FormControl, FormGroup} from '@angular/forms';
import {FormMode} from '../../models/dynamic-form/dynamic-field-enums';
import {evalExpression, evalStringExpression} from './expression-utils';
import {equalsIgnoringCase} from '../../utils/utils';
import {DynamicFormComponent} from './dynamic-form/dynamic-form.component';


export function getFormGroupCurrentValue(formGroup: FormGroup, fieldName: string, defaultVal: () => object = null) {
  const field = formGroup.get(fieldName);
  if (field) {
    return field.value;
  }
  if (defaultVal) {
    return defaultVal();
  }
  return null;
}

export function getFormFieldConfig(formGroups: Array<FormGroupModel>, fieldName: string): EditFieldDefinitionModel {
  formGroups.forEach(fg => {
    fg.editFields.forEach(field => {
      if (field.parameterName.toLowerCase() === fieldName.toLowerCase()) {
        return field;
      }
    });
  });
  return null;
}


export function getErrorMessage(abstractControl: AbstractControl): string {
  if (abstractControl == null) {
    return '';
  }
  if (abstractControl.hasError('required')) {
    return 'You must enter a value';
  }
  if (abstractControl.hasError('email')) {
    return 'Not a valid email';
  }
  if (abstractControl.hasError('confirmPassword')) {
    return 'Passwords are different';
  }
  if (abstractControl.hasError('number') && (this.fieldDefinition.inputType === 'select'
    || this.fieldDefinition.inputType === 'selectMultiple')) {
    return this.fieldDefinition.placeholder + ' is required';
  }
  if (abstractControl.hasError('number')) {
    return 'Value should be numeric';
  }
  return '';
}

export function getAllFormEditFields(formModel: FormModel<any>): EditFieldDefinitionModel[] {
  return getAllFormGroupsEditFieldsWithFGVisibilityCheck(formModel.formConfig.formGroups,
    formModel.formConfig.formMode);
}

export function isFormGroupVisibleBasedOnMetadata(fg: FormGroupModel, formMode: FormMode) {
  if (fg && equalsIgnoringCase(formMode, FormMode.ADD)
    && fg.visibleOnCreateForm) {
    return true;
  }
  if (fg && equalsIgnoringCase(formMode, FormMode.EDIT)
    && fg.visibleOnUpdateForm) {
    return true;
  }
  if (fg && equalsIgnoringCase(formMode, FormMode.VIEW)
    && fg.visibleOnUpdateForm) {
    return true;
  }
}

export function getAllFormGroupsEditFieldsWithFGVisibilityCheck(formGroups: Array<FormGroupModel>,
                                                                formMode?: FormMode) {
  const editFields = [];
  formGroups.forEach(fg => {
    if (isFormGroupVisibleBasedOnMetadata(fg, formMode)) {
      if (fg.formGroupField) {
        editFields.push(fg.formGroupField);
      }
      fg.editFields.forEach(x => editFields.push(x));
      if (fg.formGroups) {
        getAllFormGroupsEditFields(fg.formGroups).forEach(x => editFields.push(x));
      }
    }
  });
  return editFields;
}

export function getAllFormGroupsEditFields(formGroups: Array<FormGroupModel>) {
  const editFields = [];
  formGroups.forEach(fg => {
    if (fg.formGroupField) {
      editFields.push(fg.formGroupField);
    }
    fg.editFields.forEach(x => editFields.push(x));
    if (fg.formGroups) {
      getAllFormGroupsEditFields(fg.formGroups).forEach(x => editFields.push(x));
    }
  });
  return editFields;
}

export function getFormErrors(form: AbstractControl) {
  if (form instanceof FormControl) {
    // Return FormControl errors or null
    return form.errors;
  }
  if (form instanceof FormGroup) {
    const groupErrors = form.errors;
    // Form group can contain errors itself, in that case add'em
    const formErrors = groupErrors ? {groupErrors} : {};
    Object.keys(form.controls).forEach(key => {
      // Recursive call of the FormGroup fields
      const error = this.getFormErrors(form.get(key));
      if (error !== null) {
        // Only add error if not null
        formErrors[key] = error;
      }
    });
    // Return FormGroup errors or null
    return Object.keys(formErrors).length > 0 ? formErrors : null;
  }
}


export function isFieldVisibleBasedOnMetadata(fieldDefinition: EditFieldDefinitionModel, formMode: FormMode) {
  let shouldBeVisible = false;
  if (fieldDefinition && equalsIgnoringCase(formMode, FormMode.ADD)
    && fieldDefinition.displayOnCreationForm) {
    shouldBeVisible = true;
  }
  if (fieldDefinition && equalsIgnoringCase(formMode, FormMode.EDIT)
    && fieldDefinition.displayOnUpdateForm) {
    shouldBeVisible = true;
  }
  if (fieldDefinition && equalsIgnoringCase(formMode, FormMode.VIEW)
    && fieldDefinition.displayOnUpdateForm) {
    shouldBeVisible = true;
  }
  return shouldBeVisible;
}

export function isFieldVisibleBasedOnComputedFields(
  dynamicForm: DynamicFormComponent<any>,
  formGroup: FormGroup,
  fieldDefinition: EditFieldDefinitionModel) {
  if (fieldDefinition.visibilityExpression && fieldDefinition.visibilityExpressionParams) {
    return evalExpression(evalStringExpression(
      fieldDefinition.visibilityExpression,
      fieldDefinition.visibilityExpressionParams),
      // TODO
      [dynamicForm],
      [fieldDefinition.visibilityExpressionParams.map(x => getFormGroupCurrentValue(formGroup, x))]);
  }
  return true;
}
