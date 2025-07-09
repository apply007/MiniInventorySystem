# 📦 Mini Inventory System – .NET Web API

A simple yet scalable Inventory Management API with JWT authentication, CRUD for Products & Customers, Sale module, Sales report with concurrency control.

---

## 🚀 Features

- ✅ Product CRUD with soft delete, pagination, filtering  
- ✅ Customer CRUD with validation  
- ✅ Sale transaction with:
  - Stock quantity deduction
  - Concurrency control (max 3 concurrent sales)
  - 3 seconds simulated processing delay
- ✅ Sales report by date range
- ✅ JWT-based login (Username: `admin`, Password: `admin123`)
- ✅ Swagger UI enabled

---

## 🔧 Technologies

- ASP.NET Core 8 Web API  
- Entity Framework Core  
- SQL Server  
- JWT Authentication  
- Swagger (OpenAPI)

---

## 🧑‍💻 Setup Instructions

### 1️⃣ Clone the project

```bash
git clone https://github.com/apply007/MiniInventorySystem.git
cd MiniInventorySystem
```

### 2️⃣ Open in Visual Studio

- Open `MiniInventorySystem.sln`

### 3️⃣ Database setup

- Make sure **SQL Server** is installed & running.
- Change connection string in `appsettings.json` if needed:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=MiniInventoryDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 4️⃣ Run database migration (via Package Manager Console)

```bash
Update-Database
```

Alternatively, run provided script (Optional):

### 👉 Or manually run SQL script:

Use the included script:
```
/Scripts/MiniInventoryDB.sql
```

It will:
- Create DB + Tables
- Insert dummy Products, Customers, Sales

---

### 5️⃣ Run the application

- Hit `F5` or click `Start` in Visual Studio  
- Swagger UI will open at:  
  `https://localhost:{port}/swagger`

---

## 🔐 Authentication

Use JWT to access protected endpoints.

**Login Credentials:**

```json
{
  "username": "admin",
  "password": "admin123"
}
```

POST to:

```
POST /api/auth/login
```

You’ll receive a JWT token to use in headers:

```
Authorization: Bearer {token}
```

---

## 📂 Folder Structure

| Folder            | Description                      |
|-------------------|----------------------------------|
| Controllers/      | API endpoints                    |
| Models/           | Entity models                    |
| Dto/              | Request/response models          |
| Interfaces/       | 
| Repositories/     | Repository layer (Product, Sale,Customer) |     
| Scripts/          | SQL scripts                      |

---

## 📦 Dependencies

Install these packages via NuGet:

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Swashbuckle.AspNetCore`


---


## ✅ Author

A H M Hasibul Hasan