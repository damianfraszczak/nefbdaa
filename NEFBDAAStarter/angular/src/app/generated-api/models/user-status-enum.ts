/* tslint:disable */
type UserStatusEnum =
  'None' |
  'Active' |
  'Inactive';
module UserStatusEnum {
  export const NONE: UserStatusEnum = 'None';
  export const ACTIVE: UserStatusEnum = 'Active';
  export const INACTIVE: UserStatusEnum = 'Inactive';
  export function values(): UserStatusEnum[] {
    return [
      NONE,
      ACTIVE,
      INACTIVE
    ];
  }
}

export { UserStatusEnum }