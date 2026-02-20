This is a temporal README for a technical test purpose

Instructions:

This project is a comprehensive client management solution built with a decoupled microservices architecture using .NET 8, Blazor WebAssembly, and SQL Server on Docker.

The solution is divided into three main projects following the Separation of Concerns (SoC) principle:

Izumu.Api: A RESTful microservice handling business logic and data persistence.

Izumu.FrontEnd: A Single Page Application (SPA) built with Blazor WebAssembly.

Izumu.Shared: A common class library containing data models and DTOs shared between the client and the server.

Izumu.Tests: A unit testing project using xUnit and Moq to ensure business logic reliability.

Prerequisites:

.NET 8 SDK

Docker Desktop

EF Core Tools (dotnet tool install --global dotnet-ef)

Database:

The application uses SQL Server 2022 running inside a Docker container.

1. Spin up the container
In the root directory, run the following command (ensure your docker-compose.yml is present):

Bash
docker-compose up -d
Default Configuration:

Server: localhost,1433

User: sa

Password: IzumuPassword123!

2. Table Creation and Data Seeding
Using Entity Framework (Recommended):
From the Izumu.Api folder, run the migrations to create the IzumuDB database and seed master data (Document Types and Plans):

Bash
dotnet ef database update

Web App:

1. Restore Dependencies
From the solution root:

Bash
dotnet restore

2. Run the Backend (API)

Bash
cd Izumu.Api
dotnet run
API URL: http://localhost:5026

Swagger Documentation: http://localhost:5026/swagger

3. Run the Frontend (Blazor)
Open a new terminal:

Bash
cd Izumu.FrontEnd
dotnet run
Web App URL: http://localhost:5106

Note: Make sure that those ports are available.

Tests:

The Izumu.Tests project validates the integrity of the microservice. It uses InMemory Database to isolate tests from the production environment and Moq to simulate service behaviors in controllers.

Test Coverage:
ClientServiceTests: Validates business rules, such as preventing duplicate identity documents and ensuring correct CRUD operations in the database.

ClientesControllerTests: Ensures the API returns correct HTTP status codes (e.g., 400 BadRequest on duplicates, 404 NotFound for missing records).

To run the tests:

Bash
cd Izumu.Tests
dotnet test