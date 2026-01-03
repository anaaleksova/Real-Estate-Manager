# Real Estate Manager

A comprehensive real estate management system built with ASP.NET Core 8.0 following clean architecture principles with integrated payment processing and email notifications.

## 🏗️ Architecture

The application follows a layered architecture pattern with clear separation of concerns:

- **RealEstate.Domain** - Core business entities and domain logic
- **RealEstate.Repository** - Data access layer with repository pattern implementation
- **RealEstate.Service** - Business logic and service layer
- **RealEstate.Web** - Presentation layer (ASP.NET Core MVC)

## 🚀 Key Features

### Property Management
- Create, read, update, and delete properties
- Property listing with detailed information
- Property status tracking (Available, Pending, Sold, Rented)
- Multi-agent assignment per property
- External property import from third-party APIs

### Agent Management
- Manage real estate agents
- Assign agents to properties
- Agent contact information

### Appointment System
- Schedule property viewings
- Appointment management for clients
- Agent assignment to appointments
- Automatic status updates (Scheduled, Completed, Cancelled)
- **Email notifications** for appointment confirmations

### User Features
- User registration and authentication (ASP.NET Core Identity)
- Favorite properties list
- Personal appointment dashboard
- Property purchase tracking

## 💳 Stripe Integration

The application uses Stripe for secure payment processing:
- Property purchase payments
- Secure checkout sessions
- Payment confirmation handling
- Transaction status tracking

Configure your Stripe keys in `appsettings.json` to enable payment functionality.

## 📧 SMTP Email Configuration

Email notifications are sent for:
- Property appointment confirmations

Configure your SMTP settings in `appsettings.json`:
- SMTP server address
- Port (default: 587)
- Username and password
- Sender email and display name

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Language**: C#
- **Frontend**: HTML, CSS, JavaScript, Bootstrap
- **Database**: SQL Server (Entity Framework Core)
- **Payment Gateway**: Stripe.NET (v48.4.0)
- **Email Service**: MailKit & MimeKit
- **Authentication**: ASP.NET Core Identity
- **Architecture**: Repository Pattern, Service Layer, MVC

## 📋 Prerequisites

- .NET SDK 8.0 or higher
- SQL Server
- Visual Studio 2022 or VS Code
- Stripe API keys (for payment processing)
- SMTP server credentials (for email functionality)

## 🔧 Installation

1. Clone the repository
```bash
git clone https://github.com/anaaleksova/Real-Estate-Manager.git
cd Real-Estate-Manager
```

2. Restore NuGet packages
```bash
dotnet restore
```

3. Configure `appsettings.json` in RealEstate.Web:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string"
  },
  "Stripe": {
    "PublishableKey": "your-stripe-publishable-key",
    "SecretKey": "your-stripe-secret-key"
  },
  "MailSettings": {
    "SmtpServer": "smtp.your-provider.com",
    "SmtpUserName": "your-smtp-username",
    "SmtpPassword": "your-smtp-password"
  }
}
```

4. Run database migrations
```bash
dotnet ef database update --project RealEstate.Repository
```

5. Build and run the application
```bash
dotnet build
dotnet run --project RealEstate.Web
```

## 🐳 Docker Support

The project includes Docker support for containerized deployment.
```bash
docker build -t real-estate-manager .
docker run -p 8080:80 real-estate-manager
```

## 🔑 Key Functionalities

1. **Property Lifecycle Management** - Complete CRUD operations for properties
2. **Multi-Agent System** - Assign multiple agents to properties
3. **Appointment Scheduling** - Book and manage property viewings
4. **Favorites System** - Save properties for later viewing
5. **Payment Processing** - Stripe-powered property purchases
6. **Email Notifications** - Automated SMTP-based communications
7. **User Authentication** - Secure registration and login
8. **External API Integration** - Import properties from external sources
