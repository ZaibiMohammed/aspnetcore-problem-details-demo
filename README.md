# ASP.NET Core Problem Details Demo

This project demonstrates the implementation of RFC 7807 Problem Details for HTTP APIs in ASP.NET Core. It showcases best practices for error handling and provides consistent error responses across your API.

## Features

- Implements RFC 7807 Problem Details
- Custom exception handling middleware
- Domain-specific exceptions
- Environment-specific error details
- Trace ID support for error tracking
- Swagger/OpenAPI documentation

## Project Structure

```
src/
  └── ProblemDetails.Api/
      ├── Controllers/
      │   └── ProductsController.cs
      ├── Exceptions/
      │   ├── DomainException.cs
      │   └── ResourceNotFoundException.cs
      ├── Middleware/
      │   └── ExceptionHandlingMiddleware.cs
      ├── Models/
      │   └── Product.cs
      └── Program.cs
```

## Getting Started

1. Clone the repository
2. Navigate to the project directory
3. Run the project:
   ```bash
   dotnet run --project src/ProblemDetails.Api
   ```
4. Access Swagger UI at `https://localhost:7001/swagger`

## Example Usage

### Get Non-existent Product

```http
GET /api/products/999
```

Response:
```json
{
  "type": "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
  "title": "Resource Not Found",
  "status": 404,
  "detail": "Resource 'Product' with id '999' was not found.",
  "instance": "/api/products/999",
  "traceId": "00-1234567890abcdef-abcdef1234567890-00",
  "errorCode": "resource_not_found"
}
```

### Create Invalid Product

```http
POST /api/products
Content-Type: application/json

{
  "name": "Invalid Product",
  "price": -10,
  "stock": 100
}
```

Response:
```json
{
  "type": "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
  "title": "Bad Request",
  "status": 400,
  "detail": "Product price must be greater than zero",
  "instance": "/api/products",
  "traceId": "00-1234567890abcdef-abcdef1234567890-00",
  "errorCode": "invalid_price"
}
```

## License

This project is licensed under the MIT License.