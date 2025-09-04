# MyDashboard API Documentation

## Overview

This document provides detailed API documentation for the MyDashboard application components.

## Core Models

### AlarmRecord

Represents an alarm event in the system.

```csharp
public class AlarmRecord
{
    public DateTime DateTime { get; set; }
    public string Line { get; set; }
    public string Message { get; set; }
}
```

**Properties:**

- `DateTime`: The timestamp when the alarm occurred
- `Line`: The production line identifier (e.g., "Line1", "Line2")
- `Message`: The alarm message description

### ReportRecord

Represents a report entry in the system.

```csharp
public class ReportRecord
{
    public DateTime DateTime { get; set; }
    public string Line { get; set; }
    public string Detail { get; set; }
}
```

**Properties:**

- `DateTime`: The timestamp of the report entry
- `Line`: The production line identifier
- `Detail`: Detailed information about the report entry

## Services

### AlarmService

Handles business logic for alarm operations.

```csharp
public class AlarmService
{
    public List<AlarmRecord> GetAlarms(string line, DateTime from, DateTime to, string search)
}
```

**Methods:**

#### GetAlarms

Retrieves filtered alarm records based on specified criteria.

**Parameters:**

- `line` (string): Line filter - "All" for all lines, or specific line name
- `from` (DateTime): Start date for filtering
- `to` (DateTime): End date for filtering
- `search` (string): Text search filter for alarm messages

**Returns:**

- `List<AlarmRecord>`: Filtered list of alarm records

**Example:**

```csharp
var alarmService = new AlarmService();
var alarms = alarmService.GetAlarms("Line1", DateTime.Today, DateTime.Now, "overheat");
```

### ReportService

Handles business logic for report operations.

```csharp
public class ReportService
{
    // Implementation coming soon
}
```

## Data Access

### AlarmRepository

Provides data access for alarm records.

```csharp
public class AlarmRepository
{
    public List<AlarmRecord> GetAlarms(DateTime from, DateTime to)
}
```

**Methods:**

#### GetAlarms

Retrieves alarm records within the specified date range.

**Parameters:**

- `from` (DateTime): Start date for filtering
- `to` (DateTime): End date for filtering

**Returns:**

- `List<AlarmRecord>`: List of alarm records within the date range

**Example:**

```csharp
var repository = new AlarmRepository();
var alarms = repository.GetAlarms(DateTime.Today, DateTime.Now);
```

### ReportRepository

Provides data access for report records.

```csharp
public class ReportRepository
{
    // Implementation coming soon
}
```

## UI Components

### MainWindow

Main application window with navigation capabilities.

```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    private void LoadPage(UserControl page)
    private void BtnAlarm_Click(object sender, RoutedEventArgs e)
    private void BtnReport_Click(object sender, RoutedEventArgs e)
    private void BtnSetting_Click(object sender, RoutedEventArgs e)
}
```

**Methods:**

#### LoadPage

Loads a specified user control into the main content area.

**Parameters:**

- `page` (UserControl): The page to load

### FilterBar

Custom user control for filtering data.

```csharp
public partial class FilterBar : UserControl
{
    public event EventHandler<FilterEventArgs> FilterChanged;
}
```

**Events:**

#### FilterChanged

Raised when filter criteria changes.

**Event Args:**

```csharp
public class FilterEventArgs : EventArgs
{
    public string Line { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string SearchText { get; set; }
}
```

### AlarmPage

Page for displaying and managing alarm records.

```csharp
public partial class AlarmPage : UserControl
{
    public AlarmPage()
    private void OnFilterChanged(object sender, FilterEventArgs e)
}
```

## Usage Examples

### Basic Alarm Retrieval

```csharp
// Create service instance
var alarmService = new AlarmService();

// Get all alarms for today
var todayAlarms = alarmService.GetAlarms("All", DateTime.Today, DateTime.Now, "");

// Get alarms for specific line with search
var lineAlarms = alarmService.GetAlarms("Line1", DateTime.Today, DateTime.Now, "error");
```

### Filter Event Handling

```csharp
// Subscribe to filter changes
filterBar.FilterChanged += (sender, e) => {
    var alarms = alarmService.GetAlarms(e.Line, e.FromDate, e.ToDate, e.SearchText);
    dataGridView.ItemsSource = alarms;
};
```

### Page Navigation

```csharp
// Load different pages
mainWindow.LoadPage(new AlarmPage());
mainWindow.LoadPage(new ReportPage());
mainWindow.LoadPage(new SettingPage());
```

## Error Handling

The current implementation uses basic error handling. For production use, consider implementing:

- Try-catch blocks around service calls
- Validation of input parameters
- Logging of errors and exceptions
- User-friendly error messages

## Performance Considerations

- Repository methods return in-memory lists (suitable for small datasets)
- For large datasets, consider implementing pagination
- Filter operations are performed in memory using LINQ
- Consider caching frequently accessed data

## Future API Enhancements

- Async/await support for all service methods
- Generic repository pattern
- Dependency injection support
- Validation attributes for models
- API versioning support
