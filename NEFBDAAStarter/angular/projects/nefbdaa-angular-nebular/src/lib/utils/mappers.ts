import {OperatorFunction} from 'rxjs';
import {map} from 'rxjs/operators';
import {isNumeric} from 'rxjs/internal-compatibility';
import {constants} from '../shared-constants';

export function mapArray<T, R>(mapper: (t: T) => R): (values: T[]) => R[] {
  return ts => ts.map(mapper);
}

export function filterArray<T>(predicate: (value: T) => boolean): (values: T[]) => T[] {
  return ts => ts.filter(predicate);
}

export function findFirstArray<T>(predicate: (value: T) => boolean): (values: T[]) => T {
  const result = filterArray(predicate);
  return result.length > 0 ? result[0] : null;
}

export function extractContent<T>(): OperatorFunction<{ content: T }, T> {
  return map(response => response.content);
}


export function extractContentList<T>(): OperatorFunction<{ content: { list: T } }, T> {
  return map(response => response.content.list);
}


export function getDefaultId(id: any): number {
  if (isNumeric(id)) {
    return Number(id);
  } else {
    return constants.defaultID;
  }
}
