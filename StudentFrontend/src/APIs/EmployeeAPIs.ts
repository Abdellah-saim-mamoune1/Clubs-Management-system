import { createAsyncThunk } from "@reduxjs/toolkit";
import api from "./API";


export const GetClientInfoAPI= createAsyncThunk(
  'employee/get-employee-info',
  async () => {
    
  
      const response=await api.get(`employee/by-id`,{
       headers: {
       Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  },
    });
  
    return response.data.data;

  }
);


export async function GetStatsAsync(){

      const response=await api.get(`employee/statistics`,{ withCredentials:true});
       return response.data.data;

}


export async function GetClubsCreationRequestsAsync(){

  
      const response=await api.get(`employee/clubs/requests/`,{ withCredentials:true});
       return response.data.data;
 
}



export async function AcceptClubsCreationRequestsAsync(RequestId:number){

 
      const response=await api.post(`employee/clubs/requests/${RequestId}`,{ withCredentials:true});
      return response.data.data;
 
}




export async function DeleteClubsCreationRequestsAsync(RequestId:number){

      const response=await api.delete(`employee/clubs/requests/${RequestId}`,{ withCredentials:true});
      return response.data.data;

}