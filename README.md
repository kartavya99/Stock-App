# Stock Trading Platform

An ASP.NET Core MVC application for tracking stocks and placing simulated buy and sell orders, with server-rendered Razor views, built on Clean Architecture with SQL Server persistence.

## Features

- **Live pricing** — real-time price updates and charting for a selected stock
- **Search** — look up stocks by symbol or company name
- **Trading** — place buy and sell orders
- **Dashboard** — order history split by buy and sell orders
- **Authentication** — registration and sign-in via ASP.NET Core Identity

## Architecture

Structured with Clean Architecture — domain, application, infrastructure and presentation layers kept separate so business logic doesn't depend on data access or framework concerns. Entity Framework Core handles persistence
against SQL Server, and unit tests use Moq to isolate services from their dependencies.

## Stack

- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- Clean Architecture
- Moq
- xUnit

## Running locally
