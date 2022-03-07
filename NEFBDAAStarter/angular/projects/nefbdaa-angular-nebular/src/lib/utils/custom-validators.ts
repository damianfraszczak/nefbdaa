import {AbstractControl, ValidatorFn, Validators} from '@angular/forms';
import {Injectable} from '@angular/core';
import {FormValidatorModel} from '../models/dynamic-form/edit-object-model';
import {FormValidatorType} from '../models/dynamic-form/dynamic-field-enums';

@Injectable()
export class CustomValidators {
  static isTotalNumberValidator(control: AbstractControl) {
    return Validators.pattern('^[0-9]*$');
  }

  static passwordsMatchValidator(c: AbstractControl): { invalid: boolean } {
    const pass = c.get('password');
    const confPass = c.get('confirmPassword');
    if (pass && confPass && pass.value !== confPass.value) {
      confPass.setErrors({'confirmPassword': {value: c.value}});
      return {invalid: true};
    }
    if (confPass) {
      confPass.setErrors(null);
    }
    return null;
  }
}

export function getValidator(validatorType: FormValidatorModel | FormValidatorModel[]): ValidatorFn[] {
  if (!Array.isArray(validatorType)) {
    validatorType = [validatorType];
  }
  return validatorType.map(v => getValidatorFor(v));
}

export function getValidatorFor(validatorModel: FormValidatorModel): ValidatorFn {
  switch (validatorModel.validatorType) {
    case FormValidatorType.REQUIRED: {
      return Validators.required;
    }
    case FormValidatorType.EMAIL: {
      return Validators.email;
    }
    case FormValidatorType.TOTAL_NUMBER: {
      return CustomValidators.isTotalNumberValidator;
    }
    default : {
      return null;
    }
  }
}
