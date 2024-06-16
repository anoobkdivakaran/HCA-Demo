Here's the updated README with the provided project details:

---

# HCA-Demo: Book Management Application

This application is for managing books using CRUD operations.

## Project Structure

The application consists of two projects under one solution:

1. **HCA MVC Core 3.1**: UI project.
2. **API**: Web API project using MVC Core 3.1.

## Database Setup

- **Database Name**: Book_DB
- **Connection String**: 
  ```
  Server=(localdb)\mssqllocaldb;Database=Book_DB;Trusted_Connection=True;MultipleActiveResultSets=true
  ```
  (This is located in the `appsettings.json` file.)

## Prerequisites

Before running the application, ensure you have the following:

- **.NET Core 3.1 SDK** installed.
- **Entity Framework Core (EF Core)** installed.

## Setup Steps

Before running the application, you need to perform the following steps:

1. **Create Migration**:
   ```
   dotnet ef migrations add InitialCreate
   ```
   This command creates a migration named `InitialCreate`.

2. **Update Database**:
   ```
   dotnet ef database update
   ```
   This command applies the migration to create your database and schema.

By following these steps, you'll set up the necessary database structure for your application.

## Project Details

- **Pages**: 
  - Create page
  - Edit page
  - Details page
  - Index page as dashboard.
- **Model**: 
  - A model for the book, including validation messages.
