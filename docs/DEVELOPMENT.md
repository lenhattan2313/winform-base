# MyDashboard Development Guide

## Development Environment Setup

### Prerequisites

1. **.NET 8 SDK**

   - Download from [Microsoft .NET Downloads](https://dotnet.microsoft.com/download)
   - Verify installation: `dotnet --version`

2. **IDE Options**

   - **Visual Studio 2022** (Recommended)
     - Community Edition (Free)
     - Professional or Enterprise
   - **Visual Studio Code**
     - Install C# extension
     - Install .NET extension pack

3. **Git** (Optional but recommended)
   - For version control
   - Download from [Git Website](https://git-scm.com/)

### Project Setup

1. **Clone/Download the project**

   ```bash
   git clone <repository-url>
   cd MyDashboard
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Build the solution**

   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run --project MyDashboard.UI
   ```

## Development Workflow

### 1. Adding New Features

#### Step 1: Define the Model

Create or update models in `MyDashboard.Core/Models/`:

```csharp
// Example: Adding a new model
public class NewRecord
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

#### Step 2: Create Repository

Add data access logic in `MyDashboard.Data/`:

```csharp
// Example: New repository
public class NewRecordRepository
{
    public List<NewRecord> GetRecords()
    {
        // Implementation
    }
}
```

#### Step 3: Implement Service

Add business logic in `MyDashboard.Core/Services/`:

```csharp
// Example: New service
public class NewRecordService
{
    private readonly NewRecordRepository _repository = new();

    public List<NewRecord> GetFilteredRecords(string filter)
    {
        // Business logic
    }
}
```

#### Step 4: Create UI

Add user interface in `MyDashboard.UI/`:

```csharp
// Example: New page
public partial class NewRecordPage : UserControl
{
    private readonly NewRecordService _service = new();

    public NewRecordPage()
    {
        InitializeComponent();
    }
}
```

### 2. Code Style Guidelines

#### Naming Conventions

- **Classes**: PascalCase (`AlarmService`, `MainForm`)
- **Methods**: PascalCase (`GetAlarms`, `LoadPage`)
- **Properties**: PascalCase (`DateTime`, `Message`)
- **Fields**: camelCase with underscore prefix (`_alarmService`)
- **Local Variables**: camelCase (`alarmList`, `filterText`)

#### File Organization

- One class per file
- Use partial classes for Designer files
- Group related files in folders
- Use meaningful file names

#### Code Formatting

- Use 4 spaces for indentation
- Use braces for all control structures
- Place opening brace on same line
- Add blank lines between logical sections

### 3. Debugging

#### Visual Studio Debugging

1. Set breakpoints by clicking in the left margin
2. Press F5 to start debugging
3. Use F10 (Step Over) and F11 (Step Into) to navigate
4. Use the Debug windows (Locals, Watch, Call Stack)

#### Console Debugging

```csharp
// Add debug output
System.Diagnostics.Debug.WriteLine($"Processing alarm: {alarm.Message}");
Console.WriteLine($"Filter applied: {filterText}");
```

#### Exception Handling

```csharp
try
{
    var alarms = _alarmService.GetAlarms(line, from, to, search);
    dataGridView1.DataSource = alarms;
}
catch (Exception ex)
{
    MessageBox.Show($"Error loading alarms: {ex.Message}", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

### 4. Code Quality

#### Code Review Checklist

- Follow naming conventions
- Add meaningful comments
- Handle exceptions properly
- Use consistent formatting
- Validate input parameters

#### Performance Monitoring

```csharp
// Monitor method execution time
var stopwatch = System.Diagnostics.Stopwatch.StartNew();
var result = _alarmService.GetAlarms(line, from, to, search);
stopwatch.Stop();
System.Diagnostics.Debug.WriteLine($"GetAlarms took {stopwatch.ElapsedMilliseconds}ms");
```

### 5. Performance Optimization

#### Data Loading

```csharp
// Use async/await for long operations
public async Task<List<AlarmRecord>> GetAlarmsAsync(string line, DateTime from, DateTime to, string search)
{
    return await Task.Run(() => GetAlarms(line, from, to, search));
}
```

#### UI Responsiveness

```csharp
// Use background workers for long operations
private void LoadDataInBackground()
{
    var worker = new BackgroundWorker();
    worker.DoWork += (s, e) => {
        e.Result = _alarmService.GetAlarms("All", DateTime.Today, DateTime.Now, "");
    };
    worker.RunWorkerCompleted += (s, e) => {
        dataGridView1.DataSource = e.Result;
    };
    worker.RunWorkerAsync();
}
```

### 6. Error Handling Best Practices

#### Service Layer

```csharp
public List<AlarmRecord> GetAlarms(string line, DateTime from, DateTime to, string search)
{
    try
    {
        // Validate input parameters
        if (from > to)
            throw new ArgumentException("From date cannot be greater than To date");

        var data = _repo.GetAlarms(from, to);

        // Apply filters
        if (!string.IsNullOrEmpty(line) && line != "All")
            data = data.Where(a => a.Line == line).ToList();

        return data;
    }
    catch (Exception ex)
    {
        // Log the error
        System.Diagnostics.Debug.WriteLine($"Error in GetAlarms: {ex.Message}");
        throw;
    }
}
```

#### UI Layer

```csharp
private void OnFilterChanged(object sender, FilterEventArgs e)
{
    try
    {
        var alarms = _alarmService.GetAlarms(e.Line, e.FromDate, e.ToDate, e.SearchText);
        dataGridView1.DataSource = alarms;
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error loading alarms: {ex.Message}", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

### 7. Configuration Management

#### App.config (Future Enhancement)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="DatabaseConnectionString" value="..." />
    <add key="LogLevel" value="Information" />
    <add key="RefreshInterval" value="30" />
  </appSettings>
</configuration>
```

#### Configuration Class

```csharp
public class AppConfiguration
{
    public string DatabaseConnectionString { get; set; }
    public string LogLevel { get; set; }
    public int RefreshInterval { get; set; }
}
```

### 8. Debugging and Monitoring

#### Debug Output

```csharp
// Use debug output for development
System.Diagnostics.Debug.WriteLine($"Processing alarm: {alarm.Message}");
System.Diagnostics.Debug.WriteLine($"Filter applied: {filterText}");
```

### 9. Database Integration

#### Entity Framework Setup (Future)

1. Install packages:

   ```bash
   dotnet add package Microsoft.EntityFrameworkCore
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   ```

2. Create DbContext:

   ```csharp
   public class DashboardDbContext : DbContext
   {
       public DbSet<AlarmRecord> Alarms { get; set; }
       public DbSet<ReportRecord> Reports { get; set; }
   }
   ```

3. Update repository:

   ```csharp
   public class AlarmRepository
   {
       private readonly DashboardDbContext _context;

       public AlarmRepository(DashboardDbContext context)
       {
           _context = context;
       }

       public List<AlarmRecord> GetAlarms(DateTime from, DateTime to)
       {
           return _context.Alarms
               .Where(a => a.DateTime >= from && a.DateTime <= to)
               .ToList();
       }
   }
   ```

### 10. Deployment

#### Build for Release

```bash
dotnet build --configuration Release
```

#### Publish Application

```bash
dotnet publish MyDashboard.UI --configuration Release --output ./publish
```

#### Create Installer (Future)

- Use WiX Toolset for MSI installer
- Use Inno Setup for custom installer
- Use ClickOnce for easy deployment

## Troubleshooting

### Common Issues

1. **Build Errors**

   - Check .NET version compatibility
   - Verify all project references
   - Clean and rebuild solution

2. **Runtime Errors**

   - Check exception details in debug output
   - Verify data source connections
   - Check file permissions

3. **UI Issues**
   - Verify Designer file synchronization
   - Check control docking and anchoring
   - Validate event handler connections

### Getting Help

1. Check the documentation
2. Review error messages and stack traces
3. Use debugging tools
4. Search online resources
5. Contact the development team

## Best Practices Summary

1. **Follow SOLID principles**
2. **Write clean, readable code**
3. **Add meaningful comments**
4. **Handle errors gracefully**
5. **Use version control**
6. **Document your changes**
7. **Keep dependencies up to date**
8. **Follow security best practices**
9. **Optimize for performance**
