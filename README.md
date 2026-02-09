# ResilentAPI

A resilient .NET 8 Web API integrated with Azure Application Insights for monitoring and telemetry. This project demonstrates cloud-native API development with comprehensive testing, automated CI/CD pipelines, and production-ready health monitoring endpoints.

## 🎯 Overview

ResilentAPI is a lightweight Web API built with .NET 8 that provides health check, logging, and error simulation endpoints. It's designed to showcase Azure DevOps integration, Application Insights telemetry, and best practices for building observable and resilient APIs.

## ✨ Features

- **Health Monitoring**: Real-time health status endpoint with version and environment information
- **Centralized Logging**: Structured logging endpoint for application event tracking
- **Error Simulation**: Testing endpoint for validating error handling and monitoring alerts
- **Azure Integration**: Built-in Application Insights for comprehensive telemetry
- **API Documentation**: Interactive Swagger/OpenAPI documentation
- **Automated Testing**: Unit tests with code coverage reporting
- **CI/CD Pipeline**: Fully automated build, test, and deployment workflow

## 🏗️ Architecture

### Project Structure

```
ResilentAPI/
├── ResilentAPI/              # Main API project
│   ├── Controllers/          # API controllers
│   │   └── HealthController.cs
│   ├── Models/               # DTOs and models
│   │   └── LogRequest.cs
│   ├── Program.cs            # Application entry point
│   └── appsettings.json      # Configuration
├── ResilentTest/             # Unit tests
│   └── HealthControllerTests.cs
└── azure-pipelines.yml       # CI/CD pipeline
```

### Technology Stack

- **Framework**: .NET 8
- **API Documentation**: Swagger/Swashbuckle (v6.6.2)
- **Monitoring**: Azure Application Insights (v3.0.0)
- **Testing**: xUnit, Moq
- **CI/CD**: Azure DevOps Pipelines
- **Hosting**: Azure App Service (Linux)

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An IDE (Visual Studio 2022, JetBrains Rider, or VS Code)
- Azure subscription (optional, for Application Insights)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/DarianR0410/ResilentAPI.git
   cd ResilentAPI
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure Application Insights** (Optional)
   
   Update `appsettings.json` with your Application Insights connection string:
   ```json
   {
     "ApplicationInsights": {
       "ConnectionString": "your-connection-string-here"
     }
   }
   ```

4. **Build the solution**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run --project ResilentAPI
   ```

6. **Access Swagger UI**
   
   Navigate to: `https://localhost:7032/swagger` or `http://localhost:5146/swagger`

## 📡 API Endpoints

### Health Check
Returns the application's health status, version, and environment.

**Endpoint**: `GET /api/health`

**Response**:
```json
{
  "status": "healthy",
  "version": "1.0.0",
  "environment": "Production"
}
```

### Log Message
Logs an informational message to Application Insights.

**Endpoint**: `POST /api/log`

**Request Body**:
```json
{
  "message": "Your log message here",
  "level": "Information"
}
```

**Response**:
```json
{
  "success": true
}
```

### Error Simulation
Simulates an unhandled error for testing monitoring and alerting.

**Endpoint**: `GET /api/error`

**Response**: `500 Internal Server Error`
```json
{
  "error": true,
  "message": "Unhandled exception, testing ex for apps insights",
  "timestamp": "2024-02-09T10:30:00.000Z"
}
```

## 🧪 Testing

### Run Unit Tests

```bash
dotnet test
```

### Run with Code Coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Test Coverage

The test suite includes:
- Controller action return type validation
- Response payload verification
- Logging behavior verification
- Parameterized tests for different scenarios

**Current Test Coverage**: Tests cover the HealthController with comprehensive scenarios including valid requests, response validation, and logging verification.

## 🔄 CI/CD Pipeline

The project includes a complete Azure DevOps pipeline configuration with two stages:

### Build Stage
- Restores NuGet packages
- Builds the solution in Release configuration
- Runs unit tests with code coverage
- Publishes test results and coverage reports
- Creates deployment artifacts

### Deploy Stage
- Triggered only on `master` branch
- Deploys to Azure App Service (Linux)
- Requires successful build completion

### Pipeline Configuration

**Triggers**: `master`, `dev`, `add-yml` branches

**Agent Pool**: Self-hosted pool

**Azure Resources**:
- **Subscription**: Azure-Production
- **App Service**: WebResilentAPI-1

### Running the Pipeline

The pipeline automatically triggers on commits to configured branches. Manual triggers can be initiated from Azure DevOps.

## 🌐 Deployment

### Azure App Service Deployment

The application is configured for deployment to Azure App Service using:
- **Platform**: Linux
- **Runtime**: .NET 8
- **Deployment Method**: Azure DevOps Pipeline

### Configuration

Ensure the following variables are configured in your Azure DevOps pipeline:
- `azureSubscription`: Your Azure service connection
- `appName`: Target App Service name

### Manual Deployment

```bash
# Publish the application
dotnet publish -c Release -o ./publish

# Deploy to Azure (requires Azure CLI)
az webapp deploy --resource-group <resource-group> \
                 --name <app-name> \
                 --src-path ./publish
```

## 📊 Monitoring

### Application Insights

The API is integrated with Azure Application Insights for:
- Request/response telemetry
- Exception tracking
- Custom event logging
- Performance metrics
- Dependency tracking

### Accessing Telemetry

1. Navigate to your Application Insights resource in Azure Portal
2. Use the Application Map to visualize dependencies
3. Query logs using Kusto Query Language (KQL)
4. Set up alerts for errors and performance degradation

### Example Queries

**Recent Errors**:
```kql
traces
| where severityLevel >= 3
| order by timestamp desc
| take 50
```

**API Performance**:
```kql
requests
| summarize avg(duration), percentile(duration, 95) by name
| order by avg_duration desc
```

## 🛠️ Development

### Local Development

1. Use `Development` environment for local testing
2. Swagger UI is enabled in development mode
3. HTTPS redirection is configured for production

### Adding New Endpoints

1. Create controller in `ResilentAPI/Controllers/`
2. Add corresponding DTOs in `ResilentAPI/Models/`
3. Write unit tests in `ResilentTest/`
4. Update this README with new endpoint documentation

### Code Style

- Follow C# coding conventions
- Use XML documentation comments for public APIs
- Maintain nullable reference types
- Keep controllers thin (delegate business logic to services)

## 🔐 Security Considerations

- HTTPS redirection enabled in production
- Anonymous authentication configured (add authentication as needed)
- Sanitize log inputs to prevent injection attacks
- Regularly update NuGet packages for security patches

## 📝 License

This project is provided as-is for educational and demonstration purposes.

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Contribution Guidelines

- Write unit tests for new features
- Maintain code coverage above 80%
- Follow existing code style and conventions
- Update documentation for API changes

## 📞 Support

For issues, questions, or suggestions:
- Open an issue in the GitHub repository
- Review existing issues and discussions

## 🔗 Resources

- [.NET 8 Documentation](https://docs.microsoft.com/dotnet/core/whats-new/dotnet-8)
- [Azure Application Insights](https://docs.microsoft.com/azure/azure-monitor/app/app-insights-overview)
- [Azure DevOps Pipelines](https://docs.microsoft.com/azure/devops/pipelines/)
- [Swagger/OpenAPI](https://swagger.io/specification/)

---

**Version**: 1.0.0  
**Last Updated**: February 2026  
**Maintainer**: DarianR0410
