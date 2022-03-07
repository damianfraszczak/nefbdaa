/* tslint:disable */
export interface DocumentViewModel {
  ownerGuid?: string;
  name?: string;
  ownerDocumentType?: string;
  filePath?: string;
  originalFilename?: string;
  contentType?: string;
  fileSize?: string;
  lastUsedDate?: string;
  hits?: number;
  optionText?: string;
  creatorUserId?: number;
  createdUtc?: string;
  id?: number;
  guid?: string;
  optionId?: string;
  isDeleted?: boolean;
  optionAdditionalInfo?: {[key: string]: string};
  hideFromFilter?: boolean;
  hideFromEdit?: boolean;
}
