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

export { FormGroupLayoutComponent }