# 🌿 Plant CRUD Application

A full-stack CRUD web application for managing plants.  
Built using **ASP.NET Core Web API (Server)** and **ASP.NET Core MVC (Client)** inside a single blank solution.

---

## 🧾 Project Description

This solution is divided into two projects:

- **Server:** ASP.NET Core Web API for handling all CRUD operations with EF Core and LocalDB.
- **Client:** ASP.NET Core MVC app (Model-View-Controller) to interact with the API using forms and views.

🟢 Both projects run together automatically.  
No need for any additional setup like SDK install, SQL Server configuration, or separate terminal commands.

---

## ▶️ Steps to Run

1. **Clone the repository:**

   ```bash
   git clone https://github.com/your-username/PlantCrudSolution.git
   ```

2. **Open the solution in Visual Studio:**

   ```
   PlantCrudSolution.sln
   ```

3. **Set startup projects:**

   - Right-click on the solution → **Set Startup Projects...**
   - Select **Multiple startup projects**
   - Set both `Server` and `Client` → **Action: Start**

4. **Open the Package Manager Console (PMC):**

   - Select `Server` as the **Default Project**
   - Run the following commands:

   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

   > 📝 If a database already exists with same name, change the name in `appsettings.json` before running.

5. **Now just press `F5` or click "Run" — the Web API and MVC client will both launch together.**

---

## 🛠️ Technologies Used

- ASP.NET Core Web API
- ASP.NET Core MVC
- Entity Framework Core
- Bootstrap 5
- SQL Server LocalDB (no external SQL Server needed)

---

### 🔗 Navigation Bar Includes:

- 🌿 Brand: "My Plant"
- Links:
  - Home
  - Plant List
  - Create Plant
- Responsive toggle (hamburger menu)

---

## ✅ Core Features

- List all plants
- Create a new plant
- Edit plant details
- Delete a plant
- View plant information
- Responsive layout with Bootstrap

---

## 🧑‍💻 Author

**Md.Rifat Hossain**  
GitHub: [https://github.com/rifatsCreations](https://github.com/rifatsCreations)  
Email: mrifat621@gmail.com

---

## 📝 License

This project is for learning and educational purposes only.
