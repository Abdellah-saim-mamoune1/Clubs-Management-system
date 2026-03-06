import { createAsyncThunk } from "@reduxjs/toolkit";
import api from "./API";
import axios from "axios";


export async function GetStudentDataByIdAsync(id:number){

  try{
      const response=await api.get(`user/by-id/${id}`,{ withCredentials:true});
       return response.data.data;
  }catch{
    return false;
  }
}

export async function UpdateStudentImageAPIAsync(image: File) {
  try {
    const formData = new FormData();
    formData.append("image", image);

    await api.put("user/image/", formData, {
      withCredentials: true,
      headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`,
        "Content-Type": "multipart/form-data",
      },
    });

    return true;
  } catch {
    return false;
  }
}

export const GetClientInfoAPI= createAsyncThunk(
  'Client/GetClientInfo',
  async () => {
    
    try{
      const response=await api.get(`user`,{
       headers: {
       Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  },
    });
  
    return response.data.data;

  } catch (error: any) {
     
    return null;
  }
  
}
);

export async function IsStudentLoggedInAPI(){
    
    try{
     await axios.post(`http://localhost:8102/api/authentication/is-logged-in`,{},{
      withCredentials:true,
      headers: {
       Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  },
    });
     
    return true;

  } catch (error: any) {
  
   return false;
  }
}
