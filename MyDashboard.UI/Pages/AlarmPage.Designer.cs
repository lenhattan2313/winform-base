// Designer for AlarmPage
namespace MyDashboard.UI.Pages
{
    partial class AlarmPage
    {
        private FilterBar filterBar;
        private System.Windows.Forms.DataGridView dataGridView1;

        private void InitializeComponent()
        {
            this.filterBar = new MyDashboard.UI.Controls.FilterBar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();

            this.filterBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;

            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.filterBar);
        }
    }
}
