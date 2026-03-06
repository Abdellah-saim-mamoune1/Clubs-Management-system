export interface IClub{
  id: number;
  name: string;
  type: string;
  typeId:number;
  description: string;
  eventsNumber: number;
  membersNumber: number;
  openForRegistrations:boolean;
  studentRole:string;
  joiningStatus:string;

}

export interface IMember {
  id: number;
  fullName: string;
  age: number;
  degree: string;
  yearOfDegree: string;
  isAdmin: boolean;
  joinedAt: string; 
}

export interface IEventMember {
  id: number;
  fullName: string;
}


export interface IClubType {
  id: number;
  type:string;
  clubsNumber:number;
}

export interface IEvent {
  id: number;
  title: string;
  content: string;
  date: string;         
  from: string;         
  to: string;          
  address: string;
  isPrivate: boolean;
  views: number;
  clubName:string;
  clubId:number;
  isStudentJoined: boolean;
  isStudentAdmin:boolean;
  isStudentMember:boolean;
  registrationInfo: IRegistrationInfo;
}
interface IRegistrationInfo{
  currentRegistrationsCount:number;
  maxRegistrationsCount:number;

}

export interface IClubJoiningRequest{
  StudentId:number;
  ClubId:number;
  Email:string;
  Motivation:string;
}

export interface IClubCreationRequest{
  studentId:number;
  clubTypeId:number;
  clubName:string;
  description:string;
  image:FormData|null;
}

export interface IClubState{
NewClubs:IClub[]|null|false;
MostActiveClubs:IClub[]|null|false;
LatestEvents:IEvent[]|null|false;
ClubsTypes:IClubType[]|null|false;

}