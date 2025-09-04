// Designer for FilterBar
namespace MyDashboard.UI.Controls
{
    partial class FilterBar
    {
        private System.Windows.Forms.ComboBox cboLine;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;

        private void InitializeComponent()
        {
            this.cboLine = new System.Windows.Forms.ComboBox();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();

            this.cboLine.Items.AddRange(new object[] { "All", "Line1", "Line2" });
            this.cboLine.Left = 10;

            this.dtFrom.Left = 120;
            this.dtTo.Left = 280;

            this.txtSearch.Left = 440;
            this.txtSearch.Width = 150;

            this.btnSearch.Text = "Search";
            this.btnSearch.Left = 600;
            this.btnSearch.Click += btnSearch_Click;

            this.Controls.Add(this.cboLine);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);

            this.Height = 30;
            this.Dock = System.Windows.Forms.DockStyle.Top;
        }
    }
}
