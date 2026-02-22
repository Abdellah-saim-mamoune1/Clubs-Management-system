import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5271/api", // your .NET backend URL
  withCredentials: true, // since you’re using cookies/JWT
});

// Export the same instance for all your APIs
export default api;
