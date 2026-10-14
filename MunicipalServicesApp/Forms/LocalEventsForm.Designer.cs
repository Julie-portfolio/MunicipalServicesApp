using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms
{
    partial class LocalEventsForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbCategory;
        private DateTimePicker dtFrom;
        private DateTimePicker dtTo;
        private CheckBox chkFrom;
        private CheckBox chkTo;
        private Button btnSearch;
        private Button btnRecommend;
        private Button btnClear;
        private TextBox txtSearch;
        private DataGridView dataGridViewEvents;
        private ListBox listBoxRecommendations;
        private ListBox listBoxAnnouncements;
        private Button btnRefreshAnnouncements;
        private Button btnBackToMain;
        private Label lblNoEvents;
        private Label lblHeader;
        private Label lblRecommended;
        private Label lblAnnouncements;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbCategory = new ComboBox();
            this.dtFrom = new DateTimePicker();
            this.dtTo = new DateTimePicker();
            this.chkFrom = new CheckBox();
            this.chkTo = new CheckBox();
            this.btnSearch = new Button();
            this.btnRecommend = new Button();
            this.txtSearch = new TextBox();
            this.dataGridViewEvents = new DataGridView();
            this.listBoxRecommendations = new ListBox();
            this.listBoxAnnouncements = new ListBox();
            this.btnRefreshAnnouncements = new Button();
            this.btnBackToMain = new Button();
            this.lblNoEvents = new Label();
            this.lblHeader = new Label();
            this.lblRecommended = new Label();
            this.lblAnnouncements = new Label();
            // Form sizing
            this.ClientSize = new Size(1100, 700);
            this.MinimumSize = new Size(1100, 700);
            this.Text = "Local Events & Announcements";
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(32, 36, 40);

            // padding and layout constants
            int pad = 24;
            int rightColumnWidth = 320;
            int gap = 16;

            // Header
            this.lblHeader.Text = "Local Events & Announcements";
            this.lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblHeader.ForeColor = Color.FromArgb(6, 193, 196);
            this.lblHeader.AutoSize = false;
            this.lblHeader.UseMnemonic = false;
            this.lblHeader.Height = 48;
            this.lblHeader.Width = this.ClientSize.Width - pad * 2;
            this.lblHeader.Location = new Point(pad, pad - 4);
            this.lblHeader.TextAlign = ContentAlignment.MiddleLeft;

            // Search / filter row under header
            int searchTop = this.lblHeader.Bottom + 12;
            // first row: txtSearch, cmbCategory, Search, Recommend
            this.txtSearch.Location = new Point(pad, searchTop);
            this.txtSearch.Size = new Size(260, 30);
            // PlaceholderText not available on older .NET Framework TextBox; use initial text instead
            this.txtSearch.Text = "";
            // Typography: input font
            this.txtSearch.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            this.cmbCategory.Location = new Point(this.txtSearch.Right + 12, searchTop);
            this.cmbCategory.Size = new Size(180, 30);
            this.cmbCategory.FlatStyle = FlatStyle.Flat;
            this.cmbCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // If space is tight, From/To controls are placed on a second row
            int secondRowTop = searchTop + 40;
            this.chkFrom.Location = new Point(pad, secondRowTop + 4);
            this.chkFrom.Text = "From";
            this.chkFrom.ForeColor = Color.White;
            this.chkFrom.Size = new Size(48, 24);

            this.dtFrom.Location = new Point(this.chkFrom.Right + 8, secondRowTop);
            this.dtFrom.Size = new Size(190, 30);
            this.dtFrom.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            this.chkTo.Location = new Point(this.dtFrom.Right + 12, secondRowTop + 4);
            this.chkTo.Text = "To";
            this.chkTo.ForeColor = Color.White;
            this.chkTo.Size = new Size(48, 24);

            this.dtTo.Location = new Point(this.chkTo.Right + 8, secondRowTop);
            this.dtTo.Size = new Size(190, 30);
            this.dtTo.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            this.btnClear = new Button();
            this.btnClear.Text = "Clear";
            this.btnClear.Size = new Size(100, 30);
            this.btnClear.Location = new Point(this.dtTo.Right + 12, secondRowTop);
            this.btnClear.BackColor = Color.FromArgb(80, 88, 96);
            this.btnClear.ForeColor = Color.White;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            this.btnClear.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            this.btnSearch.Text = "Search";
            this.btnSearch.Size = new Size(110, 30);
            this.btnSearch.Location = new Point(this.cmbCategory.Right + 12, searchTop);
            this.btnSearch.BackColor = Color.FromArgb(6, 193, 196);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            this.btnSearch.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            this.btnRecommend.Text = "Recommend";
            this.btnRecommend.Size = new Size(120, 30);
            this.btnRecommend.Location = new Point(this.btnSearch.Right + 12, searchTop);
            this.btnRecommend.BackColor = Color.FromArgb(80, 88, 96);
            this.btnRecommend.ForeColor = Color.White;
            this.btnRecommend.FlatStyle = FlatStyle.Flat;
            this.btnRecommend.Click += new System.EventHandler(this.BtnRecommend_Click);
            this.btnRecommend.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            // Right column labels and lists
            int rightX = this.ClientSize.Width - pad - rightColumnWidth;
            int listsTop = this.txtSearch.Bottom + 18;

            this.lblRecommended.Text = "Recommended for you";
            this.lblRecommended.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblRecommended.ForeColor = Color.FromArgb(200, 200, 200);
            this.lblRecommended.AutoSize = false;
            this.lblRecommended.Height = 22;
            this.lblRecommended.Width = rightColumnWidth;
            this.lblRecommended.Location = new Point(rightX, listsTop);

            this.listBoxRecommendations.Location = new Point(rightX, this.lblRecommended.Bottom + 6);
            this.listBoxRecommendations.Size = new Size(rightColumnWidth, 240);
            this.listBoxRecommendations.BackColor = Color.FromArgb(24, 28, 32);
            this.listBoxRecommendations.ForeColor = Color.White;
            this.listBoxRecommendations.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // Announcements list
            this.listBoxAnnouncements.Location = new Point(rightX, this.listBoxRecommendations.Bottom + 18);
            this.listBoxAnnouncements.Size = new Size(rightColumnWidth, 196);
            this.listBoxAnnouncements.BackColor = Color.FromArgb(24, 28, 32);
            this.listBoxAnnouncements.ForeColor = Color.White;
            this.listBoxAnnouncements.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // label above recommendations (redundant with lblRecommended but ensure presence)
            this.lblRecommended.Text = "Recommended for you";

            this.lblAnnouncements.Text = "Announcements";
            this.lblAnnouncements.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblAnnouncements.ForeColor = Color.FromArgb(200, 200, 200);
            this.lblAnnouncements.AutoSize = false;
            this.lblAnnouncements.Height = 22;
            this.lblAnnouncements.Width = rightColumnWidth;
            this.lblAnnouncements.Location = new Point(rightX, this.listBoxRecommendations.Bottom + 12);

            // place Refresh at bottom-right inside client area
            this.btnRefreshAnnouncements.Text = "Refresh Announcements";
            this.btnRefreshAnnouncements.Size = new Size(rightColumnWidth, 32);
            this.btnRefreshAnnouncements.BackColor = Color.FromArgb(6, 193, 196);
            this.btnRefreshAnnouncements.ForeColor = Color.White;
            this.btnRefreshAnnouncements.FlatStyle = FlatStyle.Flat;
            this.btnRefreshAnnouncements.Location = new Point(this.ClientSize.Width - pad - this.btnRefreshAnnouncements.Width, this.ClientSize.Height - pad - this.btnRefreshAnnouncements.Height);
            this.btnRefreshAnnouncements.Click += new System.EventHandler(this.BtnRefreshAnnouncements_Click);
            this.btnRefreshAnnouncements.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            // Back button bottom-left
            this.btnBackToMain.Size = new Size(160, 36);
            this.btnBackToMain.Location = new Point(pad, this.ClientSize.Height - pad - this.btnBackToMain.Height);
            this.btnBackToMain.Text = "Back";
            this.btnBackToMain.Click += new System.EventHandler(this.BtnBackToMain_Click);
            this.btnBackToMain.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            // No-events label
            this.lblNoEvents.Text = "No events found";
            this.lblNoEvents.ForeColor = Color.White;
            this.lblNoEvents.Location = new Point(pad + 12, this.txtSearch.Bottom + 60);
            this.lblNoEvents.AutoSize = true;
            this.lblNoEvents.Visible = false;
            this.lblNoEvents.Font = new Font("Segoe UI", 9F, FontStyle.Italic);

            // add controls to form
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.chkFrom);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.chkTo);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRecommend);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.listBoxRecommendations);
            this.Controls.Add(this.lblRecommended);
            this.Controls.Add(this.lblAnnouncements);
            this.Controls.Add(this.listBoxAnnouncements);
            this.Controls.Add(this.btnRefreshAnnouncements);
            this.Controls.Add(this.dataGridViewEvents);
            this.Controls.Add(this.btnBackToMain);
            this.Controls.Add(this.lblNoEvents);
            this.listBoxAnnouncements.Size = new Size(rightColumnWidth, 196);
            this.listBoxAnnouncements.BackColor = Color.FromArgb(24, 28, 32);
            this.listBoxAnnouncements.ForeColor = Color.White;

            this.btnRefreshAnnouncements.Text = "Refresh Announcements";
            this.btnRefreshAnnouncements.Size = new Size(rightColumnWidth, 32);
            this.btnRefreshAnnouncements.BackColor = Color.FromArgb(6, 193, 196);
            this.btnRefreshAnnouncements.ForeColor = Color.White;
            this.btnRefreshAnnouncements.FlatStyle = FlatStyle.Flat;
            // place Refresh at bottom-right inside client area
            this.btnRefreshAnnouncements.Location = new Point(this.ClientSize.Width - pad - this.btnRefreshAnnouncements.Width, this.ClientSize.Height - pad - this.btnRefreshAnnouncements.Height);
            this.btnRefreshAnnouncements.Click += new System.EventHandler(this.BtnRefreshAnnouncements_Click);

            // No-results label inside grid area (hidden by default)
            this.lblNoEvents = new Label();
            this.lblNoEvents.Text = "No events found";
            this.lblNoEvents.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            this.lblNoEvents.ForeColor = Color.FromArgb(160, 160, 160);
            this.lblNoEvents.AutoSize = true;
            // position; will be adjusted after grid size is computed
            this.lblNoEvents.Location = new Point(pad + 12, this.txtSearch.Bottom + 60);
            this.lblNoEvents.Visible = false;

            // Anchoring so controls resize sensibly
            this.dataGridViewEvents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.listBoxRecommendations.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.listBoxAnnouncements.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnRefreshAnnouncements.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.cmbCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.dtFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.dtTo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnRecommend.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Events grid on the left, occupying remaining vertical space
            int leftX = pad;
            int leftWidth = rightX - gap - leftX;
            int gridTop = this.dtFrom.Bottom + 18;
            // if dtFrom is on second row, ensure top computed accordingly
            if (this.dtFrom.Top < this.txtSearch.Bottom) gridTop = this.txtSearch.Bottom + 18;
            int gridHeight = this.ClientSize.Height - gridTop - pad - 80; // leave room for back button

            this.dataGridViewEvents.Location = new Point(leftX, gridTop);
            this.dataGridViewEvents.Size = new Size(leftWidth, gridHeight);
            this.dataGridViewEvents.ReadOnly = true;
            this.dataGridViewEvents.AllowUserToAddRows = false; // prevent blank trailing row
            this.dataGridViewEvents.AllowUserToDeleteRows = false;
            this.dataGridViewEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEvents.BackgroundColor = Color.FromArgb(24, 28, 32);
            this.dataGridViewEvents.GridColor = Color.FromArgb(48, 52, 56);
            this.dataGridViewEvents.RowHeadersVisible = false;
            this.dataGridViewEvents.ForeColor = Color.White;
            this.dataGridViewEvents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 36, 40);
            this.dataGridViewEvents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewEvents.EnableHeadersVisualStyles = false;

            // Back button bottom-left
            this.btnBackToMain.Size = new Size(160, 36);
            this.btnBackToMain.Text = "Back to Main Menu";
            this.btnBackToMain.Location = new Point(pad, this.ClientSize.Height - pad - this.btnBackToMain.Height);
            this.btnBackToMain.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnBackToMain.BackColor = Color.FromArgb(40, 46, 54);
            this.btnBackToMain.ForeColor = Color.White;
            this.btnBackToMain.FlatStyle = FlatStyle.Flat;
            this.btnBackToMain.Click += new System.EventHandler(this.BtnBackToMain_Click);

            // form load
            this.Load += new System.EventHandler(this.LocalEventsForm_Load);

            // Anchoring so controls resize sensibly
            this.dataGridViewEvents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.listBoxRecommendations.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.listBoxAnnouncements.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRefreshAnnouncements.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.cmbCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.dtFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.dtTo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnRecommend.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Add controls in logical order
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.chkFrom);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.chkTo);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRecommend);
            this.Controls.Add(this.dataGridViewEvents);
            this.Controls.Add(this.lblRecommended);
            this.Controls.Add(this.listBoxRecommendations);
            this.Controls.Add(this.lblAnnouncements);
            this.Controls.Add(this.listBoxAnnouncements);
            this.Controls.Add(this.btnRefreshAnnouncements);
            this.Controls.Add(this.btnBackToMain);
        }
    }
}
