export function byPropertyAsc(property: string) {
  return (x, y) => x[property] > y[property] ? 1 : x[property] < y[property] ? -1 : 0;
}

export function byPropertyDesc(property: string) {
  return (x, y) => x[property] > y[property] ? -1 : x[property] < y[property] ? 1 : 0;
}
