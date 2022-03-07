/* tslint:disable */
type ModelTypes =
  'USER' |
  'DOCUMENT' |
  'EMAIL' |
  'CONFIG' |
  'COMPANY' |
  'NOTE' |
  'LANGUAGE';
module ModelTypes {
  export const USER: ModelTypes = 'USER';
  export const DOCUMENT: ModelTypes = 'DOCUMENT';
  export const EMAIL: ModelTypes = 'EMAIL';
  export const CONFIG: ModelTypes = 'CONFIG';
  export const COMPANY: ModelTypes = 'COMPANY';
  export const NOTE: ModelTypes = 'NOTE';
  export const LANGUAGE: ModelTypes = 'LANGUAGE';
  export function values(): ModelTypes[] {
    return [
      USER,
      DOCUMENT,
      EMAIL,
      CONFIG,
      COMPANY,
      NOTE,
      LANGUAGE
    ];
  }
}

export { ModelTypes }