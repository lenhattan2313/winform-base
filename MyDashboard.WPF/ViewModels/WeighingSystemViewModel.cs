using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyDashboard.WPF.ViewModels
{
    public enum IndicatorState
    {
        Gray,
        Green,
        Red
    }

    public class WeighingSystemViewModel : INotifyPropertyChanged
    {
        private string _plcName;
        public string PlcName
        {
            get => _plcName;
            set { _plcName = value; OnPropertyChanged(); }
        }

        private int _days;
        public int Days
        {
            get => _days;
            set { _days = value; OnPropertyChanged(); }
        }

        private int _hours;
        public int Hours
        {
            get => _hours;
            set { _hours = value; OnPropertyChanged(); }
        }

        private int _minutes;
        public int Minutes
        {
            get => _minutes;
            set { _minutes = value; OnPropertyChanged(); }
        }

        private int _seconds;
        public int Seconds
        {
            get => _seconds;
            set { _seconds = value; OnPropertyChanged(); }
        }

        private int _preSetBags;
        public int PreSetBags
        {
            get => _preSetBags;
            set { _preSetBags = value; OnPropertyChanged(); OnPropertyChanged(nameof(Progress)); }
        }

        private double _weightEachBag;
        public double WeightEachBag
        {
            get => _weightEachBag;
            set { _weightEachBag = value; OnPropertyChanged(); }
        }

        private double _lastWeight;
        public double LastWeight
        {
            get => _lastWeight;
            set { _lastWeight = value; OnPropertyChanged(); }
        }

        private int _totalBags;
        public int TotalBags
        {
            get => _totalBags;
            set { _totalBags = value; OnPropertyChanged(); OnPropertyChanged(nameof(Progress)); }
        }

        private double _totalWeight;
        public double TotalWeight
        {
            get => _totalWeight;
            set { _totalWeight = value; OnPropertyChanged(); }
        }

        private string _barcode;
        public string Barcode
        {
            get => _barcode;
            set { _barcode = value; OnPropertyChanged(); }
        }

        private IndicatorState _autoManual;
        public IndicatorState AutoManual
        {
            get => _autoManual;
            set { _autoManual = value; OnPropertyChanged(); }
        }

        private IndicatorState _feed;
        public IndicatorState Feed
        {
            get => _feed;
            set { _feed = value; OnPropertyChanged(); }
        }

        private IndicatorState _discharge;
        public IndicatorState Discharge
        {
            get => _discharge;
            set { _discharge = value; OnPropertyChanged(); }
        }

        private IndicatorState _releaseBag;
        public IndicatorState ReleaseBag
        {
            get => _releaseBag;
            set { _releaseBag = value; OnPropertyChanged(); }
        }

        private IndicatorState _pause;
        public IndicatorState Pause
        {
            get => _pause;
            set { _pause = value; OnPropertyChanged(); }
        }

        private IndicatorState _emergency;
        public IndicatorState Emergency
        {
            get => _emergency;
            set { _emergency = value; OnPropertyChanged(); }
        }

        private IndicatorState _tolerance;
        public IndicatorState Tolerance
        {
            get => _tolerance;
            set { _tolerance = value; OnPropertyChanged(); }
        }

        private IndicatorState _complete;
        public IndicatorState Complete
        {
            get => _complete;
            set { _complete = value; OnPropertyChanged(); }
        }

        private IndicatorState _toPresetBags;
        public IndicatorState ToPresetBags
        {
            get => _toPresetBags;
            set { _toPresetBags = value; OnPropertyChanged(); }
        }

        private IndicatorState _dataWillOver;
        public IndicatorState DataWillOver
        {
            get => _dataWillOver;
            set { _dataWillOver = value; OnPropertyChanged(); }
        }

        private IndicatorState _emptyScale;
        public IndicatorState EmptyScale
        {
            get => _emptyScale;
            set { _emptyScale = value; OnPropertyChanged(); }
        }

        public double Progress
        {
            get
            {
                if (PreSetBags <= 0) return 0;
                return (double)TotalBags / PreSetBags;
            }
        }

        // Notify
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
