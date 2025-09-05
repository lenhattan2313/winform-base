# Images Directory

This directory contains all image assets for the application.

## Supported Formats

- **PNG**: Recommended for icons and graphics with transparency
- **JPG**: For photographs and complex images
- **SVG**: For scalable vector graphics (convert to XAML for WPF)
- **ICO**: For application icons

## Organization

```
Images/
├── Icons/           # Application icons
├── Backgrounds/     # Background images
├── Logos/          # Company/product logos
├── Illustrations/  # UI illustrations
└── Screenshots/    # Documentation screenshots
```

## Usage in XAML

```xml
<!-- Image control -->
<Image Source="pack://application:,,,/Assets/Images/logo.png"
       Width="100" Height="50"/>

<!-- As background -->
<Border Background="{StaticResource SurfaceBrush}">
    <Border.Background>
        <ImageBrush ImageSource="pack://application:,,,/Assets/Images/background.jpg"/>
    </Border.Background>
</Border>
```

## Best Practices

1. **Optimize file sizes** - Use appropriate compression
2. **Use consistent naming** - snake_case or camelCase
3. **Provide multiple resolutions** - For different DPI settings
4. **Include alt text** - For accessibility
5. **Use vector graphics** when possible - Better scaling
