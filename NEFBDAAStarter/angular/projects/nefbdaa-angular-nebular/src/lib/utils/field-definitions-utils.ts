import {FieldDefinitionModel} from '../models/dynamic-form/field-definition-model';
import {OptionsFieldDefinitionModel} from '../models/dynamic-form/options-field-definition-model';
import {SelectFieldOptionModel} from '../models/dynamic-form/select-field-option-model';
import {EditFieldDefinitionModel} from '../models/dynamic-form/edit-field-definition-model';
import {getValidatorFor} from './custom-validators';
import {Validators} from '@angular/forms';
import {InputType} from '../models/dynamic-form/input-type-model';

export function getSelectedOptionsForField(fieldDefinition: FieldDefinitionModel,
                                           cellOptions: OptionsFieldDefinitionModel[],
                                           selectedVal: string): SelectFieldOptionModel[] {
  const result: SelectFieldOptionModel[] = [];
  if (selectedVal) {
    const foundOptions = getOptionsForField(fieldDefinition, cellOptions);
    if (selectedVal.indexOf(',') > 0) {
      selectedVal.split(',').forEach(val => {
        result.push(getOptionForValue(foundOptions, val));
      });
    } else {
      result.push(getOptionForValue(foundOptions, selectedVal));
    }
  }

  return result;
}

export function getFieldsByName(fieldDefinitions: Array<EditFieldDefinitionModel>, parameterName: string) {
  return fieldDefinitions.find(x => x.parameterName === parameterName);
}

export function getOptionForValue(options: SelectFieldOptionModel[], selectedVal: any): SelectFieldOptionModel {
  const selectedValStr = selectedVal + '';
  return options.filter(x => x.optionId === selectedValStr).find(() => true);
}

export function getOptionsForField(fieldDefinition: FieldDefinitionModel,
                                   cellOptions: OptionsFieldDefinitionModel[]): Array<SelectFieldOptionModel> {
  if (cellOptions != null) {
    const result = cellOptions
      .filter(x => x.parameterName === fieldDefinition.parameterName)
      .map(x => x.options)
      // get first
      .find(() => true);
    if (result != null) {
      return result;
    }
  }
  return [];
}

export function mapSelectedOptionsToValueList(options: Array<SelectFieldOptionModel>): string {
  return options.filter(x => x != null).map(x => x.optionId).join(',');
}

export function mapSelectedOptionsToTextList(options: Array<SelectFieldOptionModel>): string {
  return options.filter(x => x != null).map(x => x.optionText).join(',');
}

export function isNormalInput(fieldDefinition: EditFieldDefinitionModel): boolean {
  return !(
    isSelect(fieldDefinition) ||
    isBoolean(fieldDefinition) ||
    isTextArea(fieldDefinition) ||
    isRadio(fieldDefinition) ||
    isHeart(fieldDefinition) ||
    isDate(fieldDefinition) ||
    isTime(fieldDefinition) ||
    isDateTime(fieldDefinition) ||
    isDualBox(fieldDefinition) ||
    isMap(fieldDefinition) ||
    isFile(fieldDefinition) ||
    isRating(fieldDefinition) ||
    isImage(fieldDefinition) ||
    isHtml(fieldDefinition) ||
    isBooleanAsSelect(fieldDefinition)
  );
}

export function isSelect(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.SELECT_MULTIPLE ||
    fieldDefinition.inputType === InputType.SELECT;
}

export function isBoolean(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.BOOLEAN;
}

export function isBooleanAsSelect(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.BOOLEAN_AS_SELECT;
}

export function isRadio(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.RADIO;
}

export function isDate(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.DATE;
}

export function isTime(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.TIME;
}

export function isDateTime(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.DATE_TIME;
}

export function isHeart(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.HEART;
}

export function isDualBox(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.DUAL_BOX;
}

export function isRating(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.RATING;
}

export function isTextArea(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.TEXT_AREA;
}

export function isMap(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.GEOLOCATION;
}

export function isHtml(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.HTML;
}

export function isFile(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.FILE;
}

export function isImage(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.IMAGE;
}

export function isMultipleSelect(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.inputType === InputType.SELECT_MULTIPLE;
}

export function isAnyDateField(fieldDefinition: EditFieldDefinitionModel) {
  return isDateTime(fieldDefinition) || isDate(fieldDefinition) || isTime(fieldDefinition);
}

export function isRequired(fieldDefinition: EditFieldDefinitionModel): boolean {
  return fieldDefinition.validators.filter(e => getValidatorFor(e) === Validators.required).length > 0;
}

export function hasLabel(fieldDefinition: EditFieldDefinitionModel): boolean {
  return !(
    isBoolean(fieldDefinition)
  );
}

export function getNormalInputType(fieldDefinition: EditFieldDefinitionModel): string {
  if (fieldDefinition.inputType === InputType.INTEGER) {
    return 'number';
  }
  if (fieldDefinition.inputType === InputType.DATE_TIME) {
    return 'datetime-local';
  }
  return fieldDefinition.inputType;
}

export function getObjectValue(fieldDefinition: EditFieldDefinitionModel, valueFromObject: any) {
  if (fieldDefinition.inputType === InputType.GEOLOCATION) {
    return valueFromObject;
  } else if (!isMultipleSelect(fieldDefinition) && Array.isArray(valueFromObject)) {
    if ((valueFromObject.length > 0 && valueFromObject[0] === '') || valueFromObject.length === 0) {
      return '';
    } else {
      return valueFromObject[0];
    }
  } else {
    return valueFromObject ? String(valueFromObject) : '';
  }

}
