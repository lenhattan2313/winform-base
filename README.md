# MyDashboard - WinForms Dashboard Application

A modern Windows Forms dashboard application built with .NET 8, featuring a clean architecture with separation of concerns.

## 🏗️ Architecture

The solution follows a layered architecture pattern with three main projects:

```
MyDashboard/
├── MyDashboard.UI/          # Windows Forms UI Layer
├── MyDashboard.Core/        # Business Logic Layer
├── MyDashboard.Data/        # Data Access Layer
└── MyDashboard.sln         # Solution file
```

### Project Structure

- **MyDashboard.UI**: Contains all Windows Forms, user controls, and UI logic
- **MyDashboard.Core**: Contains business models, services, and application logic
- **MyDashboard.Data**: Contains data repositories and data access logic

## 🚀 Features

### Current Features

- **Navigation System**: Clean tab-based navigation between different dashboard sections
- **Alarm Management**: View and filter alarm records with real-time search
- **Filter System**: Advanced filtering by line, date range, and text search
- **Responsive Layout**: Maximized window with docked panels for optimal screen usage

### Planned Features

- **Report Generation**: Comprehensive reporting system
- **Settings Management**: Application configuration and preferences
- **Data Export**: Export functionality for alarms and reports
- **Real-time Updates**: Live data refresh capabilities

## 🛠️ Technology Stack

- **.NET 8**: Latest .NET framework with Windows Forms support
- **Windows Forms**: Native Windows desktop UI framework
- **C# 12**: Latest C# language features
- **Clean Architecture**: Separation of concerns with dependency injection ready

## 📋 Prerequisites

- .NET 8 SDK or later
- Visual Studio 2022 or Visual Studio Code with C# extension
- Windows 10/11 (required for Windows Forms)

## 🏃‍♂️ Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd MyDashboard
```

### 2. Build the Solution

```bash
dotnet build
```

### 3. Run the Application

```bash
dotnet run --project MyDashboard.UI
```

Or open the solution in Visual Studio and press F5.

## 📁 Project Details

### MyDashboard.UI

The main Windows Forms application containing:

- **MainForm**: Main application shell with navigation
- **Pages**: Individual dashboard pages (Alarm, Report, Settings)
- **Controls**: Reusable UI components (FilterBar)

### MyDashboard.Core

Business logic layer containing:

- **Models**: Data transfer objects (AlarmRecord, ReportRecord)
- **Services**: Business logic services (AlarmService, ReportService)

### MyDashboard.Data

Data access layer containing:

- **Repositories**: Data access implementations (AlarmRepository, ReportRepository)
- **Mock Data**: Sample data for development and testing

## 🔧 Development

### Adding New Features

1. **Models**: Add new data models in `MyDashboard.Core/Models/`
2. **Services**: Implement business logic in `MyDashboard.Core/Services/`
3. **Data Access**: Create repositories in `MyDashboard.Data/`
4. **UI**: Add new pages in `MyDashboard.UI/Pages/`

### Code Style Guidelines

- Use PascalCase for public members
- Use camelCase for private fields
- Follow C# naming conventions
- Include XML documentation for public APIs
- Use meaningful variable and method names

## 🔧 Data Sources

The application currently uses mock data for development. To add real data sources:

1. Implement `IAlarmRepository` interface
2. Update `AlarmRepository` to use actual data source
3. Configure dependency injection in the UI layer

## 📊 Data Models

### AlarmRecord

```csharp
public class AlarmRecord
{
    public DateTime DateTime { get; set; }
    public string Line { get; set; }
    public string Message { get; set; }
}
```

### ReportRecord

```csharp
public class ReportRecord
{
    public DateTime DateTime { get; set; }
    public string Line { get; set; }
    public string Detail { get; set; }
}
```

## 🎯 Future Enhancements

- [ ] Database integration (SQL Server, SQLite)
- [ ] Dependency injection container
- [ ] Configuration management
- [ ] Theme support
- [ ] Multi-language support
- [ ] Export to Excel/PDF
- [ ] Real-time data updates
- [ ] User authentication

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For support and questions, please contact the development team or create an issue in the repository.

---

**Built with ❤️ using .NET 8 and Windows Forms**
