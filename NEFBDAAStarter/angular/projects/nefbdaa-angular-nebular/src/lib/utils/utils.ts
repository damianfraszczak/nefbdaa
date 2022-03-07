import {tap} from 'rxjs/internal/operators';
import {HttpParams} from '@angular/common/http';
import {deepExtend} from '@nebular/auth';
import {ActivatedRoute} from '@angular/router';
import {Observable} from 'rxjs';
import {map, withLatestFrom} from 'rxjs/operators';


export function equalsIgnoringCase(v1: string): (v: string) => boolean;
export function equalsIgnoringCase(v1: string, v2: string): boolean;
export function equalsIgnoringCase(v1: string, v2?: string): boolean | ((v: string) => boolean) {
  return arguments.length === 1 ?
    (v: string) => equalsIgnoringCase(v1, v)
    : nullSafeToLowerCase(v1) === nullSafeToLowerCase(v2);
}

export function containsIgnoringCase(v1: string): (v: string) => boolean;
export function containsIgnoringCase(v1: string, v2: string): boolean;
export function containsIgnoringCase(v1: string, v2?: string): boolean | ((v: string) => boolean) {
  return arguments.length === 1 ?
    (v: string) => containsIgnoringCase(v1, v)
    : nullSafeToLowerCase(v1).includes(nullSafeToLowerCase(v2));
}

export function nullSafeToLowerCase(text: string): string {
  return text ? text.toLowerCase() : text;
}

export function emptyStringIfNull(text: string): string {
  return text ? text : '';
}

export function isUserValid({firstName = '', lastName = '', email = '', countryId = ''}): boolean {
  return [firstName, lastName, email, countryId].every(x => x.length > 0);
}

export function testValue<T>(value: T, predicate: (T) => boolean, rejectMessage?: string): Promise<T> {
  return new Promise((resolve, reject) =>
    predicate(value) ?
      resolve(value) :
      reject(rejectMessage));
}

export function objectFieldIncludesValue(object: Object, fieldName: string,
                                         value: string, caseSensitive = true): boolean {
  const objectValue = caseSensitive ? String(object[fieldName]) : String(object[fieldName]).toLowerCase();
  value = caseSensitive ? value : value.toLowerCase();
  return objectValue.includes(value);
}

export function someObjectFieldsIncludesValues(object: Object, fieldsNames: string[],
                                               searchValue: string, caseSensitive = true) {
  return fieldsNames.some(fieldName => objectFieldIncludesValue(object, fieldName, searchValue, caseSensitive));
}

export function getNumberOrValue(val: any) {
  const n = Number(val);
  if (n === NaN) {
    return val;
  } else {
    return n;
  }
}

export function getOrDefault(val: any, def: any = null) {
  if (val) {
    return val;
  } else {
    return def;
  }
}

/**
 * Fills 1st argument object with properties (deep) of 2nd argument - WITHOUT CLONING.
 */
export function fillMissingProperties<T>(destination: T, filler): T {
  Object.keys(filler).forEach(x => {
      if (destination[x] === undefined) {
        destination[x] = filler[x];
      } else if (destination[x] instanceof Object) {
        fillMissingProperties(destination[x], filler[x]);
      }
    },
  );
  return destination;
}

export function fillMissingPropertiesClones<T>(destination: T, filler): T {
  return fillMissingProperties(destination, deepExtend({}, filler));
}

export function reduceMapping(values: any[], mapper: (any) => any): any {
  return values.reduce((accumulator, value) => {
    accumulator[value] = mapper(value);
    return accumulator;
  }, {});
}

export function isEquivalent(a, b) {
  // Create arrays of property names
  const aProps = Object.getOwnPropertyNames(a);
  const bProps = Object.getOwnPropertyNames(b);

  // If number of properties is different,
  // objects are not equivalent
  if (aProps.length !== bProps.length) {
    return false;
  }

  for (let i = 0; i < aProps.length; i++) {
    const propName = aProps[i];

    // If values of same property are not equal,
    // objects are not equivalent
    if (a[propName] !== b[propName]) {
      return false;
    }
  }

  // If we made it this far, objects
  // are considered equivalent
  return true;
}

export function tapInEveryCase(action: (x: any) => void) {
  return tap(action, action);
}


export function capitalize(text: string | any) {
  if (!(typeof text === 'string')) {
    text = String(text);
  }
  return text.charAt(0).toUpperCase() + text.slice(1);
}

export type SetterHandler<T> = { [P in keyof T]?: (propertyValue: T[P], target: T) => void };

export function setterProxy<T extends object>(proxyTarget: T, setterHandler: SetterHandler<T>): T {
  const handler = {
    set(target, property, value, receiver) {
      const returnValue = Reflect.set(target, property, value, receiver);
      if (setterHandler[property]) {
        setterHandler[property](value, target);
      }
      return returnValue;
    },
  };
  return new Proxy<T>(proxyTarget, handler);
}

export function deepClone<T>(object: T): T {
  return deepExtend({}, object);
}

export function getParamValueQueryString(paramName: string): string {
  const url = window.location.href;
  let paramValue;
  if (url.includes('?')) {
    const httpParams = new HttpParams({fromString: url.split('?')[1]});
    paramValue = httpParams.get(paramName);
  }
  return paramValue;
}

export function truncateStringByLetters(value: string, limit = 40, completeWords = true, ellipsis = '…') {
  if (value.length < limit) {
    return `${value.substr(0, limit)}`;

  }
  if (completeWords) {
    limit = value.substr(0, limit).lastIndexOf(' ');
  }
  return `${value.substr(0, limit)}${ellipsis}`;
}

export function truncateStringByWords(value: string, limit: number = 40, trail: String = '…'): string {
  let result = value || '';

  if (value) {
    const words = value.split(/\s+/);
    if (words.length > Math.abs(limit)) {
      if (limit < 0) {
        limit *= -1;
        result = trail + words.slice(words.length - limit, words.length).join(' ');
      } else {
        result = words.slice(0, limit).join(' ') + trail;
      }
    }
  }

  return result;
}

export function getFromDictIgnoreCase<T>(key: string, dictionary: { [id: string]: T }, defaultVal: any = null): T {
  const keyFromDict = Object.keys(dictionary).find(x => equalsIgnoringCase(x, key));
  if (keyFromDict) {
    return dictionary[keyFromDict];
  } else {
    return defaultVal;
  }
}

export function getParamFromRoute(route: ActivatedRoute, paramName: string): Observable<any> {

  return route.paramMap.pipe(
    withLatestFrom(route.queryParamMap),
  ).pipe(
    map(([params, queryParams]) => {
      let result = params.get(paramName);
      if (!result) {
        result = queryParams.get(paramName)
      }
      return result;
    }),
  );
}

export function getAllParamsFromRoute(route: ActivatedRoute): Observable<{ [key: string]: string }> {
  return route.paramMap.pipe(
    withLatestFrom(route.queryParamMap),
  ).pipe(
    map(([params, queryParams]) => {
      const result = {};
      params.keys.forEach(x => result[x] = params.get(x));
      queryParams.keys.forEach(x => result[x] = queryParams.get(x));
      return result;
    }),
  );
}
