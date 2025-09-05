# MyDashboard.WPF - Architecture Documentation

## Project Overview

- **Project Name**: MyDashboard.WPF
- **Framework**: WPF (.NET 6.0)
- **Architecture**: MVVM (Model-View-ViewModel)
- **Target Platform**: Windows

## Architecture Pattern: MVVM

### MVVM Components

1. **Models**: Data entities and business objects
2. **ViewModels**: Presentation logic and data binding
3. **Views**: XAML UI definitions and user interactions

## Best Practices Implementation

### 1. MVVM Pattern

- ✅ **Models**: Pure data classes (AlarmRecord.cs)
- ✅ **ViewModels**: Business logic with INotifyPropertyChanged (AlarmViewModel.cs)
- ✅ **Views**: XAML-only with data binding (AlarmPage.xaml)

### 2. Data Binding & Commands

- ✅ **ObservableCollection<T>**: Used for Alarms collection
- ✅ **INotifyPropertyChanged**: Implemented in AlarmViewModel
- ✅ **RelayCommand**: Custom command implementation for SearchCommand
- ✅ **Two-way binding**: SearchText, SelectedLine, Date properties

### 3. Project Organization

```
MyDashboard.WPF/
├── Models/           # Data entities
├── ViewModels/       # Presentation logic
├── Views/           # XAML UI definitions
├── Services/        # Business logic services
├── Helpers/         # Utility classes
└── Assets/          # All application assets
    ├── Images/      # Image files (PNG, JPG, SVG)
    ├── Icons/       # Icon files and icon fonts
    ├── Fonts/       # Custom fonts
    ├── Styles/      # Resource dictionaries
    └── Data/        # Static data and config files
```

### 4. Layout Best Practices

- ✅ **Grid Layout**: Used instead of DockPanel/StackPanel
- ✅ **Explicit Definitions**: RowDefinitions and ColumnDefinitions
- ✅ **Responsive Design**: Auto and \* sizing for flexibility

## Project Structure

### Models

- **AlarmRecord.cs**: Data model for alarm records
  - Properties: DateTime, Line, Message

### ViewModels

- **AlarmViewModel.cs**: Main view model for alarm management
  - Implements INotifyPropertyChanged
  - Exposes ObservableCollection<AlarmRecord> Alarms
  - Properties: SearchText, SelectedLine, FromDate, ToDate
  - Command: SearchCommand (RelayCommand)
  - Method: LoadAlarms() for data refresh

### Views

- **AlarmPage.xaml**: Main alarm management interface
  - Row 0: Filter bar (ComboBox, DatePickers, TextBox, Button)
  - Row 1: DataGrid (table)
  - Row 2: Footer (Cancel/Save buttons)

### Services

- **AlarmService.cs**: Business logic for alarm data
  - Mock data implementation
  - Filtering and search functionality

### Helpers

- **RelayCommand.cs**: Command pattern implementation
  - ICommand interface implementation
  - CanExecute and Execute delegates

## Data Binding Examples

### AlarmPage.xaml Bindings

```xml
<!-- ComboBox binding -->
<ComboBox SelectedItem="{Binding SelectedLine}"
          ItemsSource="{Binding Lines}"/>

<!-- DatePicker bindings -->
<DatePicker SelectedDate="{Binding FromDate}"/>
<DatePicker SelectedDate="{Binding ToDate}"/>

<!-- TextBox binding with PropertyChanged trigger -->
<TextBox Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}"/>

<!-- Button command binding -->
<Button Command="{Binding SearchCommand}" Content="Search"/>

<!-- DataGrid binding -->
<DataGrid ItemsSource="{Binding Alarms}" AutoGenerateColumns="True"/>
```

## Grid Layout Structure

### Main Grid (3 rows)

```xml
<Grid.RowDefinitions>
    <RowDefinition Height="Auto"/>  <!-- Filter bar -->
    <RowDefinition Height="*"/>     <!-- DataGrid -->
    <RowDefinition Height="Auto"/>  <!-- Footer -->
</Grid.RowDefinitions>
```

### Filter Bar Grid (6 columns)

```xml
<Grid.ColumnDefinitions>
    <ColumnDefinition Width="Auto"/>  <!-- ComboBox -->
    <ColumnDefinition Width="Auto"/>  <!-- From Date -->
    <ColumnDefinition Width="Auto"/>  <!-- To Date -->
    <ColumnDefinition Width="Auto"/>  <!-- Search Text -->
    <ColumnDefinition Width="Auto"/>  <!-- Search Button -->
    <ColumnDefinition Width="*"/>     <!-- Spacer -->
</Grid.ColumnDefinitions>
```

## Development Guidelines

### Code-Behind Usage

- ❌ **Avoid**: Business logic in code-behind
- ✅ **Allow**: View-specific UI tweaks only
- ✅ **Example**: InitializeComponent() calls

### Style and Resource Management

- ✅ **Centralized**: All styles in Assets/Styles/ resource dictionaries
- ✅ **Modular**: Separate dictionaries for Colors, Typography, Controls, Icons
- ✅ **Reusable**: Common button, textbox, and grid styles
- ✅ **Consistent**: Color scheme and typography
- ✅ **Icon System**: Segoe MDL2 Assets font for consistent icons
- ✅ **Asset Organization**: Structured folders for Images, Icons, Fonts, Data

### Error Handling

- ✅ **Service Layer**: Handle data access errors
- ✅ **ViewModel**: Handle business logic errors
- ✅ **View**: Display user-friendly error messages

## Future Enhancements

### Planned Features

1. **ReportPage**: Chart and analytics views
2. **SettingPage**: Configuration management
3. **Real-time Updates**: SignalR integration
4. **Export Functionality**: PDF/Excel export
5. **User Authentication**: Login and role management

### Technical Improvements

1. **Dependency Injection**: IoC container integration
2. **Unit Testing**: ViewModel and Service testing
3. **Logging**: Structured logging implementation
4. **Performance**: Virtualization for large datasets
5. **Theming**: Dark/Light theme support

## Asset Management

### Asset Organization

The project uses a structured approach to manage all application assets:

#### 1. Resource Dictionaries

- **Colors.xaml**: Color palette and theme colors
- **Typography.xaml**: Font families, sizes, and text styles
- **Controls.xaml**: Control templates and styles
- **Icons.xaml**: Icon definitions and icon styles
- **AppResources.xaml**: Main resource dictionary that merges all others

#### 2. Asset Folders

```
Assets/
├── Images/          # Image files (PNG, JPG, SVG)
│   ├── Icons/      # Application icons
│   ├── Backgrounds/ # Background images
│   ├── Logos/      # Company/product logos
│   └── Illustrations/ # UI illustrations
├── Icons/          # Icon files and icon fonts
├── Fonts/          # Custom fonts
├── Styles/         # Resource dictionaries
└── Data/           # Static data and config files
    ├── Config/     # Configuration files
    ├── Mock/      # Mock data for development
    └── Templates/ # File templates
```

### Asset Usage Patterns

#### Icons

```xml
<!-- Using icon font -->
<TextBlock Text="{StaticResource IconSearch}" Style="{StaticResource IconStyle}"/>

<!-- Icon button -->
<Button Style="{StaticResource IconButtonStyle}" Content="{StaticResource IconSettings}"/>
```

#### Images

```xml
<!-- Image control -->
<Image Source="pack://application:,,,/Assets/Images/logo.png" Width="100" Height="50"/>

<!-- Background image -->
<Border>
    <Border.Background>
        <ImageBrush ImageSource="pack://application:,,,/Assets/Images/background.jpg"/>
    </Border.Background>
</Border>
```

#### Data Files

```csharp
// Loading configuration
var resourceStream = Application.GetResourceStream(
    new Uri("pack://application:,,,/Assets/Data/Config/settings.json"));
```

### Best Practices

1. **Consistent Naming**: Use snake_case or camelCase for file names
2. **Optimize Assets**: Compress images and use appropriate formats
3. **Resource Keys**: Use descriptive names for resource keys
4. **Modular Styles**: Separate concerns into different resource dictionaries
5. **Icon Fonts**: Prefer icon fonts over image icons for scalability
6. **Asset Versioning**: Track changes to important assets

## Getting Started

### Prerequisites

- Visual Studio 2022 or later
- .NET 6.0 SDK
- Windows 10/11

### Building and Running

```bash
# Restore packages
dotnet restore

# Build solution
dotnet build

# Run application
dotnet run --project MyDashboard.WPF
```

### Development Workflow

1. Create/Update Models in Models/ folder
2. Implement business logic in Services/
3. Create ViewModels with proper data binding
4. Design Views with Grid layout and data binding
5. Test MVVM separation and data flow
