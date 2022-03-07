/* tslint:disable */
import { UserStatusEnum } from './user-status-enum';
export interface AppUserViewModel {
  roles?: Array<string>;
  dateOfBirth?: string;
  dateOfBirth2?: string;
  companiesCompanyId?: Array<number>;
  addressVisible?: boolean;
  address?: string;
  addressLine1?: string;
  userDocumentId?: string;
  languageId?: number;
  ownerId?: number;
  languageName?: string;
  testBool?: boolean;
  ownerCompanyId?: number;
  lanName?: string;
  myProperty?: number;
  status222?: UserStatusEnum;
  userImage?: string;
  userTypeId?: number;
  userStaticoptions?: string;
  password?: string;
  status?: UserStatusEnum;
  fcmTokens?: Array<string>;
  uid?: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  phone?: string;
  fullName?: string;
  optionText?: string;
  id?: number;
  guid?: string;
  optionId?: string;
  isDeleted?: boolean;
  optionAdditionalInfo?: {[key: string]: string};
  hideFromFilter?: boolean;
  hideFromEdit?: boolean;
}
