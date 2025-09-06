using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MyDashboard.WPF.Models;
using OfficeOpenXml;

namespace MyDashboard.WPF.Services.Report
{
    public class ExcelExportService : IExcelExportService
    {
        public ExcelExportService()
        {
            // Set EPPlus license context for non-commercial use
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<bool> ExportReportsToExcelAsync(IEnumerable<ReportRecord> reports, string filePath = null)
        {
            try
            {
                var reportList = reports.ToList();
                if (!reportList.Any())
                {
                    MessageBox.Show("No data to export.", "Export Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                // If no file path provided, create default path in Documents folder
                if (string.IsNullOrEmpty(filePath))
                {
                    var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    var fileName = $"ProductionReports_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    filePath = Path.Combine(documentsPath, fileName);
                }

                await Task.Run(() =>
                {
                    using var package = new ExcelPackage();
                    var worksheet = package.Workbook.Worksheets.Add("Production Reports");

                    // Add headers
                    var headers = new[]
                    {
                        "Tare", "Net", "PauseNet", "Gross", "Target", "RecheckedWT", "Remains",
                        "StartWT", "TotalWT", "StartBags", "TotalBags", "SiloName", "EndTime",
                        "Shift", "ScaleNo", "ScaleName", "GroupName", "ScaleType", "Operator", "Spare_Ch1"
                    };

                    // Set headers
                    for (int i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = headers[i];
                        worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    }

                    // Add data
                    int row = 2;
                    foreach (var report in reportList)
                    {
                        worksheet.Cells[row, 1].Value = report.Tare;
                        worksheet.Cells[row, 2].Value = report.Net;
                        worksheet.Cells[row, 3].Value = report.PauseNet;
                        worksheet.Cells[row, 4].Value = report.Gross;
                        worksheet.Cells[row, 5].Value = report.Target;
                        worksheet.Cells[row, 6].Value = report.RecheckedWT;
                        worksheet.Cells[row, 7].Value = report.Remains;
                        worksheet.Cells[row, 8].Value = report.StartWT;
                        worksheet.Cells[row, 9].Value = report.TotalWT;
                        worksheet.Cells[row, 10].Value = report.StartBags;
                        worksheet.Cells[row, 11].Value = report.TotalBags;
                        worksheet.Cells[row, 12].Value = report.SiloName;
                        worksheet.Cells[row, 13].Value = report.EndTime;
                        worksheet.Cells[row, 14].Value = report.Shift;
                        worksheet.Cells[row, 15].Value = report.ScaleNo;
                        worksheet.Cells[row, 16].Value = report.ScaleName;
                        worksheet.Cells[row, 17].Value = report.GroupName;
                        worksheet.Cells[row, 18].Value = report.ScaleType;
                        worksheet.Cells[row, 19].Value = report.Operator;
                        worksheet.Cells[row, 20].Value = report.Spare_Ch1;
                        row++;
                    }

                    // Auto-fit columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Add borders to all cells with data
                    var dataRange = worksheet.Cells[1, 1, row - 1, headers.Length];
                    dataRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    // Save the file
                    var fileInfo = new FileInfo(filePath);
                    package.SaveAs(fileInfo);
                });

                MessageBox.Show($"Reports exported successfully to:\n{filePath}", 
                    "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
} 