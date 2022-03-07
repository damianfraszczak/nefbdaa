import {NbComponentStatus} from '@nebular/theme';
import {SelectFieldOptionModel} from '../../models/dynamic-form/select-field-option-model';
import {OptionsFieldDefinitionModel} from '../../models/dynamic-form/options-field-definition-model';

export interface DialogConfigModel {
  okButtonStatus?: NbComponentStatus;
  header?: string;
  message?: string;
  status?: NbComponentStatus;
  okButtonText?: string;
  cancelButtonText?: string;
}

export interface InputDialogConfigModel extends DialogConfigModel {
  input?: string;
}

export interface ConfiguredInputDialogConfigModel extends InputDialogConfigModel {
  type: string;
  label: string;
}


export interface TableSelectDialogModel extends DialogConfigModel {
  modelType: string;
  data: Array<any>;
  cellOptions: Array<OptionsFieldDefinitionModel>;
  selected: Array<any>;
  deleteButtonHidden?: boolean;

}

export interface SelectDialogConfigModel extends DialogConfigModel {
  items: SelectFieldOptionModel[];
  selected: string[] | string;
  multiple: boolean;
}

export interface ListSelectDialogConfigModel extends SelectDialogConfigModel {
  titlePropertyName?: string;
  imagePropertyName?: string;
  namePropertyName?: string;

  showTitle?: boolean;
  showImage?: boolean;
  showName?: boolean;
}
