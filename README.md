I understand. Here is the complete content for a **`README.md`** file, based on the information you provided earlier. You can copy and paste this directly into a new file named `README.md` in your project's root directory.

-----

# 🎬 Movie Management API

This is a **`.NET Core Web API`** for managing movie data, built with a focus on clean architecture and testability.

## ⚙️ Tech Stack

  * **`.NET Core Web API`**: The core framework for the API.
  * **`Entity Framework Core`**: An `ORM` for data access.
  * **`SQL Server`**: The database used.
  * **`Swagger (OpenAPI)`**: For API documentation and testing.
  * **`xUnit & Moq`**: For unit testing.

## ✨ Features

### ✅ Implemented Features

  * **`CRUD Operations`**: Full **C**reate, **R**ead, **U**pdate, **D**elete functionality for the `Movie` entity.
  * **`Data Validation`**: Ensures required fields and value ranges are valid.
  * **`Pagination`**: `GET /movies` supports pagination for efficient data retrieval.
  * **`Consistent API Responses`**: Uses `CustomResult<T>` for standardized success and error responses.
  * **`Layered Architecture`**: Follows a **Repository-Service-Controller** pattern for separation of concerns.
  * **`Error Handling`**: Proper error handling with custom results and appropriate **HTTP** status codes.
  * **`Unit Testing`**: The service layer is thoroughly tested using **`xUnit`** and **`Moq`**.

### 📝 Planned Features

  * **`Integration Tests`**: To test the complete flow of the application.
  * **`Dockerization`**: To containerize the application for easier deployment.
  * **`Environment-based Configuration`**: Using `.env` files for managing configurations.

-----

## 📂 Project Structure

```
MovieManagementAPI/
│
├── MovieManagementAPI/           # Main Web API project
│   ├── Controllers/              # API Endpoints
│   ├── Services/                 # Business Logic
│   ├── Repositories/             # Data Access Layer
│   ├── Models/                   # Database Entities
│   ├── DTOs/                     # Data Transfer Objects
│   ├── Utilities/                # Helper classes (e.g., CustomResult)
│   └── Data/                     # EF Core DbContext
│
├── MovieManagementAPI.Tests/     # Unit Test Project
│   ├── Services/                 # Unit tests for the Services layer
│   └── (optional)
│
└── README.md
```

-----

## ▶️ How to Run

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/hirtikmalvi/MovieMangementAPI.git
    cd MovieMangementAPI/MovieManagementAPI
    ```
2.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```
3.  **Update the connection string** in `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DbConnection": "Server=YOUR_DB_SERVER;Database=MovieDB;Trusted_Connection=True;TrustServerCertificate=True"
    }
    ```
4.  **Apply EF Core migrations:**
    ```bash
    dotnet ef database update
    ```
5.  **Run the API:**
    ```bash
    dotnet run
    ```
6.  **Access the Swagger UI** to test the endpoints:
      * `https://localhost:7013/swagger/index.html`
      * `http://localhost:5268/swagger/index.html`

-----

## 🧪 How to Run Tests

1.  **Navigate to the test project directory:**
    ```bash
    cd MovieManagementAPI/MovieManagementAPI.Tests
    ```
2.  **Run the tests:**
    ```bash
    dotnet test
    ```

Tests follow the **`AAA`** Pattern (**Arrange**-**Act**-**Assert**). Examples covered include movie creation (success & failure), retrieval by ID, and deletion.

-----

## 🌐 Sample Requests

### Get all movies with pagination

```http
GET /api/movies?pageNumber=1&pageSize=10
```

### Create a new movie

```http
POST /api/movies
Content-Type: application/json

{
  "title": "Inception",
  "director": "Christopher Nolan",
  "releaseYear": 2010,
  "genre": "Sci-Fi",
  "rating": 9
}
```
