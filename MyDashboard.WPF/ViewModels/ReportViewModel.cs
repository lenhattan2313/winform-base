using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            var data = await _reportService.GetReportsAsync(SearchText);
            System.Diagnostics.Debug.WriteLine($"Loaded {data.Count} reports");
            foreach (var report in data)
                Reports.Add(report);
        }

        private void ClearFilters()
        {
            SearchText = string.Empty;
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