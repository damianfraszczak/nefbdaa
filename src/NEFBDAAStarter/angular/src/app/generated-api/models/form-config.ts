/* tslint:disable */
import { FormMode } from './form-mode';
import { FormGroupLayoutComponent } from './form-group-layout-component';
import { FormLayout } from './form-layout';
import { FormValidator } from './form-validator';
import { FormGroupViewModel } from './form-group-view-model';
export interface FormConfig {
  modelType?: string;
  title?: string;
  printLabels?: boolean;
  formMode?: FormMode;
  formGroupLayoutComponent?: FormGroupLayoutComponent;
  formGroupLayout?: FormLayout;
  validators?: {[key: string]: Array<FormValidator>};
  formGroups?: Array<FormGroupViewModel>;
}
