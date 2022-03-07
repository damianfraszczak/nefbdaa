export interface Order {
  isAscending?: boolean;
  property?: string;
}

export interface Page {
  index?: number;
  size?: number;
}

export class PagedDataParams {
  order?: Order;
  page?: Page;


  pageSize?: number;
  pageIndex?: number;
  orderProperty?: string;
  orderIsAscending?: boolean;

}
