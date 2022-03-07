export interface ConfigModel {
  name: string;
  appKey: string;
  type: 'sTRING' | 'iNTEGER' | 'dOUBLE' | 'tEXT' | 'lONG_TEXT' | 'hTML' | 'dOCUMENTS';
  value: string;
  optionText?: string;
  id?: number;
  guid?: string;
  optionId?: string;
  isDeleted?: boolean;
}
