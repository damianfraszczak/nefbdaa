export function removeByField<T>(field: string, value: any): (value: T) => boolean {
  return t => t[field] !== value;
}

export function removeCommonItemsByField<T>(field: string, values: any[]): (value: T) => boolean {
  return t => !values.some(x => x[field] === t[field]);
}

export function containsAny<T>(values: T[], toSearch: T[]): boolean {
  return toSearch.map(x => values.includes(x)).reduce((x, y) => x || y);
}

export function addIfNotExists<T>(value: T, values: T[]): boolean {
  if (containsAny(values, [value])) {
    return false;
  } else {
    values.push(value);
    return true;
  }
}

export function removeIfExists<T>(value: T, values: T[]): boolean {
  const index = values.indexOf(value);
  if (index > -1) {
    values.splice(index, 1);
    return true;
  }
  return false;

}
