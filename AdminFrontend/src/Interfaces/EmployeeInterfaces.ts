
export interface IEmployee{
 employee:IEmployeeInfo|null;
 IsLoogedIn:boolean
}



export interface IEmployeeInfo{
  id:number;
  name:string;
  account:string;
}



export interface IStatistics{
  studentsCount:number;
  clubsCount:number;
  eventsCount:number;
  lastMonthNewClubs:number;
}


export interface ClubsRequestGetDto {
  studentId: number;
  clubTypeId: number;
  requestId:number;
  clubName: string;
  imageUrl: string;
  description:string;
  createdAt: string; 
}


export interface ILogin {
 
  account: string; 
  password: string;
  
}


export interface IStudent {

id:number;
fullName:string;
age:number;
degree:string;  

}

