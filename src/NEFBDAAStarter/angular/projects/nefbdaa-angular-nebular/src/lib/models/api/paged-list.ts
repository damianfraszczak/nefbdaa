export interface PagedList<T> {
  count?: number;
  list?: Array<T>;
  index?: number;
  size?: number;
}
