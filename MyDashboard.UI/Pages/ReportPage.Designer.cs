// Designer for ReportPage
namespace MyDashboard.UI.Pages
{
    partial class ReportPage
    {
        private System.Windows.Forms.Label lblReport;

        private void InitializeComponent()
        {
            this.lblReport = new System.Windows.Forms.Label();
            
            this.lblReport.Text = "Report Page - Coming Soon";
            this.lblReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            
            this.Controls.Add(this.lblReport);
        }
    }
}
