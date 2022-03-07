import {QueryBuilderConfig, Rule, RuleSet} from 'angular2-query-builder';
import {FieldDefinitionModel} from '../models/dynamic-form/field-definition-model';
import {OptionsFieldDefinitionModel} from '../models/dynamic-form/options-field-definition-model';
import {FilterRuleModel} from '../models/dynamic-form/filter-rule-model';
import {formatDateTimeForApi} from './date-utils';
import {EditFieldDefinitionModel} from '../models/dynamic-form/edit-field-definition-model';
import {isAnyDateField, isMultipleSelect} from './field-definitions-utils';
import {InputType} from '../models/dynamic-form/input-type-model';

function getFieldTypeForQuery(fieldDefinition: FieldDefinitionModel) {
  if (fieldDefinition.filterSelectMultiple) {
    return InputType.SELECT_MULTIPLE;
  }
  return fieldDefinition.inputType;
}

export interface ObjectsMetadata {
  fieldsDefinitions: FieldDefinitionModel[];
  cellOptions: OptionsFieldDefinitionModel[];
}

export function buildServerQuery(query: RuleSet, objectsMetadata: ObjectsMetadata): FilterRuleModel {
  const result: FilterRuleModel = {
    condition: query.condition,
    rules: new Array<FilterRuleModel>(),
  };
  query.rules.forEach(rule => {
    const serverRule = buildServerRuleForRule(objectsMetadata, rule);
    if (serverRule) {
      result.rules.push(serverRule);
    }

  });
  return result;
}

export function buildQueryFieldsConfig(objectsMetadata: ObjectsMetadata): QueryBuilderConfig {
  const config: QueryBuilderConfig = {
    fields: {},

  };
  if (objectsMetadata != null && objectsMetadata.fieldsDefinitions != null) {
    objectsMetadata.fieldsDefinitions.filter(x => x.filterable).forEach(fieldDefinition => {
      if (fieldDefinition.inputType !== InputType.SELECT_MULTIPLE) {
        if (fieldDefinition.inputType === InputType.SELECT ||

          fieldDefinition.inputType === InputType.RADIO) {
          // TODO
          if (objectsMetadata.cellOptions != null) {
            const foundOptions = objectsMetadata.cellOptions
              .filter(x => x.parameterName === fieldDefinition.parameterName)[0].options;
            config.fields[fieldDefinition.parameterName] = {
              name: fieldDefinition.placeholder,
              type: 'category',
              options: foundOptions.map(x => ({
                name: x.optionText,
                id: x.optionText,
                value: x.optionId,
              })),
            };
          }
        } else {
          config.fields[fieldDefinition.parameterName] = {
            name: fieldDefinition.placeholder,
            type: getInputTypeForQueryBuilder(fieldDefinition.inputType),
          };
          if (fieldDefinition.inputType === InputType.DATE_TIME) {
            config.fields[fieldDefinition.parameterName].operators = ['>', '>=', '<=', '<', '='];
          }
        }
      }

    });
  }
  return config;
}

export function getQueryValue(currentValue: any, fieldDefinition: FieldDefinitionModel) {
  const inputType = getFieldTypeForQuery(fieldDefinition);
  switch (inputType) {
    case InputType.DATE:
    case InputType.DATE_TIME:
    case InputType.TIME:
      return formatDateTimeForApi(currentValue);
    case InputType.SELECT_MULTIPLE:
      if (currentValue && !Array.isArray(currentValue)) {
        return [currentValue];
      }
  }
  return currentValue;

}

export function getFieldNameForFilter(fieldDefinitions: FieldDefinitionModel[], fieldName: string) {
  const fieldDefinition = fieldDefinitions.find(x => x.parameterName === fieldName);
  if (fieldDefinition.filterPropertyName) {
    return fieldDefinition.filterPropertyName;
  } else {
    return fieldDefinition.parameterName;
  }
}

export function getFieldDefinition(fieldDefinitions: FieldDefinitionModel[], fieldName: string): FieldDefinitionModel {
  return fieldDefinitions.find(x => x.parameterName === fieldName);
}

export function getQueryTypeForField(fieldDefinitions: FieldDefinitionModel[], fieldName: string) {
  return getFieldDefinition(fieldDefinitions, fieldName).queryType;
}

export function getQueryOperatorTypeForField(fieldDefinitions: FieldDefinitionModel[], fieldName: string) {
  const fieldDefinition = getFieldDefinition(fieldDefinitions, fieldName);

  if (fieldDefinition.filterQueryOperator) {
    return fieldDefinition.filterQueryOperator;
  }
  const inputType = getFieldTypeForQuery(fieldDefinition);
  switch (inputType) {
    case InputType.SELECT:
    case InputType.RADIO:
    case InputType.NUMBER:
    case InputType.BOOLEAN:
      return '=';
    case InputType.SELECT_MULTIPLE:
      return 'in';
    case InputType.DATE:
      return '=';
    case InputType.DATE_TIME:
    case InputType.TIME:
      return '>=';
    default:
      return 'like';
  }
  return;
}

function getInputTypeForQueryBuilder(inputType: InputType) {
  switch (inputType) {
    case InputType.DEFAULT:
    case InputType.TEXT:
    case InputType.EMAIL:
    case InputType.URL:
    case InputType.TEL:
    case InputType.TEXT_AREA:
    case InputType.RATING:
    case InputType.HEART:
    case InputType.FILE:
    case InputType.IMAGE:
      return 'string';
    case InputType.INTEGER:
    case InputType.NUMBER:
      return 'number';
  }
  return inputType;
}

export function buildServerRuleForRule(objectsMetadata: ObjectsMetadata, queryRule: RuleSet | Rule): FilterRuleModel {
  const rawRule = queryRule as Rule;
  const rawRuleSet = queryRule as RuleSet;
  if (rawRule != null) {
    return {
      operator: rawRule.operator,
      field: getFieldNameForFilter(objectsMetadata.fieldsDefinitions, rawRule.field),
      value: rawRule.value,
      type: getQueryTypeForField(objectsMetadata.fieldsDefinitions, rawRule.field),
    };
  } else {
    return buildServerQuery(rawRuleSet, objectsMetadata);
  }
}

export function buildServerRuleForField(value: any, fieldDefinition: EditFieldDefinitionModel): FilterRuleModel {
  if (value) {
    let operator = '=';
    if (isAnyDateField(fieldDefinition)) {
      operator = '>=';
    } else if (isMultipleSelect(fieldDefinition)) {
      operator = 'in';
    } else if (fieldDefinition.inputType === InputType.TEXT_AREA ||
      fieldDefinition.inputType === InputType.TEXT ||
      fieldDefinition.inputType === InputType.HTML) {
      operator = 'like';
    }

    return {
      operator: operator,
      field: fieldDefinition.parameterName,
      value: value,
      type: fieldDefinition.queryType,
    };
  } else {
    return null;
  }

}

export function getDefaultQueryFilterRule() {
  return {
    condition: 'and',
    rules: [],
  };
}
