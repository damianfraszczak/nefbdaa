/* tslint:disable */
type QueryOperatorEnum =
  'In' |
  'Not_In' |
  'Equal' |
  'Not_Equal' |
  'Between' |
  'Not_Between' |
  'Less' |
  'Less_Or_Equal' |
  'Greater' |
  'Greater_Or_Equal' |
  'Begins_With' |
  'Not_Begins_With' |
  'Contains' |
  'Like' |
  'Not_Contains' |
  'Ends_With' |
  'Not_Ends_With' |
  'Is_Empty' |
  'Is_Not_Empty' |
  'Is_Null' |
  'Is_Not_Null';
module QueryOperatorEnum {
  export const IN: QueryOperatorEnum = 'In';
  export const NOT_IN: QueryOperatorEnum = 'Not_In';
  export const EQUAL: QueryOperatorEnum = 'Equal';
  export const NOT_EQUAL: QueryOperatorEnum = 'Not_Equal';
  export const BETWEEN: QueryOperatorEnum = 'Between';
  export const NOT_BETWEEN: QueryOperatorEnum = 'Not_Between';
  export const LESS: QueryOperatorEnum = 'Less';
  export const LESS_OR_EQUAL: QueryOperatorEnum = 'Less_Or_Equal';
  export const GREATER: QueryOperatorEnum = 'Greater';
  export const GREATER_OR_EQUAL: QueryOperatorEnum = 'Greater_Or_Equal';
  export const BEGINS_WITH: QueryOperatorEnum = 'Begins_With';
  export const NOT_BEGINS_WITH: QueryOperatorEnum = 'Not_Begins_With';
  export const CONTAINS: QueryOperatorEnum = 'Contains';
  export const LIKE: QueryOperatorEnum = 'Like';
  export const NOT_CONTAINS: QueryOperatorEnum = 'Not_Contains';
  export const ENDS_WITH: QueryOperatorEnum = 'Ends_With';
  export const NOT_ENDS_WITH: QueryOperatorEnum = 'Not_Ends_With';
  export const IS_EMPTY: QueryOperatorEnum = 'Is_Empty';
  export const IS_NOT_EMPTY: QueryOperatorEnum = 'Is_Not_Empty';
  export const IS_NULL: QueryOperatorEnum = 'Is_Null';
  export const IS_NOT_NULL: QueryOperatorEnum = 'Is_Not_Null';
  export function values(): QueryOperatorEnum[] {
    return [
      IN,
      NOT_IN,
      EQUAL,
      NOT_EQUAL,
      BETWEEN,
      NOT_BETWEEN,
      LESS,
      LESS_OR_EQUAL,
      GREATER,
      GREATER_OR_EQUAL,
      BEGINS_WITH,
      NOT_BEGINS_WITH,
      CONTAINS,
      LIKE,
      NOT_CONTAINS,
      ENDS_WITH,
      NOT_ENDS_WITH,
      IS_EMPTY,
      IS_NOT_EMPTY,
      IS_NULL,
      IS_NOT_NULL
    ];
  }
}

export { QueryOperatorEnum }