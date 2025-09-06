using MyDashboard.WPF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MyDashboard.WPF.Views.Main
{
    public partial class MainPage : UserControl
    {
        private readonly Random _random = new Random();
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        public ObservableCollection<WeighingSystemViewModel> WeighingSystems { get; set; }


        public MainPage()
        {
            InitializeComponent();

            WeighingSystems = new ObservableCollection<WeighingSystemViewModel>();

            for (int i = 1; i <= 17; i++)
            {
                WeighingSystems.Add(CreateSystem($"CB-603-{i}", $"Station {i}"));
            }

            DataContext = this;

            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += (s, e) => UpdateRandomData();
            _timer.Start();
        }

        private WeighingSystemViewModel CreateSystem(string barcode, string plcName)
        {
            var vm = new WeighingSystemViewModel
            {
                PlcName = plcName,
                Days = _random.Next(0, 5000),
                Hours = _random.Next(0, 24),
                Minutes = _random.Next(0, 60),
                Seconds = _random.Next(0, 60),
                PreSetBags = 100,
                WeightEachBag = 1150.5,
                LastWeight = Math.Round(1150.5 + _random.NextDouble() * 10 - 5, 1),
                TotalBags = _random.Next(0, 101),
                Barcode = barcode,

                AutoManual = RandomState(),
                Feed = RandomState(),
                Discharge = RandomState(),
                ReleaseBag = RandomState(),
                Pause = RandomState(),
                Emergency = RandomState(),
                Tolerance = RandomState(),
                Complete = RandomState(),
                ToPresetBags = RandomState(),
                DataWillOver = RandomState(),
                EmptyScale = RandomState()
            };

            vm.TotalWeight = vm.TotalBags * vm.WeightEachBag;

            return vm;
        }

        private void UpdateRandomData()
        {
            foreach (var system in WeighingSystems)
            {
                RandomizeSystem(system);
            }
        }


        private void RandomizeSystem(WeighingSystemViewModel system)
        {
            if (system == null) return;
            system.Hours = _random.Next(0, 24);
            system.Minutes = _random.Next(0, 60);
            system.Seconds = _random.Next(0, 60);
            system.TotalBags = _random.Next(0, system.PreSetBags + 1);
            system.TotalWeight = (int)(system.TotalBags * system.WeightEachBag);
            system.LastWeight = (int)(system.WeightEachBag + _random.Next(-5, 6));

            system.AutoManual = RandomState();
            system.Feed = RandomState();
            system.Discharge = RandomState();
            system.ReleaseBag = RandomState();
            system.Pause = RandomState();
            system.Emergency = RandomState();
            system.Tolerance = RandomState();
            system.Complete = RandomState();
            system.ToPresetBags = RandomState();
            system.DataWillOver = RandomState();
            system.EmptyScale = RandomState();
        }

        private IndicatorState RandomState()
        {
            int r = _random.Next(3); // 0=Gray, 1=Green, 2=Red
            return r switch
            {
                1 => IndicatorState.Green,
                2 => IndicatorState.Red,
                _ => IndicatorState.Gray
            };
        }
    }
}
