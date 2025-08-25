# Movie Management API

## Tech Stack
- .NET Core Web API
- Entity Framework Core
- SQL Server (or In-Memory DB)
- Swagger/OpenAPI for API documentation

## Features Implemented
- CRUD operations for Movie entity
- Validation for required fields and ranges
- Pagination for GET /movies (Bonus Point)
- CustomResult<T> for consistent API responses
- Repository-Service-Controller layered architecture
- Proper error handling (CustomResult + status codes)

## Features Not Implemented Yet (Though I have started learning them)
- Unit testing (in progress)
- Integration tests
- Dockerization
- Environment-based config (.env)

## How to Run
1. Clone the repo
2. Update connection string in `appsettings.json`
3. Run `dotnet ef database update` (if using EF migrations)
4. Start the API: `dotnet run`
5. Access Swagger: `https://localhost:7013/swagger/index.html` or `http://localhost:5268/swagger/index.html`

## How to Test
- Use Swagger UI or Postman to test endpoints
- Sample GET request: `/api/movies?pageNumber=1&pageSize=10`
