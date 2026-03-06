
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
  description:string;
  createdAt: string; 
}
