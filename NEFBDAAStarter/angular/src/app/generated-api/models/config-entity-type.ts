/* tslint:disable */
type ConfigEntityType =
  'STRING' |
  'INTEGER' |
  'DOUBLE' |
  'TEXT' |
  'LONG_TEXT' |
  'HTML' |
  'DOCUMENTS';
module ConfigEntityType {
  export const STRING: ConfigEntityType = 'STRING';
  export const INTEGER: ConfigEntityType = 'INTEGER';
  export const DOUBLE: ConfigEntityType = 'DOUBLE';
  export const TEXT: ConfigEntityType = 'TEXT';
  export const LONG_TEXT: ConfigEntityType = 'LONG_TEXT';
  export const HTML: ConfigEntityType = 'HTML';
  export const DOCUMENTS: ConfigEntityType = 'DOCUMENTS';
  export function values(): ConfigEntityType[] {
    return [
      STRING,
      INTEGER,
      DOUBLE,
      TEXT,
      LONG_TEXT,
      HTML,
      DOCUMENTS
    ];
  }
}

export { ConfigEntityType }