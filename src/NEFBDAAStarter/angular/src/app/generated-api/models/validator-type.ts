/* tslint:disable */
type ValidatorType =
  'Required' |
  'Email' |
  'TotalNumber' |
  'Path';
module ValidatorType {
  export const REQUIRED: ValidatorType = 'Required';
  export const EMAIL: ValidatorType = 'Email';
  export const TOTAL_NUMBER: ValidatorType = 'TotalNumber';
  export const PATH: ValidatorType = 'Path';
  export function values(): ValidatorType[] {
    return [
      REQUIRED,
      EMAIL,
      TOTAL_NUMBER,
      PATH
    ];
  }
}

export { ValidatorType }