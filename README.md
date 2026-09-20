# Stocks App

A .NET 6 ASP.NET Core MVC stock trading application that lets users explore popular stocks, view company details and live quotes, place buy/sell orders, and review order history. The project follows a layered architecture with web, core, infrastructure, and test projects.

## Features

- Browse stock listings and popular symbols
- View stock details and current price information from Finnhub
- Place buy and sell orders
- Review trade history for buy and sell orders
- Export order summaries to PDF
- Structured logging with Serilog
- Unit and integration tests covering controller and service behavior

## Tech Stack

- ASP.NET Core MVC (.NET 6)
- Entity Framework Core
- SQL Server / LocalDB
- Finnhub API integration
- Rotativa for PDF generation
- Serilog for logging
- xUnit + FluentAssertions + Moq

## Project Structure

- `Stocks.Web` – ASP.NET Core MVC application
- `Stocks.Core` – domain logic, DTOs, service contracts, entities, validation helpers
- `Stocks.Infrastructure` – repositories and EF Core data access
- `Stocks.Controllers.Tests` – controller-level tests
- `Stocks.Services.Tests` – service-layer tests
- `Stocks.Integration.Tests` – integration tests for web endpoints

## Prerequisites

Before running the app, make sure you have:

- .NET 6 SDK installed
- SQL Server LocalDB available on your machine
- Access to a Finnhub API token (configured in app settings)

## Configuration

The app configuration is stored in `Stocks.Web/appsettings.json`.

Key settings include:

- `ConnectionStrings:DefaultConnection` – SQL Server connection string
- `TradingOptions:DefaultOrderQuantity` – default order quantity
- `TradingOptions:Top25PopularStocks` – list of popular stock symbols
- `FinnhubToken` – token used for stock/company profile calls
- `Serilog` – logging configuration

## Running the Application

From the solution root, run:

```bash
dotnet restore

dotnet build

dotnet run --project Stocks.Web
```

Then open the app in a browser at:

```text
https://localhost:5001
```

If you are running in a local development environment, the app may use different URLs depending on your launch profile.

## Database Setup

The application uses Entity Framework Core and SQL Server LocalDB. Ensure the `DefaultConnection` database is available and the database schema can be created or migrated as needed.

## Running Tests

Run the full test suite from the solution root:

```bash
dotnet test
```

You can also run individual projects if needed:

```bash
dotnet test Stocks.Services.Tests

dotnet test Stocks.Controllers.Tests

dotnet test Stocks.Integration.Tests
```

## Application Flow

1. The home/explore page displays a list of stocks.
2. Users select a stock symbol to view its trade page.
3. The app fetches company and quote information from Finnhub.
4. Users submit buy or sell orders.
5. Orders are persisted via the repository layer and shown in the order history.
6. Order history can be exported as a PDF.

## Notes

- The app is designed as a sample trading dashboard and uses Finnhub for external stock data.
- The `appsettings.json` file contains a real-looking Finnhub token in this project and should be updated for production use.
- PDF export relies on Rotativa and may require the corresponding local PDF rendering dependencies to be available in the environment.

## License

This project is intended for learning and demonstration purposes.
