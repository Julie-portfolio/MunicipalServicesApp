using System.Drawing;
using System.Windows.Forms;
using MunicipalServicesApp.Forms.Controls;

namespace MunicipalServicesApp.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Label lblTitle;
        private Label lblSubtitle;
        private GlossyButton btnReportIssues;
        private GlossyButton btnLocalEvents;
        private GlossyButton btnServiceStatus;
        private Panel pnlHeader;
        private Panel pnlCenter;
        private GlossyButton btnExit;
        private Panel pnlBottom;
        private Label lblFooter;

        private Label lblServiceComingSoon;

        private void InitializeComponent()
        {
            this.pnlHeader = new Panel();
            this.pnlCenter = new Panel();
            // removed btnThemeToggle (theme toggle moved or removed to simplify header)
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.btnReportIssues = new GlossyButton();
            this.btnLocalEvents = new GlossyButton();
            this.btnServiceStatus = new GlossyButton();
            this.btnExit = new GlossyButton();
            this.pnlBottom = new Panel();
            this.lblFooter = new Label();
            this.lblServiceComingSoon = new Label();

            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = Color.FromArgb(20, 24, 28);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 96;
            // header title
            this.lblTitle.Text = "Municipal Services Portal";
            this.lblTitle.ForeColor = Color.FromArgb(6, 193, 196);
            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(300, 24);

            this.lblSubtitle.Text = "";
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new Point(0, 0);

            this.pnlHeader.Controls.Add(this.lblTitle);

            // center panel containing the large glossy buttons (fill remaining space)
            this.pnlCenter.Dock = DockStyle.Fill;
            this.pnlCenter.Padding = new Padding(40, 14, 40, 14);
            // Use explicit background that matches dark theme center panel
            this.pnlCenter.BackColor = Color.FromArgb(32, 36, 40);

            // large primary Report Issues button
            this.btnReportIssues.Text = "  Report Issues";
            this.btnReportIssues.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnReportIssues.Size = new Size(640, 80);
            this.btnReportIssues.Location = new Point(40, 8);
            this.btnReportIssues.Anchor = AnchorStyles.Top;
            this.btnReportIssues.StartColor = Color.FromArgb(6, 193, 196);
            this.btnReportIssues.EndColor = Color.FromArgb(2, 132, 165);
            this.btnReportIssues.CornerRadius = 12;
            this.btnReportIssues.Click += new System.EventHandler(this.btnReportIssues_Click);

            // Local Events button (ENABLED now)
            this.btnLocalEvents.Text = "  Local Events && Announcements";
            this.btnLocalEvents.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.btnLocalEvents.Size = new Size(640, 64);
            this.btnLocalEvents.Location = new Point(40, 104);
            this.btnLocalEvents.Anchor = AnchorStyles.Top;
            this.btnLocalEvents.StartColor = Color.FromArgb(6, 193, 196);
            this.btnLocalEvents.EndColor = Color.FromArgb(2, 132, 165);
            this.btnLocalEvents.ForeColor = Color.White;
            this.btnLocalEvents.Enabled = true;
            this.btnLocalEvents.Click += new System.EventHandler(this.btnLocalEvents_Click);

            // Service Request Status (DISABLED, coming soon)
            this.btnServiceStatus.Text = "  Service Request Status";
            this.btnServiceStatus.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.btnServiceStatus.Size = new Size(640, 64);
            this.btnServiceStatus.Location = new Point(40, 180);
            this.btnServiceStatus.Anchor = AnchorStyles.Top;
            this.btnServiceStatus.StartColor = Color.FromArgb(64, 64, 64);
            this.btnServiceStatus.EndColor = Color.FromArgb(48, 48, 48);
            this.btnServiceStatus.ForeColor = Color.FromArgb(120, 120, 120);
            this.btnServiceStatus.Enabled = false;

            // coming soon label under disabled button
            this.lblServiceComingSoon.Text = "Coming soon";
            this.lblServiceComingSoon.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblServiceComingSoon.ForeColor = Color.FromArgb(160, 160, 160);
            this.lblServiceComingSoon.AutoSize = true;
            this.lblServiceComingSoon.Location = new Point(48, 252);

            // assemble center
            this.pnlCenter.Controls.Add(this.btnReportIssues);
            this.pnlCenter.Controls.Add(this.btnLocalEvents);
            this.pnlCenter.Controls.Add(this.btnServiceStatus);
            this.pnlCenter.Controls.Add(this.lblServiceComingSoon);

            // bottom panel
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Height = 72;
            this.pnlBottom.Padding = new Padding(18);
            this.pnlBottom.BackColor = Color.FromArgb(245, 246, 248);

            // position buttons inside bottom panel
            this.pnlBottom.BackColor = Color.FromArgb(20, 24, 28);
            this.btnExit.Text = "Exit";
            this.btnExit.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.btnExit.Size = new Size(120, 44);
            this.btnExit.StartColor = Color.FromArgb(90, 96, 102);
            this.btnExit.EndColor = Color.FromArgb(70, 76, 82);
            this.btnExit.CornerRadius = 8;
            this.btnExit.Click += new System.EventHandler((s, e) => this.Close());
            this.btnExit.Location = new Point(18, 14);
            this.btnExit.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

            // only Exit and footer are shown in bottom panel
            this.pnlBottom.Controls.Add(this.btnExit);
            this.pnlBottom.Controls.Add(this.lblFooter);

            // footer paragraph label
            this.lblFooter.Text = "Empowering communities through technology.";
            this.lblFooter.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblFooter.ForeColor = Color.FromArgb(110, 110, 110);
            this.lblFooter.AutoSize = true;
            this.lblFooter.Location = new Point(260, 20);

            // MainForm
            this.ClientSize = new Size(940, 520);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true; // allow scrolling on small displays
            this.MinimumSize = new Size(760, 420);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Municipal Services Portal";
            this.BackColor = Color.FromArgb(245, 246, 248);

            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);

            // bring bottom buttons to front so they're not obscured
            this.pnlBottom.BringToFront();
        }
    }
}
