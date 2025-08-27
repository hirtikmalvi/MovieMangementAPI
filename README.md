# Movie Management API

## Tech Stack
- .NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger for API documentation

## Features Implemented
- CRUD operations for Movie entity
- Validation for required fields and ranges
- Pagination for GET /movies (Bonus Point)
- CustomResult<T> for consistent API responses
- Repository-Service-Controller layered architecture
- Proper error handling (CustomResult + status codes)

## Features Not Implemented Yet (Though I have started learning them)
- Unit testing
- Integration tests
- Dockerization
- Environment-based config (.env)

## How to Run
1. Clone the repo
   1. git clone https://github.com/hirtikmalvi/MovieMangementAPI.git
   2. cd MovieMangementAPI
   3. Restore Dependencies `dotnet restore`
3. Update connection string in `appsettings.json`
  ```json
"ConnectionStrings": {
  "DbConnection": "Server=YOUR_DB_SERVER;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```
5. Run `dotnet ef database update`
6. Start the API: `dotnet run`
7. Access Swagger: `https://localhost:7013/swagger/index.html` or `http://localhost:5268/swagger/index.html`

## How to Test
- Use Swagger UI to test endpoints
- Sample GET request: `/api/movies?pageNumber=1&pageSize=10`
