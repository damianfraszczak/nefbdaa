/* tslint:disable */
type ExceptionUiType =
  'NOTIFICATION' |
  'DIALOG';
module ExceptionUiType {
  export const NOTIFICATION: ExceptionUiType = 'NOTIFICATION';
  export const DIALOG: ExceptionUiType = 'DIALOG';
  export function values(): ExceptionUiType[] {
    return [
      NOTIFICATION,
      DIALOG
    ];
  }
}

export { ExceptionUiType }