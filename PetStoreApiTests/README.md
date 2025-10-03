# Petstore API Testing

This repository provides automated tests for the [Swagger Petstore API](https://petstore.swagger.io/), focusing on the `/user` endpoint. The tests are implemented in C# and use modern testing and HTTP client libraries.

## Features

- Automates REST API testing for the Petstore `/user` endpoints
- Covers multiple request types: **POST**, **PUT**, **GET**, **DELETE**
- Verifies status codes for various scenarios, including 2xx (Success), 4xx (Client Error), and more
- Asserts that data created via POST matches subsequent GET responses
- Implements retry policies for transient errors and 404 responses

## Structure

- `PetStoreApiTests/ApiRequests/UserRequests.cs` — Handles HTTP requests to the `/user` endpoints
- `PetStoreApiTests/Tests/UserTests.cs` — Contains test cases covering user creation, update, deletion, and error scenarios
- `PetStoreApiTests/Data/TestData.cs` — Generates random user data for testing
- `PetStoreApiTests/Polly/PollyPolicy.cs` — Defines retry policies for robust API calls
- `PetStoreApiTests/Model/` — DTOs for API payloads and responses

## Installation

1. **Clone the repository:**
   ```sh
   git clone https://github.com/maxbahr/petstore-api-testing.git
   cd petstore-api-testing
   ```

2. **Restore dependencies:**
   ```sh
   dotnet restore
   ```

## Usage

Run the tests using the .NET CLI:

```sh
dotnet test
```

The tests will:
- Create users (single and batch)
- Retrieve users and verify their details
- Update user information
- Delete users and handle error scenarios (e.g., not found, invalid input)
- Assert API status codes and response contents

## Dependencies

- [.NET 7+](https://dotnet.microsoft.com/)
- [RestSharp](https://restsharp.dev/) for making HTTP requests
- [FluentAssertions](https://fluentassertions.com/) for expressive assertions
- [Polly](https://github.com/App-vNext/Polly) for retry policies
- [Bogus](https://github.com/bchavez/Bogus) for test data generation
- xUnit for testing

## Contributing

Pull requests are welcome! Please open an issue to discuss your proposed changes.

## License

This project does not currently specify a license.

## Author

[Max Bahr](https://github.com/maxbahr)