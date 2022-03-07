/* tslint:disable */
export interface NoteViewModel {
  ownerGuid?: string;
  note?: string;
  creatorUserId?: number;
  createdUtc?: string;
  optionText?: string;
  id?: number;
  guid?: string;
  optionId?: string;
  isDeleted?: boolean;
  optionAdditionalInfo?: {[key: string]: string};
  hideFromFilter?: boolean;
  hideFromEdit?: boolean;
}
