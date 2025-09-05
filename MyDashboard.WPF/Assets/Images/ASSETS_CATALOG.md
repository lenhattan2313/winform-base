# Assets Catalog

This document catalogs all the image assets available in the MyDashboard.WPF application.

## 📁 Directory Structure

```
Assets/Images/
├── Icons/          # Application icons (.ico files)
├── UI/            # User interface images (.bmp, .gif, .wmf)
├── Backgrounds/   # Background images (.jpg)
└── ASSETS_CATALOG.md
```

## 🎯 Icons Directory

### Navigation Icons

- **ArrowDown16.ico** - 16x16 down arrow icon
- **ArrowDown32.ico** - 32x32 down arrow icon
- **ArrowUp16.ico** - 16x16 up arrow icon
- **ArrowUp32.ico** - 32x32 up arrow icon

### Action Icons

- **Delete.ico** - Delete/remove action icon
- **user.ico** - User profile icon
- **userpass.ico** - User password icon
- **userset.ico** - User settings icon

### System Icons

- **Wheel16_1.ico** - Loading wheel icon (variant 1)
- **Wheel16_2.ico** - Loading wheel icon (variant 2)
- **icon1.ico** - Generic application icon 1
- **icon2.ico** - Generic application icon 2
- **icon3.ico** - Generic application icon 3

## 🖼️ UI Images Directory

### Interface Elements

- **Background.bmp** - Main background image
- **Main22-1.bmp** - Main interface image
- **exit1.bmp** - Exit button image
- **home.gif** - Home navigation image
- **print.gif** - Print action image
- **Search.gif** - Search interface image

### Industrial/Equipment Images

- **Bag.bmp** - Bag/container image
- **Blue barrel.wmf** - Blue barrel vector image
- **Green barrel.wmf** - Green barrel vector image
- **DB1.bmp** - Database/equipment image
- **filling.bmp** - Filling process image
- **filling2.bmp** - Filling process image (variant 2)
- **gp1.bmp** - Equipment/process image
- **mtcn.wmf** - Equipment vector image
- **SBD1.bmp** - Equipment image
- **SBS1.bmp** - Equipment image
- **Site.bmp** - Site/location image
- **SL1.bmp** - Equipment image
- **SL12.bmp** - Equipment image (variant)
- **SN1.bmp** - Equipment image
- **槽车.bmp** - Truck/vehicle image

## 🎨 Background Images

### System Backgrounds

- **Shutdown.jpg** - System shutdown background

## 💻 Usage Examples

### Using Icons in XAML

```xml
<!-- Small icon -->
<Image Source="{StaticResource IconArrowDown16}" Style="{StaticResource SmallIconStyle}"/>

<!-- Medium icon -->
<Image Source="{StaticResource IconUser}" Style="{StaticResource MediumIconStyle}"/>

<!-- Large icon -->
<Image Source="{StaticResource IconDelete}" Style="{StaticResource LargeIconStyle}"/>
```

### Using UI Images

```xml
<!-- Background image -->
<Border>
    <Border.Background>
        <ImageBrush ImageSource="{StaticResource ImageBackground}"/>
    </Border.Background>
</Border>

<!-- Interface image -->
<Image Source="{StaticResource ImageHome}" Style="{StaticResource IconImageStyle}"/>
```

### Using Background Images

```xml
<!-- Full background -->
<Window.Background>
    <ImageBrush ImageSource="{StaticResource BackgroundShutdown}"
                Stretch="UniformToFill"/>
</Window.Background>
```

## 🎨 Available Styles

- **IconImageStyle** - Base style for all icons
- **SmallIconStyle** - 16x16 icon style
- **MediumIconStyle** - 24x24 icon style
- **LargeIconStyle** - 32x32 icon style
- **BackgroundImageStyle** - Background image style

## 📝 Notes

1. **ICO files** are best for small icons and system integration
2. **BMP files** are uncompressed and good for simple graphics
3. **GIF files** support animation and transparency
4. **WMF files** are vector graphics, scalable without quality loss
5. **JPG files** are compressed and good for photographs

## 🔄 Adding New Assets

1. Place files in appropriate subdirectory
2. Add resource definitions to `Assets/Styles/Images.xaml`
3. Update this catalog
4. Test in application
