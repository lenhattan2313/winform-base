using System;

namespace MyDashboard.WPF.Models
{
    public class ReportRecord
    {
        public int Id { get; set; }
        public double Tare { get; set; }
        public double Net { get; set; }
        public double PauseNet { get; set; }
        public double Gross { get; set; }
        public double Target { get; set; }
        public double RecheckedWT { get; set; }
        public double Remains { get; set; }
        public double StartWT { get; set; }
        public double TotalWT { get; set; }
        public int StartBags { get; set; }
        public int TotalBags { get; set; }
        public string SiloName { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string Shift { get; set; } = string.Empty;
        public int ScaleNo { get; set; }
        public string ScaleName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string ScaleType { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string Spare_Ch1 { get; set; } = string.Empty;
        public int Station { get; set; } = 1;

        // Additional properties for backward compatibility and filtering
        public string StationDisplay 
        { 
            get 
            {
                return $"Station {Station}";
            }
        }
        
        public DateTime DateTime 
        { 
            get 
            {
                if (DateTime.TryParse(EndTime, out DateTime result))
                    return result;
                return DateTime.Now;
            }
        }
        
        public string Line 
        { 
            get 
            {
                // Extract line information from SiloName or other fields
                if (!string.IsNullOrEmpty(SiloName))
                    return $"Line{ScaleNo}";
                return "Line1";
            }
        }
        
        public string ReportType { get; set; } = "Production";
        public string Title 
        { 
            get 
            {
                return $"Production Report - {SiloName} - Bags: {TotalBags}";
            }
        }
        
        public string Description 
        { 
            get 
            {
                return $"Target: {Target}, Net: {Net}, Total: {TotalWT}, Bags: {TotalBags}";
            }
        }
        
        public string Status { get; set; } = "Completed";
        public string GeneratedBy { get; set; } = "Operator";
    }
} 