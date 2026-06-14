# Finance Tracker

A desktop personal/business finance manager built with **.NET 10 Windows Forms** and **SQL Server**. It lets you manage **Accounts**, **Categories**, and **Transactions**, and visualize your money on a chart-driven **Dashboard**.

> Advanced Programming Semester Project | SP26

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Solution Architecture](#solution-architecture)
- [Project Structure](#project-structure)
- [Domain Model](#domain-model)
- [Data Layer (Services)](#data-layer-services)
- [User Interface](#user-interface)
- [Database](#database)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [How It Works End-to-End](#how-it-works-end-to-end)
- [Design Decisions & Conventions](#design-decisions--conventions)
- [Troubleshooting](#troubleshooting)

---

## Overview

Finance Tracker is a classic three-tier desktop application:

1. **Presentation** — Windows Forms UI (sidebar navigation, data grids, dialog forms, charts).
2. **Business/Data Access** — a set of services that wrap ADO.NET and talk to SQL Server.
3. **Database** — a SQL Server database (`FinanceTrackerDB`) with three tables.

The core idea: you record **transactions** (income/expense) that belong to an **account** (where the money lives) and a **category** (what the money is for). The dashboard then aggregates this data into charts.

---

## Features

- **Accounts management** — create Cash, Bank, or Credit Card accounts with opening balances and currency (default `PKR`).
- **Categories management** — Income/Expense buckets with an optional monthly budget.
- **Transactions management** — full add/edit/view/delete, with amount, date, description, status (Cleared/Pending), and recurring schedules (Daily/Weekly/Monthly/Yearly).
- **Filtering & search** — the Transactions view supports filtering by free-text description, account, category, date range, and status.
- **Dashboard with charts**:
  - Pie chart — total transaction amount per category (all-time).
  - Column chart — opening balance per account.
- **Single-window, tabbed navigation** — a sidebar switches between Dashboard, Accounts, Categories, and Transactions.
- **Live dashboard refresh** — editing data in any view automatically refreshes the dashboard if it has been opened.
- **Safe SQL** — all queries are parameterized; `LIKE` searches escape wildcards.

---

## Tech Stack

| Layer        | Technology                                              |
|--------------|---------------------------------------------------------|
| Language     | C# (nullable reference types enabled, implicit usings)  |
| Runtime      | .NET 10 (`net10.0` core, `net10.0-windows` for the UI)  |
| UI           | Windows Forms                                           |
| Charts       | [LiveChartsCore.SkiaSharpView.WinForms](https://livecharts.dev/) 2.0.4 |
| Data access  | ADO.NET via `Microsoft.Data.SqlClient` 7.0.1            |
| Database     | Microsoft SQL Server / LocalDB                          |
| Config       | `System.Configuration.ConfigurationManager` (App.config) |

---

## Solution Architecture

The solution (`FinanceTracker.slnx`) contains **two projects**:

```
FinanceTracker.slnx
├── App.Core            (class library, net10.0)        ← models, enums, services
└── App.WindowsForm     (WinExe, net10.0-windows)       ← UI, references App.Core
```

`App.WindowsForm` depends on `App.Core`. `App.Core` has **no UI dependency** — it only knows about models and SQL Server. This keeps the data layer reusable and testable in isolation.

The application follows **dependency inversion**: the UI talks to services through interfaces (`IAccountService`, `ICategoryService`, `ITransactionService`) and never references the concrete `Db*Service` classes except at a single composition point (`MainForm.CreateDefaultServices`).

```
┌─────────────────────────────────────────────────────┐
│  App.WindowsForm (Presentation)                       │
│                                                       │
│  MainForm ──► Views (Dashboard/Accounts/...)          │
│      │              │                                 │
│      │              └──► Forms (Add/Edit/View dialogs)│
│      ▼                                                │
│  IAccountService / ICategoryService / ITransaction... │   ← interfaces (App.Core)
└─────────┬─────────────────────────────────────────────┘
          │
          ▼
┌─────────────────────────────────────────────────────┐
│  App.Core (Data Access)                               │
│  DbAccountService / DbCategoryService / DbTransaction │   ← ADO.NET implementations
└─────────┬─────────────────────────────────────────────┘
          │  Microsoft.Data.SqlClient
          ▼
┌─────────────────────────────────────────────────────┐
│  SQL Server — FinanceTrackerDB                        │
│  Accounts · Categories · Transactions                 │
└─────────────────────────────────────────────────────┘
```

---

## Project Structure

```
FinanceTracker/
├── FinanceTracker.slnx              # Solution file
├── README.md                        # This file
│
├── App.Core/                        # Data + domain layer (no UI)
│   ├── App.Core.csproj
│   ├── Enums/
│   │   ├── AccountStatusEnum.cs      # Active, Inactive
│   │   ├── AccountTypeEnum.cs        # Cash, Bank, CreditCard
│   │   ├── CategoryStatusEnum.cs     # Active, Inactive
│   │   ├── CategoryTypeEnum.cs       # Income, Expense
│   │   ├── RecurringFrequencyEnum.cs # Daily, Weekly, Monthly, Yearly
│   │   └── TransactionStatusEnum.cs  # Cleared, Pending
│   ├── Models/
│   │   ├── Account.cs
│   │   ├── Category.cs
│   │   └── Transaction.cs
│   └── Services/
│       ├── IAccountService.cs        # Contracts...
│       ├── ICategoryService.cs
│       ├── ITransactionService.cs
│       ├── DbAccountService.cs       # ADO.NET implementations
│       ├── DbCategoryService.cs
│       └── DbTransactionService.cs
│
├── App.WindowsForm/                 # Presentation layer
│   ├── App.WindowsApp.csproj
│   ├── App.config                   # Connection string lives here
│   ├── Program.cs                   # Entry point — runs MainForm
│   ├── MainForm.cs / .Designer.cs   # Shell: sidebar + content panel
│   ├── Assets/                      # Sidebar icons (png)
│   ├── Properties/                  # Resources
│   ├── Views/                       # Full-page UserControls
│   │   ├── DashboardView.cs         # Pie + column charts
│   │   ├── AccountsView.cs          # Accounts grid + toolbar
│   │   ├── CategoriesView.cs        # Categories grid + toolbar
│   │   └── TransactionsView.cs      # Transactions grid + filters
│   └── Forms/                       # Modal dialogs
│       ├── AccountForm.cs           # Add/Edit/View account
│       ├── CategoryForm.cs          # Add/Edit/View category
│       ├── TransactionForm.cs       # Add/Edit/View transaction
│       └── *FormMode.cs             # Add | Edit | View enums
│
└── Databases/
    ├── SQLQuery1.sql                # Schema + seed data (run this first)
    └── SQLQuery2.sql                # Migration: widen Id columns to NVARCHAR(50)
```

> Note: `Designer.cs` and `.resx` files are auto-generated by the WinForms designer and describe control layout. The hand-written logic lives in the corresponding `.cs` files.

---

## Domain Model

All three models share a convention: the `Id` is a string generated by the **service** during `Add()`, not by the form. It defaults to an empty string so a model can be `new()`-ed without an initializer.

### Account — *where money lives*

| Property         | Type                | Notes                                  |
|------------------|---------------------|----------------------------------------|
| `Id`             | `string`            | Generated as `A-<guid>`                |
| `Name`           | `string` (required) | e.g. "Cash Wallet", "HBL Current"      |
| `Email`          | `string?`           | Optional                               |
| `AccountType`    | `AccountTypeEnum`   | Cash / Bank / CreditCard               |
| `OpeningBalance` | `decimal`           | Starting balance                       |
| `Currency`       | `string`            | Defaults to `"PKR"`                    |
| `AccountStatus`  | `AccountStatusEnum` | Active (default) / Inactive            |
| `CreatedAt`      | `DateTime`          | Set to `UtcNow` on insert              |

### Category — *income/expense buckets*

| Property         | Type                 | Notes                          |
|------------------|----------------------|--------------------------------|
| `Id`             | `string`             | Service-generated              |
| `Name`           | `string` (required)  | e.g. "Office Rent"             |
| `CategoryType`   | `CategoryTypeEnum`   | Income / Expense               |
| `MonthlyBudget`  | `decimal?`           | Optional; `NULL` = no budget   |
| `CategoryStatus` | `CategoryStatusEnum` | Active (default) / Inactive    |

### Transaction — *each money event*

| Property             | Type                       | Notes                                        |
|----------------------|----------------------------|----------------------------------------------|
| `Id`                 | `string`                   | Generated as `T-<18 guid chars>`             |
| `AccountId`          | `string` (required)        | References an Account (no FK constraint)     |
| `CategoryId`         | `string` (required)        | References a Category (no FK constraint)     |
| `Amount`             | `decimal`                  | Must be > 0 (enforced in the form)           |
| `TransactionDate`    | `DateTime`                 | Stored as `DATE`                             |
| `Description`        | `string?`                  | Optional                                     |
| `IsRecurring`        | `bool`                     | Defaults to `false`                          |
| `RecurringFrequency` | `RecurringFrequencyEnum?`  | Required only when `IsRecurring` is true     |
| `TransactionStatus`  | `TransactionStatusEnum`    | Cleared (default) / Pending                  |

> **Enums are persisted as their string name** (e.g. `"Cleared"`), not as integers — and parsed back with `Enum.Parse<T>` when reading. This keeps the database human-readable.

---

## Data Layer (Services)

Each entity has an interface and a `Db*Service` implementation. All three implementations follow the **same template** (documented at the top of `DbAccountService.cs`):

1. The constructor receives the **connection string** (constructor injection).
2. Every method opens a `SqlConnection` in a `using` block, runs SQL, and closes it.
3. All user values flow through `SqlParameter` — **never string concatenation** (prevents SQL injection).
4. A private `MapRow` helper builds a model object from a `SqlDataReader` row (DRY).
5. Enums are stored as `ToString()` names and parsed back with `Enum.Parse<T>`.
6. Ids are generated in C# from a short prefix + part of a GUID.

### Common service contract

Each service exposes:

```csharp
List<T> GetAll();
T?      GetById(string id);
T       Add(T entity);          // generates Id, inserts, returns the saved entity
bool    Update(T entity);       // true only if a row with that Id existed
bool    Delete(string id);      // true only if a row was actually removed
List<T> Search(...);            // filtered query (filters vary per entity)
```

### Notable implementation details

- **Unique-Id retry loop** — `Add()` generates an Id and inserts; if SQL Server reports a duplicate-key error (codes `2627`/`2601`), it retries up to **5 times** with a fresh Id before throwing. In practice a GUID collision never happens, but the guard is there.
- **Nullable columns** — `Category.MonthlyBudget`, `Transaction.Description`, and `Transaction.RecurringFrequency` use the `DBNull.Value` dance on write and a `== DBNull.Value` check on read.
- **Dynamic search with `WHERE 1=1`** — `DbTransactionService.Search` supports six optional filters. It starts with `WHERE 1=1` so each optional clause can simply append `" AND ..."` without worrying about whether it's the first condition.
- **`LIKE` wildcard escaping** — free-text searches escape `\`, `%`, and `_` and use `ESCAPE '\'` so a user typing `50%` searches for the literal text, not a wildcard.

#### Transaction search signature

```csharp
List<Transaction> Search(
    string text,                 // matches Description (LIKE)
    string accountId,            // exact match, "" = any
    string categoryId,           // exact match, "" = any
    DateTime? fromDate,          // >=
    DateTime? toDate,            // <=
    TransactionStatusEnum? status);
```

---

## User Interface

### `MainForm` — the application shell

`MainForm` is the single top-level window. It contains a **sidebar** of navigation buttons (Dashboard, Accounts, Categories, Transactions) and a **content panel** that hosts the active view.

Key behaviors:

- **Composition root** — `CreateDefaultServices()` reads the connection string from `App.config` and constructs the three `Db*Service` instances. This is the *only* place concrete service types are named. A second constructor accepts services directly for **dependency injection / testing**.
- **Cache-aside views** — `ShowView<T>(factory)` creates each view **once**, caches it in a dictionary keyed by type, and reuses it on later clicks. This preserves view state (filters, scroll, selection) across navigation.
- **Active-tab styling** — `SelectTab` highlights the current button (blue background, bold font, left border) and resets the others.
- **Cross-view refresh** — after any view mutates data, it calls `(ParentForm as MainForm)?.RefreshDashboardIfCached()`, which re-runs the dashboard charts *if* the dashboard has been opened.
- **Resource cleanup** — `OnFormClosed` disposes all cached views and cached fonts to avoid leaks.

### Views (`Views/`)

Views are `UserControl`s docked to fill the content panel. The three CRUD views (`AccountsView`, `CategoriesView`, `TransactionsView`) share a pattern:

- A `DataGridView` bound to a `BindingSource`.
- A toolbar with **Add / Edit / View / Delete / Refresh** buttons.
- Lazy first load (`_loaded` flag fires `RefreshGrid()` on the first `Load`).
- `SelectedX` reads `bindingSource1.Current` to get the highlighted row.
- Each action wraps service calls in `try/catch` and reports errors with a `MessageBox`.

`TransactionsView` additionally has a **filter bar** (search box, account/category/status dropdowns, from/to date pickers) wired to **Apply** and **Clear** buttons that call `Search` / reset to `GetAll`.

`DashboardView` builds two LiveCharts series from the database:

- `BuildAmountByCategoryPie()` — groups all transactions by `CategoryId` once (O(n)), joins to categories, drops zero-total categories, and renders a pie. 
- `BuildOpeningBalanceByAccountColumn()` — one column per account's opening balance.
- `RefreshCharts()` is **public** so `MainForm` can refresh it after external mutations.

### Forms (`Forms/`)

Each entity has one **mode-driven dialog** that handles **Add / Edit / View** via a `*FormMode` enum — instead of three separate forms. For example, `TransactionForm`:

- Takes the mode, an optional input entity, and any services needed to populate dropdowns (accounts/categories).
- `ConfigureForMode()` adjusts the title, the Save button label ("Save" vs "Update"), and — in **View** mode — hides Save and disables every input (read-only).
- `PopulateFields()` fills controls from the input entity (Edit/View).
- `ValidateData()` enforces rules (e.g. account & category required, amount > 0, frequency required when recurring) before saving.
- On save it populates a public `Result` property and sets `DialogResult.OK`. The calling **view** reads `Result` and passes it to the service (`Add`/`Update`). In Edit mode the original entity is reused so its `Id` is preserved.

---

## Database

The database is **`FinanceTrackerDB`** with three tables. There are **no foreign-key constraints** between Transactions and Accounts/Categories — this is intentional per the project spec; referential integrity is handled in application code via dropdown selection.

### Schema (`Databases/SQLQuery1.sql`)

| Table          | Key columns                                                                 |
|----------------|------------------------------------------------------------------------------|
| `Accounts`     | `Id` PK, `Name`, `AccountType`, `OpeningBalance`, `Currency`, `Status`, `CreatedAt` |
| `Categories`   | `Id` PK, `Name`, `CategoryType`, `MonthlyBudget` (nullable), `Status`        |
| `Transactions` | `Id` PK, `AccountId`, `CategoryId`, `Amount`, `TransactionDate`, `Description` (nullable), `IsRecurring`, `RecurringFrequency` (nullable), `Status` |

`SQLQuery1.sql` is **idempotent**: it creates the database if missing, drops and recreates the tables, and inserts **seed data** (3 accounts, 6 categories, 5 transactions) so there's something to look at on first run.

### Migration (`Databases/SQLQuery2.sql`)

Widens the `Id`, `AccountId`, and `CategoryId` columns from `NVARCHAR(20)` to `NVARCHAR(50)`. Run this after `SQLQuery1.sql` if you hit Id-length limits (account Ids use a full GUID and exceed 20 characters).

---

## Getting Started

### Prerequisites

- **Windows** (Windows Forms is Windows-only).
- **.NET 10 SDK**.
- **SQL Server** — LocalDB, SQL Server Express, or a full instance reachable at `localhost`.
- Optional: **Visual Studio 2022+** (with the .NET desktop workload) for the form designer.

### 1. Create the database

Open `Databases/SQLQuery1.sql` in SQL Server Management Studio or the Visual Studio SQL editor and execute it against your server. Then run `Databases/SQLQuery2.sql` to widen the Id columns.

> The script comments walk through doing this from **SQL Server Object Explorer** in Visual Studio.

### 2. Configure the connection string

Edit `App.WindowsForm/App.config` if your server isn't the default `localhost` with Windows auth (see [Configuration](#configuration)).

### 3. Build and run

From the repository root:

```powershell
dotnet build FinanceTracker.slnx
dotnet run --project App.WindowsForm/App.WindowsApp.csproj
```

Or open `FinanceTracker.slnx` in Visual Studio, set **App.WindowsApp** as the startup project, and press **F5**.

---

## Configuration

The connection string lives in **one place** — `App.WindowsForm/App.config`:

```xml
<connectionStrings>
  <add name="FinanceTrackerDB"
       connectionString="Server=localhost;Database=FinanceTrackerDB;Trusted_Connection=true;TrustServerCertificate=true;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Adjust `Server=` to match your instance. Common alternatives:

| Setup                  | `Server=` value                       |
|------------------------|---------------------------------------|
| Local default instance | `localhost`                           |
| SQL Express            | `localhost\SQLEXPRESS`                |
| LocalDB                | `(localdb)\MSSQLLocalDB`              |

`MainForm.CreateDefaultServices()` reads this entry at startup and throws a clear `ConfigurationErrorsException` if it's missing or blank — so a misconfigured connection string fails fast with an explanatory message.

---

## How It Works End-to-End

A typical "add a transaction" flow ties all the layers together:

1. User clicks **Transactions** in the sidebar → `MainForm` shows the cached `TransactionsView`.
2. User clicks **Add** → `TransactionsView` opens `TransactionForm` in **Add** mode, passing the account & category services so the dropdowns can populate.
3. User fills the form and clicks **Save** → `TransactionForm.ValidateData()` runs; on success it builds `Result` and returns `DialogResult.OK`.
4. `TransactionsView` reads `f.Result` and calls `_transactionService.Add(result)`.
5. `DbTransactionService.Add()` generates a `T-...` Id, opens a connection, runs a parameterized `INSERT`, and returns the saved transaction.
6. The view calls `RefreshGrid()` to reload the data grid, shows a success `MessageBox`, and calls `RefreshDashboardIfCached()` so the dashboard charts update if they're already open.

---

## Design Decisions & Conventions

- **Interface-first / dependency inversion.** Views and `MainForm` depend on `IAccountService` etc., never on `Db*Service`. Concrete types are wired up in exactly one factory method, making the data layer swappable and testable.
- **Service-generated Ids.** Models default `Id` to `""`; the service assigns the real Id on insert. Forms never invent Ids.
- **Enums stored as text.** Human-readable in the DB, type-safe in C#.
- **Parameterized SQL everywhere.** No string concatenation of user input; `LIKE` searches escape wildcards.
- **Mode-driven forms.** One dialog per entity covers Add/Edit/View, reducing duplication.
- **Cache-aside views.** Views are built once and reused, preserving UI state and avoiding rebuild cost.
- **Fail-fast configuration.** A missing connection string throws immediately with guidance.

---

## Troubleshooting

| Symptom | Likely cause / fix |
|---------|--------------------|
| App throws `ConfigurationErrorsException` on startup | The `FinanceTrackerDB` connection string in `App.config` is missing or empty. |
| `Cannot open database "FinanceTrackerDB"` | You haven't run `Databases/SQLQuery1.sql` yet, or `Server=` points to the wrong instance. |
| Login / SSL errors connecting to SQL | Check `Trusted_Connection` and `TrustServerCertificate` in the connection string; ensure your Windows account has DB access. |
| Inserting an account fails with a length/truncation error | Run `Databases/SQLQuery2.sql` to widen the `Id` columns to `NVARCHAR(50)`. |
| Sidebar icons don't appear | The `Assets/*.png` files must be copied to the output directory — the `.csproj` does this with `CopyToOutputDirectory=PreserveNewest`; rebuild if needed. |
| Build fails — designer types not found | Build the whole solution so `App.Core` is compiled before `App.WindowsForm`. |

---

*Built for the Advanced Programming course (SP26) as a Windows Forms + SQL Server case study in layered architecture, ADO.NET, and clean service design.*
