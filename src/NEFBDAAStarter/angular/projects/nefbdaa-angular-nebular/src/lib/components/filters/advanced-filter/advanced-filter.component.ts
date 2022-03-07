import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FieldDefinitionModel} from '../../../models/dynamic-form/field-definition-model';
import {OptionsFieldDefinitionModel} from '../../../models/dynamic-form/options-field-definition-model';
import {QueryBuilderClassNames, QueryBuilderConfig, RuleSet} from 'angular2-query-builder';
import {buildQueryFieldsConfig, buildServerQuery} from '../../../utils/query-builder';
import {FilterRuleModel} from '../../../models/dynamic-form/filter-rule-model';

@Component({
  selector: 'ngx-advanced-filter',
  templateUrl: './advanced-filter.component.html',
  styleUrls: ['./advanced-filter.component.scss'],
})
export class AdvancedFilterComponent implements OnInit {

  @Input()
  public fieldsDefinitions: FieldDefinitionModel[];
  @Input()
  public cellOptions: OptionsFieldDefinitionModel[];

  @Output()
  public filterEvent: EventEmitter<FilterRuleModel> = new EventEmitter<FilterRuleModel>();

  filterClassNames: QueryBuilderClassNames = {
    removeIcon: 'fa fa-minus',
    addIcon: 'fa fa-plus',
    arrowIcon: 'fa fa-chevron-right px-2',
    button: 'btn',
    buttonGroup: 'btn-group',
    rightAlign: 'order-12 ml-auto',
    switchRow: 'd-flex px-2',
    switchGroup: 'd-flex align-items-center',
    switchRadio: 'custom-control-input',
    switchLabel: 'custom-control-label',
    switchControl: 'custom-control custom-radio custom-control-inline',
    row: 'row p-2 m-1',
    rule: 'border',
    ruleSet: 'border',
    invalidRuleSet: 'alert alert-danger',
    emptyWarning: 'text-danger mx-auto',
    operatorControl: 'form-control',
    operatorControlSize: 'col-auto pr-0',
    fieldControl: 'form-control',
    fieldControlSize: 'col-auto pr-0',
    entityControl: 'form-control',
    entityControlSize: 'col-auto pr-0',
    inputControl: 'form-control',
    inputControlSize: 'col-auto',
  };

  filterQuery: RuleSet = null;
  filterConfig: QueryBuilderConfig = null;
  allowRuleset = false;

  constructor() {
    this.clearFilters();
  }

  filter() {
    this.filterEvent.emit(buildServerQuery(this.filterQuery,
      {cellOptions: this.cellOptions, fieldsDefinitions: this.fieldsDefinitions}));
  }

  clearFilters() {
    this.filterQuery = {
      condition: 'and',
      rules: [],
    };
  }

  ngOnInit() {
    this.filterConfig = buildQueryFieldsConfig({cellOptions: this.cellOptions, fieldsDefinitions: this.fieldsDefinitions});
  }

}
