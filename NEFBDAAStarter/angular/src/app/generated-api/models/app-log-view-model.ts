/* tslint:disable */
export interface AppLogViewModel {
  creatorUserId?: number;
  level?: string;
  timestamp?: string;
  fileName?: string;
  lineNumber?: string;
  message?: string;
  additional?: {};
  optionText?: string;
  id?: number;
  guid?: string;
  optionId?: string;
  isDeleted?: boolean;
  optionAdditionalInfo?: {[key: string]: string};
  hideFromFilter?: boolean;
  hideFromEdit?: boolean;
}
