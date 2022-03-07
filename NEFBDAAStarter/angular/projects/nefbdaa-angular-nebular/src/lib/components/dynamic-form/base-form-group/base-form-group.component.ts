import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormGroupModel, FormModel} from '../../../models/dynamic-form/edit-object-model';
import {FormGroup} from '@angular/forms';
import {EditFieldDefinitionModel} from '../../../models/dynamic-form/edit-field-definition-model';
import {equalsIgnoringCase} from '../../../utils/utils';
import {FormLayout, FormMode} from '../../../models/dynamic-form/dynamic-field-enums';
import {FieldChangedModel} from '../../../models/dynamic-form/field-changed-model';
import {evalExpression, evalStringExpression} from '../expression-utils';
import {getAllFormGroupsEditFields} from '../dynamic-form-utils';

@Component({
  selector: 'ngx-base-form-group',
  template: ``,
  styles: [],
})
export class BaseFormGroupComponent<T> {

  @Input()
  public formModel: FormModel<T>;
  @Input()
  public formGroup: FormGroup;
  @Input()
  public formGroupModels: Array<FormGroupModel>;
  @Output()
  valueChanged = new EventEmitter<FieldChangedModel>();
  @Input()
  allValuesChanged: EventEmitter<FieldChangedModel>;

  private allFields: EditFieldDefinitionModel[];

  private visibleGroups: number = -1;

  isFieldVisible(fieldDefinition: EditFieldDefinitionModel) {
    let shouldBeVisible = false;
    if (fieldDefinition && equalsIgnoringCase(this.formModel.formConfig.formMode, FormMode.ADD)
      && fieldDefinition.displayOnCreationForm) {
      shouldBeVisible = true;
    }
    if (fieldDefinition && equalsIgnoringCase(this.formModel.formConfig.formMode, FormMode.EDIT)
      && fieldDefinition.displayOnUpdateForm) {
      shouldBeVisible = true;
    }
    if (fieldDefinition && equalsIgnoringCase(this.formModel.formConfig.formMode, FormMode.VIEW)
      && fieldDefinition.displayOnUpdateForm) {
      shouldBeVisible = true;
    }
    if (fieldDefinition.visibilityExpression && fieldDefinition.visibilityExpressionParams) {
      shouldBeVisible = evalExpression(evalStringExpression(
        fieldDefinition.visibilityExpression,
        fieldDefinition.visibilityExpressionParams)
        ,
        [this], [fieldDefinition.visibilityExpressionParams.map(x => this.getFormGroupCurrentValue(x))]);
    }
    return shouldBeVisible;
  }

  isLayout(formLayout: string, layout: string): boolean {
    return equalsIgnoringCase(layout, formLayout);
  }

  getFormGroupLayout(formGroup: FormGroupModel): string {

    if (formGroup.formLayout !== FormLayout.ONE && this.getNumberOfVisibleGroups() === 1) {
      return this.getLayoutClass(FormLayout.ONE);
    }
    // if (formGroup.formLayout !== FormLayout.DEFAULT) {
    //   return this.getLayoutClass(formGroup.formLayout);
    // } else {
    //   return this.getLayoutClass(FormLayout.ONE);
    // }
    return this.getLayoutClass(formGroup.formLayout);
  }

  getFieldLayout(fieldDefinition: EditFieldDefinitionModel, formGroup: FormGroupModel): string {
    // if (fieldDefinition.formLayout !== FormLayout.DEFAULT) {
    //   return this.getLayoutClass(fieldDefinition.formLayout);
    // } else {
    //   return this.getFormGroupLayout(formGroup);
    // }
    return this.getLayoutClass(fieldDefinition.formLayout);
  }

  getLayoutClassFromComponent(component: { formLayout?: FormLayout }): string {
    return this.getLayoutClass(component.formLayout);
  }

  getLayoutClass(formLayout?: FormLayout): string {

    if (formLayout === null) {
      formLayout = FormLayout.DEFAULT;
    }
    switch (formLayout) {
      case FormLayout.ONE:
        return 'col-md-12';
      case FormLayout.TWO:
        return 'block-form col-md-6';
      case FormLayout.THREE:
        return 'block-form col-md-4';
      case FormLayout.FOUR:
        return 'block-form col-md-3';
      case FormLayout.SIX:
        return 'block-form col-md-2';
      default:
        return 'col-md-12';
    }
  }


  isFormGroupVisible(formGroup: FormGroupModel): boolean {
    let visible = this.getFormGroupFields(formGroup).filter(x => this.isFieldVisible(x)).length > 0;
    if (!formGroup.visibleOnCreateForm && equalsIgnoringCase(this.formModel.formConfig.formMode, FormMode.ADD)) {
      return false;
    }
    if (!formGroup.visibleOnUpdateForm && equalsIgnoringCase(this.formModel.formConfig.formMode, FormMode.EDIT)) {
      return false;
    }
    if (formGroup.visibilityExpression && formGroup.visibilityExpressionParams) {
      visible = evalExpression(evalStringExpression(
        formGroup.visibilityExpression,
        formGroup.visibilityExpressionParams)
        ,
        [this], formGroup.visibilityExpressionParams.map(x => this.getFormGroupCurrentValue(x)));
    }

    return visible;
  }

  isFormGroupFieldsVisibleBasedOnGroupField(formGroup: FormGroupModel) {
    let visible = true;
    if (formGroup.formGroupField) {
      visible = this.getFormGroupCurrentValue(formGroup.formGroupField.parameterName);
    }
    return visible;
  }

  getFormGroupCurrentValue(fieldName: string) {
    const field = this.formGroup.get(fieldName);
    if (field) {
      return field.value;
    }
    return null;
  }

  private getEditFields(): EditFieldDefinitionModel[] {
    if (this.allFields == null) {
      this.allFields = getAllFormGroupsEditFields(this.formGroupModels);

    }
    return this.allFields;
  }

  private getNumberOfVisibleGroups() {
    if (this.visibleGroups === -1) {
      this.visibleGroups = this.formGroupModels.filter(x => this.isFormGroupVisible(x)).length;
    }
    return this.visibleGroups;
  }

  private getFormGroupFields(formGroup: FormGroupModel) {
    if (formGroup.editFields && formGroup.editFields.length > 0) {
      return formGroup.editFields;
    } else if (formGroup.formGroups) {
      const fields = [];
      formGroup.formGroups.forEach(fg => {
        fields.push(...fg.editFields);
      });
      return fields;
    } else {
      return [];
    }
  }
}
