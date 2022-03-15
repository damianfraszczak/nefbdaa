# NEFBDAA - .NET Environment for Building Dynamic Angular Applications

## Form model configuration
``` typescript
export interface FormGroupModel {
  name?: string;
  displayName?: boolean;
  editFields?: Array<EditFieldDefinitionModel>;
  fieldsetClass?: string;
  formGroupField?: EditFieldDefinitionModel;
  formLayout?: FormLayout;
  visibilityExpression?: string;
  visibilityExpressionParams?: Array<string>;
  order?: number;
  formGroups?: Array<FormGroupModel>;
  visibleOnCreateForm?: boolean;
  visibleOnUpdateForm?: boolean;
}

export interface FormValidatorModel {
  validatorType?: FormValidatorType;
  min?: number;
  max?: number;
  regex?: string;

}

export interface FormConfig {
  modelType?:string;
  title?: string;
  formLayout?: FormLayout;
  formMode?: FormMode;
  printLabels?: boolean;
  validators?: { [key: string]: Array<FormValidatorModel> };
  formGroups?: Array<FormGroupModel>;
  formGroupLayoutComponent?: FormGroupLayoutComponent;
}

export interface FormModel<T> {
  formConfig?: FormConfig;
  object?: T;
}

export interface EditFieldDefinitionModel {
  parameterName?: string;
  editable?: boolean;
  displayOnUpdateForm?: boolean;
  editableOnCreateForm?: boolean;
  editableOnEditForm?: boolean;
  validators?: Array<FormValidatorModel>;
  options?: Array<SelectFieldOptionModel>;
  placeholder?: string;
  tableTitle?: string;
  helpText?: string;
  modelType?: string;
  displayOnCreationForm?: boolean;
  display?: boolean;
  filterable?: boolean;
  order?: number;
  filterOrder?: number;
  exportOrder?: number;
  inputType?: InputType;
  queryType?: string;
  groupName?: string;
  visibilityExpressionParams?: Array<string>;
  visibilityExpression?: string;
  formLayout?: FormLayout;
  computedOn?: ComputedConfig;
  optionsFilteredOn?: FilteredOptionsConfig;
  addInlineConfig?: AddInlineConfig;
  extraClassList?: string[];
  max?: string;
  min?: string;
}
```
- `FormModel` – containing the form configuration and edited object. This object is returned from the backend API service and consumed by the frontend to provide a dynamic UI form.
- `FormConfig` – contains the configuration for the whole form, including its: title, general form layout, list of custom validators, defined form groups, or the form display mode.  
- `FormGroupModel` – is a representation of form groups, i.e., a list of fields representing as one whole, rendered together. It consists of such configuration options as title, name, layout, set of visibility conditions to display/hide components based on computed properties of an edited object, etc.
- `EditFieldDefinitionModel` – is a representation of the object fields displayed in a form. It has a lot of configuration options. The most important are: options – for select like fields, it is a list of options to display what can be configured statically or computed automatically based on the provided conditions and data models; inputType – the type of UI component responsible for showing that field; computedOn – it allows to render fields that do not exist in a model, but they are read-only fields computed on some model properties; addInlineConfig – if the field represents a related model, it consists the configuration to add a new object on current form. There are also multiple table-specific properties such as filterable, queryType, tableTitle etc.

