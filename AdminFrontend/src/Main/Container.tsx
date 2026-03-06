
import { Route, Routes } from "react-router-dom";
import { AxiosInterceptor } from "../APIs/AxiosInterceptor";
import NotFound from "../Errors/NotFound";
import Unauthorized from "../Errors/Unathorized";
import Forbidden from "../Errors/Forbidden";
import ServerError from "../Errors/ServerError";
import NetworkError from "../Errors/NetworkError";
import GenericError from "../Errors/GenericError";
import EmployeeDashboardApp from "../EmployeePages/EmployeeDashboardApp";
import Login from "../EmployeePages/Login";



export function Container() {
    AxiosInterceptor();

    return (
    <div className="w-full h-full flex flex-col min-h-screen">
    
       <div className="flex-1 h-full overflow-y-auto ">
        <Routes>
       
         <Route path="/" element={<EmployeeDashboardApp />} />
         <Route path="/login" element={<Login />} />
     
         
          {/* Error pages */}
         <Route path="/404" element={<NotFound />} />
         <Route path="/401" element={<Unauthorized />} />
         <Route path="/403" element={<Forbidden />} />
         <Route path="/500" element={<ServerError />} />
         <Route path="/network-error" element={<NetworkError />} />
         <Route path="/error" element={<GenericError />} />

        {/* Catch-all for unmatched routes */}
         <Route path="*" element={<NotFound />} />
         
        </Routes>
      </div>

     
    </div>
  );
  
}
