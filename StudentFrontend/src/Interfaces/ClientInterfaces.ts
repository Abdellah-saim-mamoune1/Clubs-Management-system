

export interface IClient{
    IsLoogedIn:boolean
    ClientInfo:IGetClientInfo|null
    
}

export interface IContactUs{
userName:string;
account:string;
message:string;
}

interface IAccount{
  account:string;
  password:string
}
export interface ISignUp{
  firstName:string;
  lastName:string;
  phoneNumber:string;
  account_informations:IAccount;
}

export interface IRestPassword{
 oldPassword:string;
 newPassword:string;

}


export interface IGetClientInfo{
  id:number;
  fullName:string;
  degree:string;
  age:number;
  yearOfDegree:string;
  joinedClubs:IJoinedClub[];
}

interface IJoinedClub{
  id:number;
  name:string;
  userRole:string;
  type:string;
}

export interface IAddOrder{
  shipmentAddress:string;
  totalQuantity:number;
  totalPrice:number;
  cartItemsIds:number[];
}
export interface IUpdateClientInfo{
 firstName:string;
 lastName:string;
 phoneNumber:string;
}


export interface DPagination {
  pageNumber: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
}


