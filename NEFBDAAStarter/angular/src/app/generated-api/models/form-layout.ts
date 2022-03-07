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