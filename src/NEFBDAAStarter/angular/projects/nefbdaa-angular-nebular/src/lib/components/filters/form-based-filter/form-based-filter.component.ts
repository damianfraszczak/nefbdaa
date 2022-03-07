import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FilterRuleModel} from '../../../models/dynamic-form/filter-rule-model';
import {FormModel} from '../../../models/dynamic-form/edit-object-model';
import {buildServerRuleForField, getDefaultQueryFilterRule} from '../../../utils/query-builder';
import {EditFieldDefinitionModel} from '../../../models/dynamic-form/edit-field-definition-model';
import {FormLayout, FormMode} from '../../../models/dynamic-form/dynamic-field-enums';
import {InputType} from '../../../models/dynamic-form/input-type-model';
import {isFilterable} from '../../smart-table-utilities/smart-table/smart-table-utils';
import {equalsIgnoringCase} from '../../../utils/utils';

@Component({
  selector: 'ngx-form-based-filter',
  templateUrl: './form-based-filter.component.html',
  styleUrls: ['./form-based-filter.component.scss'],
})
export class FormBasedFilterComponent<T> implements OnInit {

  filterModel: FormModel<T> = null;
  filterableFields: EditFieldDefinitionModel[] = [];

  @Input()
  printLabels: boolean = true;
  @Input()
  fieldLayout = FormLayout.FOUR;
  @Input()
  hiddenFields: Array<string> = [];
  @Input()
  visibleFields: Array<string> = [];
  @Input()
  formModel: FormModel<T>;
  @Output()
  public filterEvent: EventEmitter<FilterRuleModel> = new EventEmitter<FilterRuleModel>();

  constructor() {
  }

  ngOnInit() {
    this.formModel.formConfig.formGroups.forEach(fg => {
      fg.editFields
        .filter(x => isFilterable(x))
        .filter(x => this.isFieldVisible(x, this.visibleFields, this.hiddenFields))
        .sort((x, y) => {
          return x.filterOrder - y.filterOrder;
        })
        .forEach(x => {
          x.order = x.filterOrder;
          if (x.inputType === InputType.SELECT) {
            x.inputType = InputType.SELECT_MULTIPLE;
          }
          x.editable = true;
          x.validators = [];
          x.displayOnCreationForm = true;
          x.displayOnUpdateForm = true;
          this.filterableFields.push(x);
          x.formLayout = this.fieldLayout;
          x.addInlineConfig = null;
          x.optionsFilteredOn = null;
        });
    });
    this.filterModel = {
      formConfig: {
        formMode: FormMode.ADD,
        formLayout: FormLayout.ONE,
        printLabels: this.printLabels,
        formGroups: [{
          formLayout: FormLayout.ONE,
          editFields: this.filterableFields,
          visibleOnCreateForm: true,
        }],
      },
      object: this.formModel.object,
    };
  }

  clearFilter() {
    this.filterModel.object = {} as T;
    this.filter();
  }

  filter() {
    const rule = getDefaultQueryFilterRule();
    this.filterableFields.forEach(field => {
      const fieldValue = this.formModel.object[field.parameterName];
      if (this.isCorrectValue(fieldValue, field)) {
        rule.rules.push(buildServerRuleForField(fieldValue, field));
      }
    });
    this.filterEvent.emit(rule);
  }

  private isCorrectValue(fieldValue: any, field: EditFieldDefinitionModel) {
    if (!fieldValue) {
      return false;
    }
    if ((field.inputType === InputType.NUMBER || field.inputType === InputType.SELECT) && Number(fieldValue) === 0) {
      return false;
    }
    if (field.inputType === InputType.SELECT_MULTIPLE && fieldValue.length === 0) {
      return false;
    }
    return true;
  }


  private isFieldVisible(field: EditFieldDefinitionModel, visibleColumns: Array<string>, hiddenColumns: Array<string>) {
    if (visibleColumns.length === 0 && hiddenColumns.length === 0) {
      return field.display;
    }
    const isInVisible = visibleColumns.length === 0 ? field.display : visibleColumns
      .find(x => equalsIgnoringCase(x, field.parameterName)) != null;
    const isInHidden = hiddenColumns.find(x => equalsIgnoringCase(x, field.parameterName)) != null;
    if (isInHidden) {
      return false;
    }
    return isInVisible;
  }
}
