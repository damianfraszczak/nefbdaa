export interface LoggedUserModel {
  roles?: Array<string>;
  phone?: string;
  email: string;
  firstName: string;
  lastName: string;
  optionText?: string;
  id?: number;
  guid?: string;
  optionId?: string;
}
