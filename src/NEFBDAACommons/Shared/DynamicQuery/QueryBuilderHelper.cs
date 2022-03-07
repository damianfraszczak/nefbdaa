using NEFBDAACommons.DynamicForms.Models;
using NEFBDAACommons.Shared.Extensions;
using NEFBDAACommons.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NEFBDAACommons.Shared.DynamicQuery
{
    public enum QueryConditionEnum
    {
        AND,
        OR
    }

    public enum QueryOperatorEnum
    {
        In,
        Not_In,
        Equal,
        Not_Equal,
        Between,
        Not_Between,
        Less,
        Less_Or_Equal,
        Greater,
        Greater_Or_Equal,
        Begins_With,
        Not_Begins_With,
        Contains,
        Like,
        Not_Contains,
        Ends_With,
        Not_Ends_With,
        Is_Empty,
        Is_Not_Empty,
        Is_Null,
        Is_Not_Null
    }

    public class FilterRuleByReflection
    {
        public string FieldName { get; set; }
        public QueryOperatorEnum Operator { get; set; }
        public object Value { get; set; }
    }

    public class QueryBuilderHelper
    {
        public static FilterRule GetEmptyRule(QueryConditionEnum defaultCondition = QueryConditionEnum.AND)
        {
            return new FilterRule()
            {
                Condition = defaultCondition.ToString().ToLower(),
                Rules = new List<FilterRule>()
            };
        }

        public static FilterRule BuildFilterRuleByReflection(Type type, FilterRuleByReflection rule)
        {
            var condition = rule.Operator.ToString().ToLower();
            return new FilterRule()
            {
                Condition = "and",
                Field = rule.FieldName,
                Id = rule.FieldName,
                Input = "NA",
                Operator = condition,
                Type = GetQueryTypeString(ReflectionHelper.GetPropertyByName(type, rule.FieldName)),
                Value = rule.Value
            };
        }

        public static FilterRule BuildFilterRule(Type type, QueryConditionEnum condition,
            params FilterRuleByReflection[] rules)
        {
            return new FilterRule()
            {
                Condition = Enum.GetName(typeof(QueryConditionEnum), condition).ToLower(),
                Rules = rules.Select(x => BuildFilterRuleByReflection(type, x)).ToList()
            };
        }

        public static FilterRule BuildFilterRuleForManyFields(Type type,
            QueryConditionEnum condition,
            QueryOperatorEnum queryOperator, string value, params string[] fieldNames)
        {
            return new FilterRule()
            {
                Condition = Enum.GetName(typeof(QueryConditionEnum), condition).ToLower(),
                Rules = fieldNames.Select(x => new FilterRule()
                {
                    Condition = Enum.GetName(typeof(QueryConditionEnum), condition).ToLower(),
                    Field = x,
                    Id = x,
                    Input = "NA",
                    Operator = Enum.GetName(typeof(QueryOperatorEnum), queryOperator).ToLower(),
                    Type = GetQueryTypeString(ReflectionHelper.GetPropertyByName(type, x)),
                    Value = value
                }).ToList()
            };
        }

        public static string GetQueryTypeString(InputType inputType, PropertyInfo propertyInfo)
        {
            if (ReflectionHelper.IsDateTimeType(propertyInfo.PropertyType))
            {
                // enum serialized camelcase
                return inputType.ToString().ToCamelCase();
            }
            else if (!ReflectionHelper.IsEnumType(propertyInfo.PropertyType) &&
                     (InputType.Select == inputType || InputType.Radio == inputType ||
                      InputType.SelectMultiple == inputType))
            {
                return "integer";
            }
            else
            {
                return GetQueryTypeString(propertyInfo);
            }
        }

        public static string GetQueryTypeString(PropertyInfo prop)
        {
            return GetQueryTypeString(ReflectionHelper.GetNullableTypeIfIs(prop.PropertyType));
        }

        public static string GetQueryTypeString(Type propertyType)
        {
            var type = string.Empty;
            if (ReflectionHelper.IsLikeIntegerType(propertyType))
            {
                type = "integer";
            }
            else if (ReflectionHelper.IsNumericType(propertyType))
            {
                type = "double";
            }

            else if (ReflectionHelper.IsDateTimeType(propertyType))
            {
                type = "date";
            }
            else if (ReflectionHelper.IsStringType(propertyType))
            {
                type = "string";
            }
            else if (ReflectionHelper.IsBooleanType(propertyType))
            {
                type = "boolean";
            }
            else if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
            {
                type = "guid";
            }
            else if (ReflectionHelper.IsEnumType(propertyType))
            {
                type = ReflectionHelper.GetNullableTypeIfIs(propertyType).Name;
            }
            else if (ReflectionHelper.IsEnumerableOrArrayType(propertyType))
            {
                type = GetQueryTypeString(propertyType.GetGenericArguments()[0]);
            }

            return type;
        }

        public static Type GetTypeForStringType(string typeString)
        {
            Type type = null;
            switch (typeString)
            {
                case "integer":
                    type = typeof(int);
                    break;
                case "guid":
                    type = typeof(Guid);
                    break;
                case "double":
                    type = typeof(double);
                    break;
                case "string":
                    type = typeof(string);
                    break;
                case "date":
                case "datetime":
                case "dateTime":
                case "time":
                    type = typeof(DateTime);
                    break;
                case "boolean":
                    type = typeof(bool);
                    break;
                default:
                    type = EnumHelper.GetEnumTypeBySimpleName(typeString);
                    if (type == null)
                    {
                        throw new Exception($"Unexpected data type {typeString}");
                    }

                    break;
            }

            return type;
        }
    }
}