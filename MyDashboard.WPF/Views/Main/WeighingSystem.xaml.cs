using System.Windows;
using System.Windows.Controls;
using MyDashboard.WPF.ViewModels;

namespace MyDashboard.WPF.Views.Main
{
    public partial class WeighingSystem : UserControl
    {
        public WeighingSystem()
        {
            InitializeComponent();
        }

        public WeighingSystemViewModel Data
        {
            get => (WeighingSystemViewModel)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                nameof(Data),   
                typeof(WeighingSystemViewModel),
                typeof(WeighingSystem),
                new PropertyMetadata(null, OnDataChanged)
            );

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (WeighingSystem)d;
            if (e.NewValue is WeighingSystemViewModel vm)
            {
                control.DataContext = vm;
            }
        }
    }
}
