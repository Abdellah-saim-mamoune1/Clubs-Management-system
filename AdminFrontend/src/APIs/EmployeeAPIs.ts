
import api from "./API";
import axios from "axios";


export async function GetEmployeeInfoAPI(){
 
  
const response=await api.get(`employee/by-id`,{
       withCredentials:true,
   headers: {
   Authorization: `Bearer ${localStorage.getItem("authToken")}`
  }
}
);
  return response.data.data;

}


export async function LoginAsync(account:string,password:string){
 try{
    const response = await axios.post("http://localhost:8102/api/employee/login", {
        account: account,
        password: password,
      });

      const token = response.data.data;
      localStorage.setItem("authToken", token);
}catch{
      return false;
}
}


export async function IsLoggedInAsync(){
 try{
   await api.get("employee/is-logged-in",{
       withCredentials:true,
   headers: {
   Authorization: `Bearer ${localStorage.getItem("authToken")}`
  }
});
console.log("true");
return true;
 }catch{
      console.log("false");
  return false;
 }

}



export async function GetStudentsAsync(){
 
 var data=await api.get("employee/studets",{
       withCredentials:true,
   headers: {
   Authorization: `Bearer ${localStorage.getItem("authToken")}`
  }
});
  console.log(data.data.data);
  return data.data.data;

}

export async function GetStatsAsync(){

      const response=await api.get(`employee/statistics`,{
         withCredentials:true,
         headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});
       return response.data.data;

}


export async function GetClubsCreationRequestsAsync(){

  
      const response=await api.get(`employee/clubs/requests/`,{
         withCredentials:true,
         headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});
       return response.data.data;
 
}



export async function AcceptClubsCreationRequestsAsync(RequestId:number){

 
      const response=await api.post(`employee/clubs/requests/${RequestId}`,{},{
         withCredentials:true,
         headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});
      return response.data.data;
 
}




export async function DeleteClubsCreationRequestsAsync(RequestId:number){

      const response=await api.delete(`employee/clubs/requests/${RequestId}`,{
         withCredentials:true,
         headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`,
  }});
      return response.data.data;

}