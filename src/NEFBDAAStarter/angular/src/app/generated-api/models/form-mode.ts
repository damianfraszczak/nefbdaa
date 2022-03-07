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

export { FormMode }