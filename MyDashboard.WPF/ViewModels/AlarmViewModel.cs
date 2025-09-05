using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MyDashboard.WPF.Models;
using MyDashboard.WPF.Services;

namespace MyDashboard.WPF.ViewModels
{
    public class AlarmViewModel : INotifyPropertyChanged
    {
        private readonly AlarmService _alarmService;

        public ObservableCollection<AlarmRecord> Alarms { get; } = new();

        public ObservableCollection<string> Lines { get; } = new() { "All", "Line1", "Line2", "Line3" };

        private string _selectedLine = "All";
        public string SelectedLine
        {
            get => _selectedLine;
            set { _selectedLine = value; OnPropertyChanged(nameof(SelectedLine)); }
        }

        private DateTime _fromDate = DateTime.Today.AddDays(-7);
        public DateTime FromDate
        {
            get => _fromDate;
            set { _fromDate = value; OnPropertyChanged(nameof(FromDate)); }
        }

        private DateTime _toDate = DateTime.Today;
        public DateTime ToDate
        {
            get => _toDate;
            set { _toDate = value; OnPropertyChanged(nameof(ToDate)); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        public ICommand SearchCommand { get; }
        public ICommand ClearFiltersCommand { get; }

        public AlarmViewModel(AlarmService alarmService)
        {
            _alarmService = alarmService;
            SearchCommand = new RelayCommand(_ => LoadAlarms());
            ClearFiltersCommand = new RelayCommand(_ => ClearFilters());
            LoadAlarms();
        }

        private async void LoadAlarms()
        {
            Alarms.Clear();
            var data = await _alarmService.GetAlarmsAsync(SelectedLine, FromDate, ToDate, SearchText);
            foreach (var alarm in data)
                Alarms.Add(alarm);
        }

        private void ClearFilters()
        {
            SelectedLine = "All";
            SearchText = string.Empty;
            FromDate = DateTime.Today.AddDays(-7);
            ToDate = DateTime.Today;
            LoadAlarms();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
