# 🛒 Shop Management & Reporting System

[![.NET Version](https://img.shields.io/badge/.NET-10.0-blueviolet?style=for-the-badge&logo=.net)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF%20Core-10.0-blue?style=for-the-badge&logo=.net)](https://learn.microsoft.com/en-us/ef/core/)
[![Database](https://img.shields.io/badge/Database-SQL%20Server-red?style=for-the-badge&logo=microsoft-sql-server)](https://www.microsoft.com/en-us/sql-server)
[![UI Template](https://img.shields.io/badge/UI%20Template-Kaiadmin%20%2F%20Bootstrap%205-blue?style=for-the-badge&logo=bootstrap)](https://themekita.com/kaiadmin-free-bootstrap-admin-dashboard.html)

A professional, enterprise-grade Shop Management and Reporting System built on **.NET 10** using a clean, layered (N-Tier) architecture. This application enables store administrators to securely manage customers, products, and orders, monitor real-time stock levels, and generate rich analytical reports in PDF and Excel formats.

---

## 🛠️ Technology Stack

| Layer / Aspect | Technology / Library | Version | Description |
| :--- | :--- | :--- | :--- |
| **Core Framework** | ASP.NET Core MVC | `.NET 10.0` | High-performance, modern web application framework |
| **Database** | Microsoft SQL Server | LocalDB / Express | Enterprise relational database |
| **ORM / Data Access** | Entity Framework Core | `10.0.9` | Code-First relational database mapper |
| **PDF Generation** | QuestPDF | `2026.6.1` | Fluent-based engine for professional PDF exports (Community License) |
| **Excel Export** | EPPlus | `8.6.1` | Feature-rich spreadsheet generator (Non-commercial License) |
| **Security / Auth** | Cookie Authentication | Native ASP.NET Core | Secure, session-based cookie authentication & role checks |
| **Front-End Styling** | Bootstrap & Kaiadmin Theme | `5.x` | Modern, fully responsive admin dashboard layout |
| **Icons & Typography** | FontAwesome & Public Sans | `6.5.0` / Web Font | Rich iconography and clean typography |

---

## ✨ Key Features

- **🔐 Secure Authentication & Access Control**
  - Cookie-based authentication with distinct user roles (`Admin` and `User`).
  - Hashed user password storage using SHA-256 (`PasswordHelper`).
  - Strict authorization checks preventing standard users from accessing admin routes.

- **📊 Dynamic Administrative Dashboard**
  - Instant overview cards for crucial store KPIs: Total Customers, Products, Orders, and Total Sales.
  - Interactive table displaying the 5 most recent orders with live status.
  
  ![Admin Dashboard](https://github-production-user-asset-6210df.s3.amazonaws.com/placeholder-dashboard.png)
  *Instruction: Upload a screenshot of the main admin dashboard here*

- **📦 Inventory & Product Management**
  - Full CRUD lifecycle (Create, Read, Update, Delete) for products.
  - Critical stock alerts identifying products with stock level below safety thresholds (`CriticalStockLevel`).
  - Single-click PDF sheet export using QuestPDF and styled Excel spreadsheets using EPPlus.

  ![Product Index](https://github-production-user-asset-6210df.s3.amazonaws.com/placeholder-products.png)
  *Instruction: Upload a screenshot of the product catalog with export options here*

- **👥 Customer Relations**
  - Comprehensive customer registration details (First/Last Name, City, Age, Phone, Registration Date).
  - Clean client database list.

- **🛒 Order Tracking**
  - Structured ordering pipeline mapping customer demands to products.
  - Real-time unit price snapshots and quantity-based totals calculation.

- **📈 Advanced Business Reports**
  - Joined reporting queries analyzing sales data:
    - **Customer Order Report**: Full order history mapped to customer location and price.
    - **Order Product Report**: Sales distribution detailing item counts and product categories.
    - **Customer Product Report**: Direct linkage of customer demographic and purchased products.
    - **Customer Order Count**: High-level aggregation showing order frequencies per client.
    - **Critical Stock Alert Report**: Lists all under-stocked products requiring immediate replenishment.

  ![Reports View](https://github-production-user-asset-6210df.s3.amazonaws.com/placeholder-reports.png)
  *Instruction: Upload a screenshot of the reporting panel here*

---

## 🏗️ Architecture & Folder Structure

The repository is structured as a standard **N-Tier Layered Architecture** splitting domain logic, data access, and representation to achieve clean separation of concerns.

```text
Shop.UI/ (Solution Root)
├── Shop.UI.slnx                 # Solution configuration
├── Shop.UI/                     # Presentation Layer (MVC Web App)
│   ├── Controllers/             # MVC Controllers (Account, Admin, Customer, Product, Order, Report, Home)
│   ├── Models/                  # UI-specific Models
│   ├── Views/                   # Razor Views (cshtml layout, page designs)
│   │   ├── Account/
│   │   ├── Admin/
│   │   ├── Customer/
│   │   ├── Product/
│   │   ├── Order/
│   │   ├── Report/
│   │   ├── Shared/              # _Layout.cshtml (Kaiadmin Template), Error views, validation scripts
│   ├── wwwroot/                 # Static files (Bootstrap, Custom CSS, Kaiadmin assets, JS libraries)
│   ├── Program.cs               # Application entry point, dependency injection, and middleware configurations
│   ├── appsettings.json         # Configuration file (including DB Connection String)
│   └── Shop.UI.csproj           # Project configuration referencing EPPlus, QuestPDF, EF Core
├── Shop.Data/                   # Data Access Layer
│   ├── Data/
│   │   └── AppDbContext.cs      # EF Core DbContext, entity mapping, and database seed data
│   ├── Migrations/              # Entity Framework Core migrations
│   └── Shop.Data.csproj         # EF Core dependencies
├── Shop.Model/                  # Domain Model Layer
│   ├── Helpers/
│   │   └── PasswordHelper.cs    # Utility for SHA-256 password hashing & verification
│   ├── viewModel/               # Business-oriented view models (CustomerOrderVM, OrderProductVM, etc.)
│   ├── Customer.cs              # Customer model definition
│   ├── Order.cs                 # Order model definition
│   ├── Product.cs               # Product model definition
│   ├── User.cs                  # User model definition
│   └── Shop.Model.csproj        # Domain model layer configuration
└── README.md                    # Project documentation (this file)
```

### Layer Responsibilities
- **`Shop.Model` (Domain Model)**: Contains standard database models, view models (DTOs) for analytical screens, and core static tools (e.g., `PasswordHelper`).
- **`Shop.Data` (Data Layer)**: Controls database connection logic, Entity mappings (including setting precise decimal scale for currency attributes), seeds default accounts, and manages entity history migrations.
- **`Shop.UI` (Presentation Layer)**: ASP.NET Core MVC interface. Handles user requests, security cookies, and utilizes QuestPDF/EPPlus libraries to generate dynamic business files directly from the controller.

---

## ⚙️ Setup & Installation Guide

### 📋 Prerequisites
Ensure the following tools are installed on your machine:
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later.
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express, Developer, or LocalDB instances).
- Command-line interface or an IDE like Visual Studio 2022 / VS Code.

### 🔌 Database Configuration
1. Locate the configuration file under the presentation project: `Shop.UI/appsettings.json`.
2. Modify the connection string under `ConnectionStrings:Default` to match your local SQL Server instance:
   ```json
   "ConnectionStrings": {
     "Default": "Server=YOUR_SERVER_NAME;Database=ShopDb;TrustServerCertificate=true;Trusted_Connection=True"
   }
   ```

### 🚀 Local Execution
To configure and launch the system locally, execute the following commands in order from the terminal:

1. **Install EF Core Global CLI Tool (If not already installed)**:
   ```bash
   dotnet tool install --global dotnet-ef
   ```
   
2. **Apply Database Migrations & Seed Default Data**:
   From the root folder of the project, apply the migrations to generate the schema and seed accounts automatically:
   ```bash
   dotnet ef database update --project Shop.Data --startup-project Shop.UI
   ```
   
3. **Build the Solution**:
   ```bash
   dotnet build
   ```

4. **Run the MVC Project**:
   ```bash
   dotnet run --project Shop.UI
   ```

5. **Access the App**:
   Open your browser and navigate to:
   - HTTPS: `https://localhost:7010`
   - HTTP: `http://localhost:5282`

---

## 🔑 Default Credentials

The database is pre-seeded with two accounts for easy developer evaluation:

> 🔐 **Administrator Role**
> - **Email**: `admin@shop.com`
> - **Password**: `Admin123!`
> - **Role**: `Admin` *(Full access to dashboard, management modules, export utilities, and reports)*

> 👤 **Standard User Role**
> - **Email**: `user@shop.com`
> - **Password**: `User123!`
> - **Role**: `User` *(Accesses home user interface landing page)*

---

## 📄 License & Attribution

This project is licensed under the MIT License - see the LICENSE file for details.

### Third-Party Licensing Notes
- **QuestPDF**: Distributed under the *QuestPDF Community License* for non-commercial or small-company usage.
- **EPPlus**: Managed under the *EPPlus PolyForm Non-Commercial License* for private/evaluation purposes.
- **Kaiadmin**: Free Bootstrap 5 admin dashboard theme.
