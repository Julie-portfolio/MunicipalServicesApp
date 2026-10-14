using System;
using System.Linq;
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
            // NeonPainter drawing may interfere with control rendering on some setups.
            // Temporarily disable custom background painting so standard controls render normally.
            // this.Paint += (s, e) => NeonPainter.PaintBackground(e.Graphics, this.ClientRectangle);

            // (diagnostics removed) -- keep startup clean
            // reduce flicker
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint, true);

            // Ensure main menu buttons are centered and reposition on resize
            this.Resize += (s, e) => AdjustMainButtonsLayout();
            this.pnlCenter.Resize += (s, e) => AdjustMainButtonsLayout();
            AdjustMainButtonsLayout();
        }

        // LogControlTree and debug helpers removed


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
            using (var form = new LocalEventsForm())
            {
                this.Hide();
                form.ShowDialog(this);
                this.Show();
            }
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

        private void AdjustMainButtonsLayout()
        {
            try
            {
                const int btnWidth = 560;
                const int btnHeight = 64;
                int spacing = 24;

                if (pnlCenter == null || btnReportIssues == null || btnLocalEvents == null || btnServiceStatus == null)
                    return;

                int availW = Math.Max(0, pnlCenter.ClientSize.Width);
                int availH = Math.Max(0, pnlCenter.ClientSize.Height);

                // compute left to center horizontally
                int left = Math.Max(12, (availW - btnWidth) / 2);

                // compute top to center vertically the whole block of 3 buttons
                int totalH = btnHeight * 3 + spacing * 2;
                int top = Math.Max(12, (availH - totalH) / 2);

                btnReportIssues.Size = new System.Drawing.Size(btnWidth, btnHeight);
                btnReportIssues.Location = new System.Drawing.Point(left, top);
                btnReportIssues.Anchor = AnchorStyles.Top;

                btnLocalEvents.Size = new System.Drawing.Size(btnWidth, btnHeight);
                btnLocalEvents.Location = new System.Drawing.Point(left, top + btnHeight + spacing);
                btnLocalEvents.Anchor = AnchorStyles.Top;

                btnServiceStatus.Size = new System.Drawing.Size(btnWidth, btnHeight);
                btnServiceStatus.Location = new System.Drawing.Point(left, top + (btnHeight + spacing) * 2);
                btnServiceStatus.Anchor = AnchorStyles.Top;

                // ensure text colors/styles
                btnReportIssues.ForeColor = System.Drawing.Color.White;
                btnLocalEvents.ForeColor = System.Drawing.Color.White;
                btnServiceStatus.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);

                // place the 'Coming soon' label under the disabled service button
                if (lblServiceComingSoon != null)
                {
                    lblServiceComingSoon.Location = new System.Drawing.Point(btnServiceStatus.Left + 8, btnServiceStatus.Bottom + 6);
                }
            }
            catch { /* non-fatal */ }
        }

    }
}
