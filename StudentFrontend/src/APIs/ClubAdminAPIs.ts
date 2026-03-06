
import {  IEventSet, IEventUpdate } from "../Interfaces/ClubAdminInterfaces";
import api from "./API";



export async function ClubEventAddAPI(UserId:number,form:IEventSet){
 
      await api.post(`club/admin/event/${UserId}`,form,{
         withCredentials:true,
         headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});        
    
}

export async function ClubEventUpdateAPI(UserId:number,form:IEventUpdate){
  
      await api.put(`club/admin/event/${UserId}`,form,{
         withCredentials:true,
         headers: {
         Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});       
    
}

export async function ClubEventDeleteAPI(UserId:number,EventId:number){
 
      await api.delete(`club/admin/event/${UserId},${EventId}`,{
         withCredentials:true,
         headers: {
       Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});    
}

export async function ClubUpdateAPI(
  UserId: number,
  form: FormData
) {
  await api.put(`club/admin/${UserId}`, form, {
    headers: {
      Authorization: `Bearer ${localStorage.getItem("authToken")}`,
      "Content-Type": "multipart/form-data",
    },
  });
}


export async function GetApplicationsRequestsAPI(ClubId:number,PageNumber:number,PageSize:number){
 
      const response=await api.get(`club/admin/candidates/${ClubId},${PageNumber},${PageSize}`,{
         withCredentials:true,
         headers: {
       Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});    
      return response.data.data;

}

export async function AcceptApplicationAPI(ApplicationId:number,UserId:number,ClubId:number){
 
      await api.post(`club/admin/candidate/accept/${ApplicationId},${UserId},${ClubId}`,{
         withCredentials:true,
         headers: {
       Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});       
      return true;

}


export async function RefuseApplicationAPI(applicationId:number){
 
      await api.post(`club/admin/candidate/refuse/${applicationId}`,{
         withCredentials:true,
         headers: {
       Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});      
      return true;
}

export async function SetMemberAsAdminAPI(UserId:number,ClubId:number){
 
      await api.put(`club/admin/member/set-as-admin/${UserId},${ClubId}`,{
         withCredentials:true,
         headers: {
       Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});    
}

export async function RemoveMemberAPI(UserId:number,ClubId:number){
  
      await api.delete(`club/admin/member/${UserId},${ClubId}`,{
         withCredentials:true,
         headers: {
       Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});    
}