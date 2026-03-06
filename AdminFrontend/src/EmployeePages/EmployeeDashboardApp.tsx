import { useEffect, useState } from "react";
import {
  GetStatsAsync,
  GetClubsCreationRequestsAsync,
  AcceptClubsCreationRequestsAsync,
  DeleteClubsCreationRequestsAsync,
  GetEmployeeInfoAPI,
  IsLoggedInAsync,
  GetStudentsAsync
} from "../APIs/EmployeeAPIs";

import type {
  IEmployeeInfo,
  IStatistics,
  ClubsRequestGetDto,
  IStudent
} from "../Interfaces/EmployeeInterfaces";

import { motion } from "framer-motion";
import { useNavigate } from "react-router-dom";
import { NotificationCard } from "../Components/NotificationCard";

export default function EmployeeDashboardApp() {

  const [activePage, setActivePage] = useState<"dashboard" | "students" | "account">(
    "dashboard"
  );

  const navigate = useNavigate();

  const [employee, setEmployee] = useState<IEmployeeInfo | null>(null);
  const [stats, setStats] = useState<IStatistics | null>(null);
  const [requests, setRequests] = useState<ClubsRequestGetDto[]>([]);
  const [students, setStudents] = useState<IStudent[]>([]);

  const [loading, setLoading] = useState<boolean>(true);
  const [studentsLoading, setStudentsLoading] = useState<boolean>(false);

  const [actionLoadingId, setActionLoadingId] = useState<number | null>(null);
 
  const [notif, setNotif] = useState({
    show: false,
    message: "",
    isSuccess: true
  });

  useEffect(() =>{
    
    checkIsLoggedIn();
    loadDashboard();
    loadEmployeeInfo();
  }, []);

  useEffect(() => {
    if (activePage === "students") {
      loadStudents();
    }
  }, [activePage]);

  const checkIsLoggedIn = async () => {
    var res=await IsLoggedInAsync();
  
    if (!res) {
      navigate("/login");
    }
  };

  const loadEmployeeInfo = async () => {
    setEmployee(await GetEmployeeInfoAPI());
  };

  const loadDashboard = async () => {
    setLoading(true);

    const statsRes = await GetStatsAsync();
    const reqRes = await GetClubsCreationRequestsAsync();

    if (statsRes) setStats(statsRes);
    if (reqRes) setRequests(reqRes);

    setLoading(false);
  };

  const loadStudents = async () => {
    setStudentsLoading(true);

    try {
      const res = await GetStudentsAsync();
      if (res) setStudents(res);
    } finally {
      setStudentsLoading(false);
    }
  };

  const handleAccept = async (id: number) => {
    setActionLoadingId(id);

   await AcceptClubsCreationRequestsAsync(id);

   
      setNotif({ show: true, message: "Request accepted!", isSuccess: true });
      loadDashboard();
   

    setActionLoadingId(null);
  };

  const handleDelete = async (id: number) => {
    setActionLoadingId(id);

    await DeleteClubsCreationRequestsAsync(id);

    
      setNotif({ show: true, message: "Request deleted!", isSuccess: true });
      loadDashboard();
   

    setActionLoadingId(null);
  };

  
  return (
    <div className="flex min-h-screen w-full bg-gray-100">

      <NotificationCard
        show={notif.show}
        message={notif.message}
        isSuccess={notif.isSuccess}
        onClose={() => setNotif({ ...notif, show: false })}
      />

      {/* Sidebar */}

      <aside className="w-64 bg-black text-white flex flex-col p-6 space-y-8">

        <h1 className="text-2xl font-bold tracking-wide">
          EMPLOYEE
        </h1>

        <nav className="flex flex-col space-y-3">

          <button
            onClick={() => setActivePage("dashboard")}
            className={`text-left px-4 py-3 rounded-2xl ${
              activePage === "dashboard"
                ? "bg-gray-800"
                : "hover:bg-gray-900"
            }`}
          >
            Dashboard
          </button>

          <button
            onClick={() => setActivePage("students")}
            className={`text-left px-4 py-3 rounded-2xl ${
              activePage === "students"
                ? "bg-gray-800"
                : "hover:bg-gray-900"
            }`}
          >
            Students
          </button>

          <button
            onClick={() => setActivePage("account")}
            className={`text-left px-4 py-3 rounded-2xl ${
              activePage === "account"
                ? "bg-gray-800"
                : "hover:bg-gray-900"
            }`}
          >
            Account
          </button>

        </nav>
      </aside>

      {/* Main */}

      <main className="flex-1 p-10">

        {/* DASHBOARD */}

        {activePage === "dashboard" && (
          <motion.div
            initial={{opacity:0,y:20}}
            animate={{opacity:1,y:0}}
            className="space-y-10"
          >

            <h2 className="text-3xl font-bold">
              Dashboard
            </h2>

            {loading && <p>Loading dashboard...</p>}

            {stats && (
              <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
                <StatCard title="Students" value={stats.studentsCount}/>
                <StatCard title="Clubs" value={stats.clubsCount}/>
                <StatCard title="Events" value={stats.eventsCount}/>
                <StatCard title="New Clubs" value={stats.lastMonthNewClubs}/>
              </div>
            )}

            {/* Requests */}

            <div className="space-y-6">

              <h3 className="text-2xl font-semibold">
                Clubs Creation Requests
              </h3>

              {requests.map(req => (

                <div
                  key={req.requestId}
                  className="bg-white rounded-2xl shadow-md p-6 flex justify-between"
                >

                  <div>
                    <h4 className="text-xl font-semibold">
                      {req.clubName}
                    </h4>

                    <img
                      src={`http://localhost:8102/api/employee/club-request/${req.requestId}/image`}
                      className="mt-3 w-full h-40 object-cover rounded-lg border"
                    />

                    <p>Student ID: {req.studentId}</p>
                    <p>Created At: {req.createdAt}</p>
                  </div>

                  <div className="flex gap-3">

                    <button
                      onClick={()=>handleAccept(req.requestId)}
                      className="px-5 max-h-16 py-2 bg-green-600 text-white rounded-2xl"
                    >
                      {actionLoadingId===req.requestId
                        ? <Spinner/>
                        : "Accept"}
                    </button>

                    <button
                      onClick={()=>handleDelete(req.requestId)}
                      className="px-5 py-2 max-h-16 bg-red-600 text-white rounded-2xl"
                    >
                      {actionLoadingId===req.requestId
                        ? <Spinner/>
                        : "Delete"}
                    </button>

                  </div>

                </div>
              ))}

            </div>

          </motion.div>
        )}

        {/* STUDENTS PAGE */}

        {activePage === "students" && (

          <motion.div
            initial={{opacity:0,y:20}}
            animate={{opacity:1,y:0}}
            className="space-y-6"
          >

            <h2 className="text-3xl font-bold">
              Students
            </h2>

            {studentsLoading && <Spinner/>}

            {!studentsLoading && (

              <div className="bg-white rounded-2xl shadow overflow-hidden">

                <table className="w-full">

                  <thead className="bg-gray-100 text-left">

                    <tr>
                      <th className="p-4">ID</th>
                      <th className="p-4">Full Name</th>
                      <th className="p-4">Age</th>
                      <th className="p-4">Degree</th>
                    </tr>

                  </thead>

                  <tbody>

                    {students.map(student => (

                      <tr
                        key={student.id}
                        className="border-t hover:bg-gray-50"
                      >

                        <td className="p-4">
                          {student.id}
                        </td>

                        <td className="p-4">
                          {student.fullName}
                        </td>

                        <td className="p-4">
                          {student.age}
                        </td>

                        <td className="p-4">
                          {student.degree}
                        </td>

                      </tr>

                    ))}

                  </tbody>

                </table>

              </div>

            )}

          </motion.div>

        )}

        {/* ACCOUNT */}

        {activePage === "account" && employee && (

          <div className="bg-white rounded-2xl shadow-lg p-8 max-w-xl">

            <h2 className="text-3xl font-bold mb-6">
              Account
            </h2>

            <p>Name: {employee.name}</p>
            <p>Account: {employee.account}</p>

          </div>

        )}

      </main>
    </div>
  );
}

function Spinner(){
  return (
    <span className="w-5 h-5 border-2 border-white border-t-transparent rounded-full animate-spin inline-block"/>
  )
}

function StatCard({title,value}:{title:string,value:number}){

  return (
    <div className="bg-white rounded-2xl shadow-md p-6">
      <p className="text-gray-500 text-sm">{title}</p>
      <p className="text-3xl font-bold mt-2">{value}</p>
    </div>
  )
}