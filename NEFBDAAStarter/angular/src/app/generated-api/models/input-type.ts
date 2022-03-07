/* tslint:disable */
type InputType =
  'Default' |
  'Number' |
  'Text' |
  'Email' |
  'Url' |
  'Tel' |
  'Password' |
  'Select' |
  'Radio' |
  'SelectMultiple' |
  'TextArea' |
  'Boolean' |
  'BooleanAsSelect' |
  'Date' |
  'Time' |
  'DateTime' |
  'Integer' |
  'Rating' |
  'Heart' |
  'Color' |
  'Image' |
  'File' |
  'DualBox' |
  'Geolocation' |
  'Html';
module InputType {
  export const DEFAULT: InputType = 'Default';
  export const NUMBER: InputType = 'Number';
  export const TEXT: InputType = 'Text';
  export const EMAIL: InputType = 'Email';
  export const URL: InputType = 'Url';
  export const TEL: InputType = 'Tel';
  export const PASSWORD: InputType = 'Password';
  export const SELECT: InputType = 'Select';
  export const RADIO: InputType = 'Radio';
  export const SELECT_MULTIPLE: InputType = 'SelectMultiple';
  export const TEXT_AREA: InputType = 'TextArea';
  export const BOOLEAN: InputType = 'Boolean';
  export const BOOLEAN_AS_SELECT: InputType = 'BooleanAsSelect';
  export const DATE: InputType = 'Date';
  export const TIME: InputType = 'Time';
  export const DATE_TIME: InputType = 'DateTime';
  export const INTEGER: InputType = 'Integer';
  export const RATING: InputType = 'Rating';
  export const HEART: InputType = 'Heart';
  export const COLOR: InputType = 'Color';
  export const IMAGE: InputType = 'Image';
  export const FILE: InputType = 'File';
  export const DUAL_BOX: InputType = 'DualBox';
  export const GEOLOCATION: InputType = 'Geolocation';
  export const HTML: InputType = 'Html';
  export function values(): InputType[] {
    return [
      DEFAULT,
      NUMBER,
      TEXT,
      EMAIL,
      URL,
      TEL,
      PASSWORD,
      SELECT,
      RADIO,
      SELECT_MULTIPLE,
      TEXT_AREA,
      BOOLEAN,
      BOOLEAN_AS_SELECT,
      DATE,
      TIME,
      DATE_TIME,
      INTEGER,
      RATING,
      HEART,
      COLOR,
      IMAGE,
      FILE,
      DUAL_BOX,
      GEOLOCATION,
      HTML
    ];
  }
}

export { InputType }