
export interface Login{
  account:string;
  password:string;
}

export interface GetPaginatedBooksParams {
  type: string;
  PageNumber: number;
  PageSize: number;
}


export interface IPagination{
 pageNumber:number
 pageSize:number
}
