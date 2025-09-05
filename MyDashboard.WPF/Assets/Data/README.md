# Data Directory

This directory contains static data files and configuration files.

## File Types

- **JSON**: Configuration files, mock data
- **XML**: Settings, data definitions
- **CSV**: Import/export data
- **TXT**: Text resources, templates

## Organization

```
Data/
├── Config/         # Configuration files
├── Mock/          # Mock data for development
├── Templates/     # File templates
└── Resources/     # Text resources
```

## Usage Examples

### Loading JSON Configuration

```csharp
public class ConfigService
{
    public T LoadConfig<T>(string fileName)
    {
        var resourceStream = Application.GetResourceStream(
            new Uri($"pack://application:,,,/Assets/Data/Config/{fileName}"));

        using var reader = new StreamReader(resourceStream.Stream);
        var json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<T>(json);
    }
}
```

### Loading Mock Data

```csharp
public class MockDataService
{
    public List<AlarmRecord> LoadMockAlarms()
    {
        var resourceStream = Application.GetResourceStream(
            new Uri("pack://application:,,,/Assets/Data/Mock/alarms.json"));

        using var reader = new StreamReader(resourceStream.Stream);
        var json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<List<AlarmRecord>>(json);
    }
}
```

## Best Practices

1. **Use structured formats** - JSON, XML for complex data
2. **Validate data** - Check format and content
3. **Handle errors** - Graceful fallbacks for missing files
4. **Cache data** - Avoid repeated file reads
5. **Version control** - Track changes to data files
