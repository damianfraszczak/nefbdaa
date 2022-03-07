import {ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {FormModel} from '../../../models/dynamic-form/edit-object-model';
import {formatDateTimeForApi, isDateString} from '../../../utils/date-utils';
import {getObjectValue, isAnyDateField, isMultipleSelect} from '../../../utils/field-definitions-utils';
import {CustomValidators, getValidator} from '../../../utils/custom-validators';
import {EditFieldDefinitionModel} from '../../../models/dynamic-form/edit-field-definition-model';
import {FormMode} from '../../../models/dynamic-form/dynamic-field-enums';
import {FieldChangedModel} from '../../../models/dynamic-form/field-changed-model';
import {
  getAllFormEditFields,
  isFieldVisibleBasedOnComputedFields,
  isFieldVisibleBasedOnMetadata,
} from '../dynamic-form-utils';
import {InputType} from '../../../models/dynamic-form/input-type-model';


@Component({
  selector: 'ngx-dynamic-form',
  templateUrl: './dynamic-form.component.html',
  styleUrls: ['./dynamic-form.component.scss'],
})
export class DynamicFormComponent<T> implements OnInit {
  @Input()
  public formModel: FormModel<T>;
  @Input()
  public form: FormGroup;
  @Input()
  public formTitle: string;
  @Input()
  public displayGlobalErrors: boolean = false;
  @Output()
  valueChanged = new EventEmitter<FieldChangedModel>();

  editFields: Array<EditFieldDefinitionModel> = [];

  constructor(
    private cdr: ChangeDetectorRef) {
  }

  markAllAsTouched() {
    this.form.markAllAsTouched();
  }

  ngOnInit() {
    this.form = this.generateForm();
    this.cdr.detectChanges();
    this.initCombinedFields();
    // this.valueChanged.subscribe(newVal => {
    //   // const changedEntity = {};
    //   // changedEntity[newVal.name] = newVal.newVal;
    //   // const expression = evalStringExpression(
    //   //   'object[\'firstName\'] === object[\'lastName\']',
    //   //   ['object', 'fields']);
    //   // console.log(evalExpression(expression
    //   //   ,
    //   //   [this], [Object.assign(this.formModel.object, changedEntity), this.editFields]));
    // });
  }

  public updateModel() {

    this.editFields.filter(e => e.editable).forEach((fieldDefinition) => {
        const formValue: any = this.form.get(fieldDefinition.parameterName);
        if (formValue != null) {
          let newVal = formValue.value;
          if (isAnyDateField(fieldDefinition)) {
            if (isDateString(newVal)) {
              newVal = formatDateTimeForApi(newVal);
            }
          }
          if (fieldDefinition.inputType === InputType.SELECT_MULTIPLE &&
            Array.isArray(newVal) && newVal.length > 0 && newVal[0] === '') {
            newVal = [];
          }
          this.formModel.object[fieldDefinition.parameterName] = newVal;
        }
      },
    );
  }

  setValue(parameterName: string, value: any) {
    const control = this.form.get(parameterName);
    if (control != null) {
      const fieldDefinition = this.getEditField(parameterName);
      if (fieldDefinition != null) {
        control.setValue(this.getValueForField(fieldDefinition, value));
      }

    }
  }

  getEditField(parameterName: string): EditFieldDefinitionModel {
    return this.editFields.find(x => x.parameterName === parameterName);
  }

  setCombinedValue(sourceParameterName: string,
                   sourceValue: any,
                   destinationParameterName: string,
                   additionalParameterName: string) {
    const field = this.getEditField(sourceParameterName);
    if (field != null) {
      const val = field.options.find(x => x.optionId === sourceValue);
      if (val != null) {
        this.setValue(destinationParameterName, val.optionAdditionalInfo[additionalParameterName]);
      } else {
        // do not change if not set
        this.setValue(destinationParameterName, '');
      }

    }
  }

  generateForm(): FormGroup {
    const formGroup = new FormGroup({});

    this.editFields = getAllFormEditFields(this.formModel);

    this.editFields.filter(x => this.isFieldVisible(x))
      .map(e => ({
        parameterName: e.parameterName,
        formControl: this.createFormControl(e),
      }))
      .forEach(e => {
        if (!formGroup.contains(e.parameterName)) {
          formGroup.addControl(e.parameterName, e.formControl);
        }
      });
    formGroup.setValidators(CustomValidators.passwordsMatchValidator);
    this.valueChanged.subscribe((evt: FieldChangedModel) => {
      // computed fields
      this.editFields.filter(x => x.computedOn && x.computedOn.onFieldChanged === evt.name)
        .forEach(fc => {
          this.setCombinedValue(evt.name,
            evt.newVal,
            fc.computedOn.setField,
            fc.computedOn.fromProp);
        });
      // options filtered fields
      // need to do it in field

      // disable/enable controls
      this.editFields.forEach(field => {
        const formControl = this.form.get(field.parameterName);
        const isVisible = this.isFieldVisibleCurrently(field);
        if (formControl && isVisible !== formControl.enabled) {
          if (isVisible && !this.isReadonly(field)) {
            formControl.enable();
          } else {
            formControl.disable();
          }
        }
      })
    });

    return formGroup;
  }

  isFieldVisible(fieldDefinition: EditFieldDefinitionModel) {
    return isFieldVisibleBasedOnMetadata(fieldDefinition, this.formModel.formConfig.formMode);
  }

  isFieldVisibleCurrently(fieldDefinition: EditFieldDefinitionModel) {
    return this.isFieldVisible(fieldDefinition) &&
      isFieldVisibleBasedOnComputedFields(this, this.form, fieldDefinition);
  }

  isReadonly(fieldDefinition: EditFieldDefinitionModel): boolean {
    const result = !fieldDefinition.editable || !this.isEditable();
    return result;
  }

  isEditable() {
    return this.formModel.formConfig.formMode !== FormMode.VIEW;
  }

  private createFormControl(fieldDefinition: EditFieldDefinitionModel): FormControl {
    const val = this.getValueForField(fieldDefinition, this.formModel.object[fieldDefinition.parameterName]);
    const validators = getValidator(fieldDefinition.validators);
    return new FormControl({
      value: val,
      disabled: this.isReadonly(fieldDefinition),
    }, validators);
  }

  private getValueForField(fieldDefinition: EditFieldDefinitionModel, valueFromObject: any): any {
    let value: any = getObjectValue(fieldDefinition, valueFromObject);
    if (isAnyDateField(fieldDefinition) && value === '0001-01-01T00:00:00') {
      value = String(new Date());
    }
    if (isMultipleSelect(fieldDefinition)) {
      value = value.split(',');
      // null needed for empty selection
      if (value.length === 1 && !value[0]) {
        value = [];
      }
    }
    return value;
  }


  private initCombinedFields() {
    // init computed fields
    this.editFields.filter(x => x.computedOn && x.computedOn.onFieldChanged)
      .forEach(fc => {
        const currentComputedValue = getObjectValue(fc, this.formModel.object[fc.parameterName]);
        if (currentComputedValue === null || String(currentComputedValue) === '') {
          const currentVal = getObjectValue(fc, this.formModel.object[fc.computedOn.onFieldChanged]);
          this.setCombinedValue(fc.computedOn.onFieldChanged,
            currentVal,
            fc.computedOn.setField,
            fc.computedOn.fromProp);
        }
      });
  }
}

