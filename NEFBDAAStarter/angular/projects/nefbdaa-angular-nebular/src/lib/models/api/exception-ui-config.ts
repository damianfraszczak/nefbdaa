export interface ExceptionUiConfig {
  timeout?: number;
  type?: ExceptionUiTypeModel;
}


type ExceptionUiTypeModel =
  'NOTIFICATION' |
  'DIALOG';
module ExceptionUiType {
  export const NOTIFICATION: ExceptionUiTypeModel = 'NOTIFICATION';
  export const DIALOG: ExceptionUiTypeModel = 'DIALOG';

  export function values(): ExceptionUiTypeModel[] {
    return [
      NOTIFICATION,
      DIALOG,
    ];
  }
}

export {ExceptionUiTypeModel}
