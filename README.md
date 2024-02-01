# Usage

- `git clone https://github.com/mihaicaragheorghe/tasks-backend.git`
- `dotnet run --project src/Tasks.Api .`

## API Definition

### Documentation

- [Swagger documentation](https://app.swaggerhub.com/apis-docs/MIHAICARAGHEORGHE96/tasks-api/v1)
- Use swagger to execute endpoints by running the project and accessing <https://localhost:your-port/swagger>

### Error handling

The API follows a consistent error response format with accurate HTTP status codes. In case of errors, you can expect the following format:

``` js
404 Not Found
```

```json
{
    "errors": [
        {
            "code": "User.NotFound",
            "description": "The user could not be found"
        }
    ]
}
```
