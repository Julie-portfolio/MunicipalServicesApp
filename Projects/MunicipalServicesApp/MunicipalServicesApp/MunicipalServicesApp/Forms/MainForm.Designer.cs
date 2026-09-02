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
        private GlossyButton btnSubmitReport;
        private Panel pnlBottom;
        private Label lblFooter;
        private Button btnThemeToggle;

        private void InitializeComponent()
        {
            this.pnlHeader = new Panel();
            this.pnlCenter = new Panel();
            this.btnThemeToggle = new Button();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.btnReportIssues = new GlossyButton();
            this.btnLocalEvents = new GlossyButton();
            this.btnServiceStatus = new GlossyButton();
            this.btnExit = new GlossyButton();
            this.btnSubmitReport = new GlossyButton();
            this.pnlBottom = new Panel();
            this.lblFooter = new Label();

            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = Color.FromArgb(240, 240, 242);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Height = 96;

            // header title
            this.lblTitle.Text = "Municipal Services Portal";
            this.lblTitle.ForeColor = Color.FromArgb(12, 92, 120);
            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(300, 24);

            this.lblSubtitle.Text = "";
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new Point(0, 0);

            // Theme button (small) on header
            this.btnThemeToggle.Text = "";
            this.btnThemeToggle.Size = new Size(44, 28);
            this.btnThemeToggle.Location = new Point(860, 34);
            this.btnThemeToggle.FlatStyle = FlatStyle.Flat;
            this.btnThemeToggle.FlatAppearance.BorderSize = 0;
            this.btnThemeToggle.BackColor = Color.Transparent;
            this.btnThemeToggle.Cursor = Cursors.Hand;
            this.btnThemeToggle.Click += new System.EventHandler(this.btnThemeToggle_Click);

            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnThemeToggle);

            // center panel containing the large glossy buttons (fill remaining space)
            this.pnlCenter.Dock = DockStyle.Fill;
            this.pnlCenter.Padding = new Padding(120, 14, 120, 14);
            this.pnlCenter.BackColor = Color.Transparent;

            // large primary Report Issues button
            this.btnReportIssues.Text = "  Report Issues";
            this.btnReportIssues.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.btnReportIssues.Size = new Size(640, 80);
            this.btnReportIssues.Location = new Point(30, 8);
            this.btnReportIssues.StartColor = Color.FromArgb(6, 193, 196);
            this.btnReportIssues.EndColor = Color.FromArgb(2, 132, 165);
            this.btnReportIssues.CornerRadius = 12;
            this.btnReportIssues.Click += new System.EventHandler(this.btnReportIssues_Click);

            // secondary buttons (disabled look)
            this.btnLocalEvents.Text = "  Local Events & Announcements";
            this.btnLocalEvents.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.btnLocalEvents.Size = new Size(640, 64);
            this.btnLocalEvents.Location = new Point(30, 104);
            this.btnLocalEvents.StartColor = Color.FromArgb(235, 238, 240);
            this.btnLocalEvents.EndColor = Color.FromArgb(220, 224, 227);
            this.btnLocalEvents.ForeColor = Color.FromArgb(96, 96, 96);
            this.btnLocalEvents.Enabled = false;

            this.btnServiceStatus.Text = "  Service Request Status";
            this.btnServiceStatus.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.btnServiceStatus.Size = new Size(640, 64);
            this.btnServiceStatus.Location = new Point(30, 180);
            this.btnServiceStatus.StartColor = Color.FromArgb(235, 238, 240);
            this.btnServiceStatus.EndColor = Color.FromArgb(220, 224, 227);
            this.btnServiceStatus.ForeColor = Color.FromArgb(96, 96, 96);
            this.btnServiceStatus.Enabled = false;

            // footer paragraph label
            this.lblFooter.Text = "Empowering communities through technology.";
            this.lblFooter.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            this.lblFooter.ForeColor = Color.FromArgb(110, 110, 110);
            this.lblFooter.AutoSize = true;
            this.lblFooter.Location = new Point(260, 230 + 120);

            // bottom actions (moved into pnlBottom)
            this.btnExit.Text = "\u2190  Exit";
            this.btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.btnExit.Size = new Size(120, 44);
            this.btnExit.StartColor = Color.FromArgb(90, 96, 102);
            this.btnExit.EndColor = Color.FromArgb(70, 76, 82);
            this.btnExit.CornerRadius = 8;
            this.btnExit.Click += new System.EventHandler((s, e) => this.Close());

            // ensure exit anchors to bottom-left so it remains visible on resize/DPI
            this.btnExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            this.btnSubmitReport.Text = "  \u2708  Submit Report";
            this.btnSubmitReport.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSubmitReport.Size = new Size(200, 48);
            this.btnSubmitReport.StartColor = Color.FromArgb(6, 193, 196);
            this.btnSubmitReport.EndColor = Color.FromArgb(2, 132, 165);
            this.btnSubmitReport.CornerRadius = 10;
            this.btnSubmitReport.Click += new System.EventHandler(this.btnReportIssues_Click);

            // anchor submit to bottom-right so it stays visible
            this.btnSubmitReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // assemble center
            this.pnlCenter.Controls.Add(this.btnReportIssues);
            this.pnlCenter.Controls.Add(this.btnLocalEvents);
            this.pnlCenter.Controls.Add(this.btnServiceStatus);

            // bottom panel
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Height = 72;
            this.pnlBottom.Padding = new Padding(18);
            this.pnlBottom.BackColor = Color.Transparent;

            // position buttons inside bottom panel
            this.btnExit.Location = new Point(18, 14);
            this.btnExit.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnSubmitReport.Size = new Size(200, 48);
            this.btnSubmitReport.Location = new Point(this.ClientSize.Width - this.btnSubmitReport.Width - 36, 12);
            this.btnSubmitReport.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;

            this.pnlBottom.Controls.Add(this.btnExit);
            this.pnlBottom.Controls.Add(this.btnSubmitReport);

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
            this.Controls.Add(this.lblFooter);

            // bring bottom buttons to front so they're not obscured
            this.pnlBottom.BringToFront();
        }
    }
}
