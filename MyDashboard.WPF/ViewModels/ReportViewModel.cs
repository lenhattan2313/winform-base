using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using MyDashboard.WPF.Models;
using MyDashboard.WPF.Services;
using MyDashboard.WPF.Services.Report;

namespace MyDashboard.WPF.ViewModels
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        private readonly ReportService _reportService;
        private readonly IExcelExportService _excelExportService;

        public ObservableCollection<ReportRecord> Reports { get; } = new();

        public ObservableCollection<string> Stations { get; } = new() 
        { 
            "All", "Station 1", "Station 2", "Station 3", "Station 4", "Station 5", "Station 6", "Station 7", "Station 8", "Station 9", "Station 10", "Station 11", "Station 12", "Station 13", "Station 14", "Station 15", "Station 16", "Station 17" 
        };

        private string _selectedStation = "All";
        public string SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation = value; OnPropertyChanged(nameof(SelectedStation)); }
        }

        private DateTime? _endTimeFilter;
        public DateTime? EndTimeFilter
        {
            get => _endTimeFilter;
            set { _endTimeFilter = value; OnPropertyChanged(nameof(EndTimeFilter)); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        public ICommand SearchCommand { get; }
        public ICommand ClearFiltersCommand { get; }
        public ICommand ExportToExcelCommand { get; }

        public ReportViewModel(ReportService reportService, IExcelExportService excelExportService)
        {
            _reportService = reportService;
            _excelExportService = excelExportService;
            SearchCommand = new RelayCommand(_ => LoadReports());
            ClearFiltersCommand = new RelayCommand(_ => ClearFilters());
            ExportToExcelCommand = new RelayCommand(async _ => await ExportToExcel());
            LoadReports();
        }

        private async void LoadReports()
        {
            Reports.Clear();
            var data = await _reportService.GetReportsAsync(SelectedStation, EndTimeFilter, SearchText);
            System.Diagnostics.Debug.WriteLine($"Loaded {data.Count} reports");
            foreach (var report in data)
                Reports.Add(report);
        }

        private void ClearFilters()
        {
            SelectedStation = "All";
            SearchText = string.Empty;
            EndTimeFilter = null;
            LoadReports();
        }

        private async System.Threading.Tasks.Task ExportToExcel()
        {
            await _excelExportService.ExportReportsToExcelAsync(Reports);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
} 