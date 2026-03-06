# Clubs Management System

A **full-stack web application** for managing university or organizational clubs.

The system provides **two separate interfaces**:
- **Student Interface** → for students to browse clubs and events
- **Admin Interface** → for administrators to manage clubs, events, and members

The entire system runs using **Docker Compose**, which starts the backend, database, and both frontend applications automatically.

---

# Screenshots

![Student Dashboard](./StudentFrontend/assets/screenshot-2763.png)
![Student Dashboard](./Frontend/assets/screenshot-2764.png)
![Student Dashboard](./StudentFrontend/assets/screenshot-2765.png)
![Student Dashboard](./StudentFrontend/assets/screenshot-2766.png)
![Student Dashboard](./StudentFrontend/assets/screenshot-2767.png)
![Student Dashboard](./StudentFrontend/assets/screenshot-2768.png)
![Student Dashboard](./StudentFrontend/assets/screenshot-2769.png)

## Admin Dashboard

![Admin Dashboard](./AdminFrontend/assets/dash1.png)
![Admin Dashboard](./AdminFrontend/assets/dash2.png)
![Admin Dashboard](./AdminFrontend/assets/dash3.png)

---

# Tech Stack

## Frontend
- React
- TypeScript
- Tailwind CSS

Two frontend applications:
- **StudentFrontend** → Student portal
- **AdminFrontend** → Admin dashboard

## Backend
- ASP.NET Core 9 Web API
- Entity Framework Core
- JWT Authentication

## Database
- SQL Server

## Infrastructure
- Docker
- Docker Compose

---

# Project Structure

```
Clubs_Management
│
├── Backend/                # ASP.NET Core Web API
│
├── StudentFrontend/        # React app for students
│
├── AdminFrontend/          # React app for administrators
│
├── docker-compose.yml      # Runs the entire system
│
└── README.md
```

---

# Getting Started

##  Clone the Repository

```bash
git clone https://github.com/Abdellah-saim-mamoune1/Clubs_Management
cd Clubs_Management
```

---

# Run the Project with Docker

Make sure you have installed:

- Docker
- Docker Compose

Then run:

```bash
docker compose up --build
```

Docker will automatically start:

- Backend API
- SQL Server database
- Student frontend
- Admin frontend

---

# Application URLs

After the containers start, you can access the system:

| Service | URL |
|-------|------|
| Student Frontend | http://localhost:3002 |
| Admin Frontend | http://localhost:3003 |
| Backend API | http://localhost:8102 |

*(Ports may vary depending on your docker-compose configuration.)*

---

# 🛠 Running Without Docker (Optional)

## Backend

```bash
cd Backend
dotnet restore
dotnet run
```

## Student Frontend

```bash
cd StudentFrontend
npm install
npm run dev
```

## Admin Frontend

```bash
cd AdminFrontend
npm install
npm run dev
```

---

# Configuration

Update the backend configuration in:

```
Backend/appsettings.json
```

Example:

```json
{
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "clubs-app",
    "Audience": "clubs-app",
    "AccessTokenExpirationMinutes": 10,
    "RefreshTokenExpirationDays": 7
  },
  "ConnectionStrings": {
    "DefaultConnection": "your-db-connection-string"
  }
}
```

---

#  Contact

If you have any questions or suggestions, feel free to reach out:

**abdellahsaimmamoune1@gmail.com**
