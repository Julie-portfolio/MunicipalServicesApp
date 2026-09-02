using System;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // Apply current theme
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            // Use NeonPainter to draw background without changing base class
            this.Paint += (s, e) => NeonPainter.PaintBackground(e.Graphics, this.ClientRectangle);
            // reduce flicker
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint, true);
        }


        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            using (var reportForm = new ReportIssueForm())
            {
                this.Hide();
                reportForm.ShowDialog();
                this.Show();
            }
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            // To be implemented in Part 2
            MessageBox.Show("Local Events and Announcements will be available in a future update.",
                "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            // To be implemented in the PoE (Part 3)
            MessageBox.Show("Service Request Status will be available in a future update.",
                "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            // Toggle theme and re-apply to this form
            ThemeManager.ToggleTheme(this);
        }

    }
}
