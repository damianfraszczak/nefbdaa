export interface SelectFieldOptionModel {
  optionText?: string;
  optionId?: string;
  optionAdditionalInfo?: { [key: string]: string };
  hideFromFilter?: boolean;
  hideFromEdit?: boolean;
}
