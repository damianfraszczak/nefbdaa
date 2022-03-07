import {Component, EventEmitter, forwardRef, Injector, Input, OnInit, Output} from '@angular/core';
import {AbstractControl, ControlValueAccessor, FormGroup, NG_VALUE_ACCESSOR} from '@angular/forms';
import {EditFieldDefinitionModel} from '../../../models/dynamic-form/edit-field-definition-model';
import {FormMode} from '../../../models/dynamic-form/dynamic-field-enums';
import {
  getNormalInputType,
  getObjectValue,
  hasLabel,
  isAnyDateField,
  isBoolean,
  isBooleanAsSelect,
  isDate,
  isDateTime,
  isDualBox,
  isFile,
  isHtml,
  isImage,
  isMap,
  isMultipleSelect,
  isNormalInput,
  isRadio,
  isRequired,
  isSelect,
  isTextArea,
  isTime,
} from '../../../utils/field-definitions-utils';
import {FieldChangedModel} from '../../../models/dynamic-form/field-changed-model';
import {MODEL_MAPPER_SERVICE} from '../../../shared-constants';
import {NgSelectComponent} from '@ng-select/ng-select';
import {concat, Observable, of, Subject} from 'rxjs';
import {distinctUntilChanged, switchMap, tap} from 'rxjs/operators';
import {SelectFieldOptionModel} from '../../../models/dynamic-form/select-field-option-model';
import {extractContent} from '../../../utils/mappers';
import {InputType} from '../../../models/dynamic-form/input-type-model';
import {FormConfig} from '../../../models/dynamic-form/edit-object-model';
import {formatDateTimeForApi, getDate, getDateWithoutTime, isDateString} from '../../../utils/date-utils';
// import '../../../utils/ckeditor.loader';
// import 'ckeditor';


export let APP_INJECTOR: Injector;

@Component({
  selector: 'ngx-dynamic-field',
  templateUrl: './dynamic-field.component.html',
  styleUrls: ['./dynamic-field.component.scss'],
  // changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => DynamicFieldComponent), multi: true},
  ],
})
export class DynamicFieldComponent implements ControlValueAccessor, OnInit {
  @Input()
  object: any;
  @Input()
  fieldDefinition: EditFieldDefinitionModel;
  @Input()
  formMode: FormMode = FormMode.EDIT;
  @Input()
  formGroup: FormGroup;
  @Input()
  printLabels = true;
  @Input()
  formConfig?: FormConfig;
  @Output()
  valueChanged = new EventEmitter<FieldChangedModel>();

  @Input()
  allValuesChanged: EventEmitter<FieldChangedModel>;

  dropdownLoading = false;


  referencedFieldInput = new Subject<any>();
  selectDataSource$: Observable<SelectFieldOptionModel[]>;
  addInlineReferencedFieldValue: any;

  constructor(private injector: Injector) {
    APP_INJECTOR = injector;
  }

  ngOnInit(): void {

    if (this.isSelect(this.fieldDefinition)) {
      if (this.fieldDefinition.optionsFilteredOn && this.getCrudMapperService()) {
        this.selectDataSource$ = concat(
          of(this.fieldDefinition.options), // default items
          this.referencedFieldInput.pipe(
            distinctUntilChanged(),
            tap(() => this.dropdownLoading = true),
            switchMap(val => this.getCrudMapperService()
              .getCrudServiceForModel(this.fieldDefinition.modelType)
              .GetOptionsForEdit({
                additionalRule: {
                  condition: 'and', rules: [{
                    condition: 'and',
                    field: this.fieldDefinition.optionsFilteredOn.fieldToCompare,
                    value: this.getSelectValueToFilter(this.fieldDefinition, val),
                    operator: this.fieldDefinition.optionsFilteredOn.operator,
                    type: this.fieldDefinition.queryType,
                  }],
                },
                editedObject: this.getObjectWithCurrentValues(),
                fieldName: this.fieldDefinition.parameterName,
                modelType: this.formConfig.modelType,
              }).pipe(extractContent(), tap(() => this.dropdownLoading = false))),
          ),
        );

        // subscribe to value changes
        this.allValuesChanged.subscribe(evt => {
          if (evt.name === this.fieldDefinition.optionsFilteredOn.onFieldChanged) {
            this.referencedFieldInput.next(evt.newVal);
          }
        });
      } else {
        this.selectDataSource$ = concat(
          of(this.fieldDefinition.options),
        );
      }
      if (this.fieldDefinition.addInlineConfig) {
        this.allValuesChanged.subscribe(evt => {
          if (evt.name === this.fieldDefinition.addInlineConfig.referencedPropertyName) {
            this.addInlineReferencedFieldValue = evt.newVal;
          }
        });
      }
    }
  }

  propagateChange: any = () => {
  };

  registerOnChange(fn): void {
    this.propagateChange = fn;
  }

  registerOnTouched(fn: any): void {
  }

  setDisabledState(isDisabled: boolean): void {
  }

  writeValue(obj: any): void {

  }

  errorsMessagesMapping = {
    'required': 'You must enter a value',
    'email': 'Not a valid email',
    'confirmPassword': 'Passwords are different',
  };

  getErrorMessage(abstractControl: AbstractControl): string {
    if (abstractControl == null) {
      return '';
    }

    if (abstractControl.hasError('required')) {
      return this.fieldDefinition.placeholder + ' is required';
    }
    if (abstractControl.hasError('email')) {
      return 'Not a valid email';
    }
    if (abstractControl.hasError('confirmPassword')) {
      return 'Passwords are different';
    }
    if (abstractControl.hasError('number') && (this.fieldDefinition.inputType === InputType.SELECT
      || this.fieldDefinition.inputType === InputType.SELECT_MULTIPLE)) {
      return this.fieldDefinition.placeholder + ' is required';
    }
    if (abstractControl.hasError('number')) {
      return 'Value should be numeric';
    }
    if (abstractControl.hasError('owlDateTimeFilter')) {
      const min = this.getFilterDateValue(this.fieldDefinition, 'min');
      const max = this.getFilterDateValue(this.fieldDefinition, 'max');
      if (min && max) {
        return 'Value must be >= ' + min + ' and <= ' + max;
      }
      if (max) {
        return 'Value must be <= ' + max;
      }
      return 'Value must be >= ' + min;
    }

    return '';
  }

  isReadonly(fieldDefinition: EditFieldDefinitionModel): boolean {
    return !fieldDefinition.editable || !this.isEditable();
  }

  isEditable() {
    return this.formMode !== FormMode.VIEW;
  }

  isMap(fieldDefinition: EditFieldDefinitionModel) {
    return isMap(fieldDefinition);
  }

  isNormalInput(fieldDefinition: EditFieldDefinitionModel) {
    return isNormalInput(fieldDefinition);
  }

  isRequired(fieldDefinition: EditFieldDefinitionModel) {
    return isRequired(fieldDefinition);
  }

  getNormalInputType(fieldDefinition: EditFieldDefinitionModel) {
    return getNormalInputType(fieldDefinition);
  }

  isDate(fieldDefinition: EditFieldDefinitionModel) {
    return isDate(fieldDefinition);
  }

  isTime(fieldDefinition: EditFieldDefinitionModel) {
    return isTime(fieldDefinition);
  }

  isDateTime(fieldDefinition: EditFieldDefinitionModel) {
    return isDateTime(fieldDefinition);
  }

  isBoolean(fieldDefinition: EditFieldDefinitionModel) {
    return isBoolean(fieldDefinition);
  }

  isBooleanAsSelect(fieldDefinition: EditFieldDefinitionModel) {
    return isBooleanAsSelect(fieldDefinition);
  }

  isTextArea(fieldDefinition: EditFieldDefinitionModel) {
    return isTextArea(fieldDefinition);
  }

  hasLabel(fieldDefinition: EditFieldDefinitionModel) {
    return hasLabel(fieldDefinition);
  }

  isDualBox(fieldDefinition: EditFieldDefinitionModel) {
    return isDualBox(fieldDefinition);
  }

  isRadio(fieldDefinition: EditFieldDefinitionModel) {
    return isRadio(fieldDefinition);
  }

  isMultipleSelect(fieldDefinition: EditFieldDefinitionModel) {
    return isMultipleSelect(fieldDefinition);
  }

  isSelect(fieldDefinition: EditFieldDefinitionModel) {
    return isSelect(fieldDefinition);
  }

  isHtml(fieldDefinition: EditFieldDefinitionModel) {
    return isHtml(fieldDefinition);
  }

  isFile(fieldDefinition: EditFieldDefinitionModel) {
    return isFile(fieldDefinition);
  }

  isImage(fieldDefinition: EditFieldDefinitionModel) {
    return isImage(fieldDefinition);
  }

  checkValue(parameterName: string, value: any) {
    if (value !== this.object[parameterName]) {
      this.valueChanged.emit({
        name: parameterName,
        newVal: getObjectValue(this.fieldDefinition, value),
      });
    }
  }

  addReferencedItem(select: NgSelectComponent) {

    const additionalParams = {};
    if (this.fieldDefinition.addInlineConfig) {
      if (this.fieldDefinition.addInlineConfig.referencedPropertyName) {
        let value = this.object[this.fieldDefinition.addInlineConfig.referencedPropertyValue];
        if (!value) {
          value = this.fieldDefinition.addInlineConfig.referencedPropertyValue;
        }
        if (this.addInlineReferencedFieldValue) {
          value = this.addInlineReferencedFieldValue;
        }
        additionalParams[this.fieldDefinition.addInlineConfig.referencedPropertyName] = value;
      }
    }

    this.getCrudMapperService()
      .showEditDialog(this.fieldDefinition.modelType, additionalParams,
        {header: 'Create ' + this.fieldDefinition.placeholder})
      .subscribe(x => {
        select.select(select.itemsList.addItem({
          id: x.optionId,
          value: x.optionText,
          optionId: x.optionId,
          optionText: x.optionText,
          label: x.optionText,
        }));

      });
  }

  canBeAdded(): boolean {
    return this.fieldDefinition.modelType && this.fieldDefinition.editable &&
      this.fieldDefinition.addInlineConfig && this.getCrudMapperService() != null;
  }

  getCrudMapperService() {
    return APP_INJECTOR.get(MODEL_MAPPER_SERVICE);
  }

  editReferencedItem(select: NgSelectComponent) {

  }

  private getSelectValueToFilter(fieldDefinition: EditFieldDefinitionModel, val: any) {
    let toReturn = getObjectValue(fieldDefinition, val);
    if (toReturn === '') {
      toReturn = '0';
    }
    return toReturn;
  }

  private getObjectWithCurrentValues() {
    const current = Object.assign({}, this.object);
    Object.keys(this.formGroup.controls).forEach(key => {
      let newVal = this.formGroup.get(key).value;
      if (isAnyDateField(this.fieldDefinition)) {
        if (isDateString(newVal)) {
          newVal = formatDateTimeForApi(newVal);
        }
      }
      if (this.fieldDefinition.inputType === InputType.SELECT_MULTIPLE &&
        Array.isArray(newVal) && newVal.length > 0 && newVal[0] === '') {
        newVal = [];
      }
      current[key] = newVal;
    });
    return current;
  }

  getFilterDateValue(fieldDefinition, name) {
    let val = fieldDefinition[name] ?
      (this.object[fieldDefinition[name]] ? this.object[fieldDefinition[name]] : fieldDefinition[name])
      : null;
    val = getDateWithoutTime(getDate(val));
    return val;
  }

  getDateFilter(fieldDefinition: EditFieldDefinitionModel) {
    return (date: Date): boolean => {
      const min = this.getFilterDateValue(fieldDefinition, 'min');
      const max = this.getFilterDateValue(fieldDefinition, 'max');
      date = getDateWithoutTime(date);
      if (min && max) {
        return date >= min && date <= max;
      } else if (min) {
        return date >= min;
      } else if (max) {
        return date <= max;
      }

      return true;
    };
  }

  getDynamicValue(fieldDefinition: EditFieldDefinitionModel, attributeName: string) {
    const dynamicVal = fieldDefinition[attributeName];
    if (dynamicVal) {
      const objVal = this.object[dynamicVal];
      if (objVal) {
        return objVal;
      } else {
        return dynamicVal;
      }
    }
    return null;
  }
}
