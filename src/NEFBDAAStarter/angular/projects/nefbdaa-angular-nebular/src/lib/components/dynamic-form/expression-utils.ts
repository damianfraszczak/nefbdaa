export function evalStringExpression(expression: string, argNames: string[]) {
  try {
    return Function(...argNames, `return ${expression};`) as any;
  } catch (error) {
    console.error(error);
  }
}

export function evalExpressionValueSetter(expression: string, argNames: string[]) {
  try {
    return Function(...argNames, `${expression} = expressionValue;`) as (value: any) => void;
  } catch (error) {
    console.error(error);
  }
}

export function evalExpression(expression: string | Function | boolean, thisArg: any, argVal: any[]): any {
  if (expression instanceof Function) {
    return expression.apply(thisArg, ...argVal);
  } else {
    return expression ? true : false;
  }
}

export function getFunctionResultOrVal(expression: any, thisArg: any = null, argVal: any[] = []): any {
  if (expression && expression instanceof Function) {
    return expression.apply(thisArg, argVal);
  } else {
    return expression;
  }
}

