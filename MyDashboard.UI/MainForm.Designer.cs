// Designer for MainForm
namespace MyDashboard.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Panel contentPanel;

        private void InitializeComponent()
        {
            this.navPanel = new System.Windows.Forms.Panel();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();

            this.navPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.navPanel.Height = 50;
            this.navPanel.Controls.Add(this.btnAlarm);
            this.navPanel.Controls.Add(this.btnReport);
            this.navPanel.Controls.Add(this.btnSetting);

            this.btnAlarm.Text = "Alarm";
            this.btnAlarm.Left = 10;
            this.btnAlarm.Click += btnAlarm_Click;

            this.btnReport.Text = "Report";
            this.btnReport.Left = 100;
            this.btnReport.Click += btnReport_Click;

            this.btnSetting.Text = "Setting";
            this.btnSetting.Left = 190;
            this.btnSetting.Click += btnSetting_Click;

            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.navPanel);
            this.Text = "My Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
    }
}
