/* tslint:disable */
import { ValidatorType } from './validator-type';
export interface FormValidator {
  validatorType?: ValidatorType;
  min?: number;
  max?: number;
  regex?: string;
  errorMessage?: string;
}
