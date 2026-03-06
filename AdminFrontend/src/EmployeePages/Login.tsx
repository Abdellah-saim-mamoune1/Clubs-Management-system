import React, { useState } from "react";
import { useNavigate } from "react-router-dom"; 
import axios from "axios";


export async function LoginAsync(account: string, password: string) {
  try {
    const response = await axios.post("http://localhost:8102/api/employee/login", {
      account,
      password,
    });
    const token = response.data.data;
    localStorage.setItem("authToken", token);
    return true;
  } catch (err) {
    return false;
  }
}

interface LoginFormData {
  account: string;
  password: string;
}

const Login: React.FC = () => {
  const [form, setForm] = useState<LoginFormData>({ account: "", password: "" });
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string>("");

  const navigate = useNavigate(); 

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const validate = () => {
    if (!form.account || !form.password) {
      setError("All fields are required.");
      return false;
    }
    setError("");
    return true;
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!validate()) return;

    setLoading(true);
    const success = await LoginAsync(form.account, form.password);

    if (success) {
      setLoading(false);
      navigate("/");
    } else {
      setLoading(false);
      setError("Login failed. Check your credentials.");
    }
  };

  return (
    <div style={styles.container}>
      <form style={styles.card} onSubmit={handleSubmit}>
        <h2 style={styles.title}>Welcome Back</h2>

        {error && <p style={styles.error}>{error}</p>}

        <div style={styles.inputGroup}>
          <label style={styles.label}>Account</label>
          <input
            type="text"
            name="account"
            value={form.account}
            onChange={handleChange}
            style={styles.input}
            placeholder="Enter your account"
          />
        </div>

        <div style={styles.inputGroup}>
          <label style={styles.label}>Password</label>
          <input
            type="password"
            name="password"
            value={form.password}
            onChange={handleChange}
            style={styles.input}
            placeholder="Enter your password"
          />
        </div>

        <button type="submit" style={styles.button} disabled={loading}>
          {loading ? (
            <div style={styles.spinner}></div> // Circular loading
          ) : (
            "Sign In"
          )}
        </button>
      </form>
    </div>
  );
};

export default Login;

const styles: { [key: string]: React.CSSProperties } = {
  container: {
    height: "100vh",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#000", // Black background
  },
  card: {
    backgroundColor: "#fff", // White card
    padding: "2.5rem",
    borderRadius: "12px",
    width: "360px",
    boxShadow: "0 8px 20px rgba(0,0,0,0.3)",
    display: "flex",
    flexDirection: "column",
  },
  title: {
    textAlign: "center",
    marginBottom: "2rem",
    color: "#000",
    fontWeight: 600,
  },
  inputGroup: {
    marginBottom: "1.2rem",
    display: "flex",
    flexDirection: "column",
  },
  label: {
    marginBottom: "0.5rem",
    fontSize: "0.9rem",
    color: "#000",
  },
  input: {
    padding: "0.75rem",
    borderRadius: "6px",
    border: "1px solid #000",
    backgroundColor: "#fff",
    color: "#000",
    outline: "none",
  },
  button: {
    padding: "0.8rem",
    borderRadius: "8px",
    border: "none",
    backgroundColor: "#000",
    color: "#fff",
    fontWeight: 600,
    cursor: "pointer",
    marginTop: "1.5rem",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    minHeight: "42px",
  },
  spinner: {
    border: "3px solid #fff",
    borderTop: "3px solid #000",
    borderRadius: "50%",
    width: "20px",
    height: "20px",
    animation: "spin 1s linear infinite",
  },
  error: {
    color: "red",
    marginBottom: "1rem",
    textAlign: "center",
  },
};

const styleSheet = document.styleSheets[0];
styleSheet.insertRule(`
@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}`, styleSheet.cssRules.length);