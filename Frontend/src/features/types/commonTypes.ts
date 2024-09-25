export interface KKResult<T> {
  message: string | null;
  value: T | null;
  succeeded: boolean;
}

export enum OrderBy {
  ASC = "ASC",
  DESC = "DESC",
}

export interface QueryOptions<TOrderByEnum> {
  search?: string;
  pageNo: number;
  pageSize: number;
  orderWith: TOrderByEnum;
  orderBy: OrderBy;
}

export interface PaginatedResult<T> {
  pageNo: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
  results: T[];
}
