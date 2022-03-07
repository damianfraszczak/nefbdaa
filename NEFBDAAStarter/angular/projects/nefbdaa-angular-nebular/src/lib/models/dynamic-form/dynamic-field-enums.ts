/* tslint:disable */
type FormMode =
  'ADD' |
  'EDIT' |
  'VIEW';
module FormMode {
  export const ADD: FormMode = 'ADD';
  export const EDIT: FormMode = 'EDIT';
  export const VIEW: FormMode = 'VIEW';

  export function values(): FormMode[] {
    return [
      ADD,
      EDIT,
      VIEW
    ];
  }
}
export {FormMode}
/* tslint:disable */
type FormGroupLayoutComponent =
  'DEFAULT' |
  'CARD' |
  'SIMPLE' |
  'ACCORDION' |
  'FIELDSET' |
  'NONE' |
  'TABS';
module FormGroupLayoutComponent {
  export const DEFAULT: FormGroupLayoutComponent = 'DEFAULT';
  export const CARD: FormGroupLayoutComponent = 'CARD';
  export const SIMPLE: FormGroupLayoutComponent = 'SIMPLE';
  export const ACCORDION: FormGroupLayoutComponent = 'ACCORDION';
  export const FIELDSET: FormGroupLayoutComponent = 'FIELDSET';
  export const NONE: FormGroupLayoutComponent = 'NONE';
  export const TABS: FormGroupLayoutComponent = 'TABS';

  export function values(): FormGroupLayoutComponent[] {
    return [
      DEFAULT,
      CARD,
      SIMPLE,
      ACCORDION,
      FIELDSET,
      NONE,
      TABS
    ];
  }
}

export {FormGroupLayoutComponent}

/* tslint:disable */
type FormLayout =
  'ONE' |
  'TWO' |
  'THREE' |
  'FOUR' |
  'SIX' |
  'DEFAULT';
module FormLayout {
  export const ONE: FormLayout = 'ONE';
  export const TWO: FormLayout = 'TWO';
  export const THREE: FormLayout = 'THREE';
  export const FOUR: FormLayout = 'FOUR';
  export const SIX: FormLayout = 'SIX';
  export const DEFAULT: FormLayout = 'DEFAULT';
  export function values(): FormLayout[] {
    return [
      ONE,
      TWO,
      THREE,
      FOUR,
      SIX,
      DEFAULT
    ];
  }
}

export { FormLayout }


/* tslint:disable */
type FormValidatorType =
  'Required' |
  'Email' |
  'TotalNumber' |
  'Path';
module FormValidatorType {
  export const REQUIRED: FormValidatorType = 'Required';
  export const EMAIL: FormValidatorType = 'Email';
  export const TOTAL_NUMBER: FormValidatorType = 'TotalNumber';
  export const PATH: FormValidatorType = 'Path';
  export function values(): FormValidatorType[] {
    return [
      REQUIRED,
      EMAIL,
      TOTAL_NUMBER,
      PATH
    ];
  }
}

export { FormValidatorType }
