/* tslint:disable */
import { ConfigEntityType } from './config-entity-type';
export interface ConfigViewModel {
  name: string;
  appKey: string;
  type: ConfigEntityType;
  value: string;
  additionalInfo?: string;
  hiddenConfig?: boolean;
  optionText?: string;
  id?: number;
  guid?: string;
  optionId?: string;
  isDeleted?: boolean;
  optionAdditionalInfo?: {[key: string]: string};
  hideFromFilter?: boolean;
  hideFromEdit?: boolean;
}
