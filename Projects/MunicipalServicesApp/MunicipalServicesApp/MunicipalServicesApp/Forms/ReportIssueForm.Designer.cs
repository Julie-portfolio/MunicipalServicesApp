using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp.Forms
{
    partial class ReportIssueForm
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

        private Label lblHeader;
        private Label lblLocation;
        private TextBox txtLocation;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblDescription;
        private RichTextBox rtbDescription;
        private Button btnAttach;
        private Label lblAttachedFile;
        private Button btnSubmit;
        private Button btnBack;
        private ProgressBar progressEngagement;
        private Label lblEngagement;
        private PictureBox picPreview;
        private Panel pnlCard;
        private Panel pnlFields;
        private Panel pnlContent;
        private Panel pnlLeft;
        private Panel pnlBottom;

        private void InitializeComponent()
        {
            // Initialize basic components
            this.components = new System.ComponentModel.Container();
            this.lblHeader = new Label();
            this.lblLocation = new Label();
            this.txtLocation = new TextBox();
            this.lblCategory = new Label();
            this.cmbCategory = new ComboBox();
            this.lblDescription = new Label();
            this.rtbDescription = new RichTextBox();
            this.btnAttach = new Button();
            this.lblAttachedFile = new Label();
            this.btnSubmit = new Button();
            this.btnBack = new Button();
            this.progressEngagement = new ProgressBar();
            this.lblEngagement = new Label();
            this.picPreview = new PictureBox();
            this.pnlCard = new Panel();
            this.pnlFields = new Panel();
            this.pnlBottom = new Panel();
            this.pnlContent = new Panel();
            this.pnlLeft = new Panel();

            // Form sizing (larger to avoid clipping)
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 600);
            this.AutoScroll = false;
            this.MinimumSize = new Size(800, 540);

            // pnlBottom - bottom action bar
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Height = 72;
            this.pnlBottom.Padding = new Padding(12);
            this.pnlBottom.BackColor = Color.Transparent;

            // btnBack (left)
            this.btnBack.Text = "Main Menu";
            this.btnBack.Font = new Font("Segoe UI", 10F);
            this.btnBack.Size = new Size(140, 44);
            this.btnBack.Dock = DockStyle.Left;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.BackColor = Color.FromArgb(40, 46, 54);
            this.btnBack.ForeColor = Color.White;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // btnSubmit (right)
            this.btnSubmit.Text = "Submit Report";
            this.btnSubmit.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSubmit.Size = new Size(160, 44);
            this.btnSubmit.Dock = DockStyle.Right;
            this.btnSubmit.FlatStyle = FlatStyle.Flat;
            this.btnSubmit.BackColor = Color.FromArgb(6, 193, 196);
            this.btnSubmit.ForeColor = Color.White;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

            this.pnlBottom.Controls.Add(this.btnBack);
            this.pnlBottom.Controls.Add(this.btnSubmit);

            // pnlCard - main content area above bottom bar
            this.pnlCard.Dock = DockStyle.Fill;
            this.pnlCard.Padding = new Padding(12);
            this.pnlCard.BackColor = Color.Transparent;

            // Header label (top of card) - centered bold
            this.lblHeader.Text = "Report an Issue";
            this.lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblHeader.ForeColor = Color.FromArgb(200, 230, 255);
            this.lblHeader.AutoSize = false;
            this.lblHeader.Height = 56;
            this.lblHeader.Dock = DockStyle.Top;
            this.lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            this.lblHeader.Padding = new Padding(8, 8, 8, 8);

            // progressEngagement dock at bottom of card (above pnlBottom)
            this.progressEngagement.Height = 18;
            this.progressEngagement.Dock = DockStyle.Bottom;
            this.progressEngagement.Minimum = 0;
            this.progressEngagement.Maximum = 100;

            // lblEngagement placed just above progress bar
            this.lblEngagement.Text = "Let's get started - tell us where the issue is.";
            this.lblEngagement.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblEngagement.ForeColor = Color.FromArgb(140, 220, 200);
            this.lblEngagement.AutoSize = false;
            this.lblEngagement.Height = 22;
            this.lblEngagement.Dock = DockStyle.Bottom;
            this.lblEngagement.TextAlign = ContentAlignment.MiddleLeft;

            // pnlContent contains left and right columns
            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.Padding = new Padding(8);

            // pnlLeft (preview area)
            this.pnlLeft.Dock = DockStyle.Left;
            this.pnlLeft.Width = 360;
            this.pnlLeft.Padding = new Padding(8);

            // picPreview on left, dock top
            this.picPreview.Size = new Size(320, 260);
            this.picPreview.Dock = DockStyle.Top;
            this.picPreview.BorderStyle = BorderStyle.FixedSingle;
            this.picPreview.SizeMode = PictureBoxSizeMode.Zoom;

            // lblAttachedFile under preview
            this.lblAttachedFile.Text = "No file attached.";
            this.lblAttachedFile.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblAttachedFile.ForeColor = Color.LightGray;
            this.lblAttachedFile.AutoSize = false;
            this.lblAttachedFile.Height = 20;
            this.lblAttachedFile.Dock = DockStyle.Top;
            this.lblAttachedFile.TextAlign = ContentAlignment.MiddleLeft;

            // btnAttach under label
            this.btnAttach.Text = "Attach File...";
            this.btnAttach.Font = new Font("Segoe UI", 9F);
            this.btnAttach.Size = new Size(120, 34);
            this.btnAttach.Dock = DockStyle.Top;
            this.btnAttach.FlatStyle = FlatStyle.Flat;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);

            this.pnlLeft.Controls.Add(this.btnAttach);
            this.pnlLeft.Controls.Add(this.lblAttachedFile);
            this.pnlLeft.Controls.Add(this.picPreview);

            // pnlFields (right column)
            this.pnlFields.Dock = DockStyle.Fill;
            this.pnlFields.Padding = new Padding(8);

            // Position fields relative to pnlFields
            this.lblLocation.Text = "Location:";
            this.lblLocation.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblLocation.ForeColor = Color.FromArgb(200, 230, 255);
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new Point(8, 8);

            this.txtLocation.Location = new Point(8, 32);
            this.txtLocation.Size = new Size(460, 26);
            this.txtLocation.Font = new Font("Segoe UI", 10F);

            this.lblCategory.Text = "Category:";
            this.lblCategory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCategory.ForeColor = Color.FromArgb(200, 230, 255);
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new Point(8, 68);

            this.cmbCategory.Location = new Point(8, 92);
            this.cmbCategory.Size = new Size(460, 26);
            this.cmbCategory.Font = new Font("Segoe UI", 10F);
            this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategory.Items.AddRange(new object[] { "Sanitation", "Roads and Potholes", "Water and Utilities", "Electricity", "Refuse Collection", "Parks and Public Spaces", "Other" });

            this.lblDescription.Text = "Description of the Issue:";
            this.lblDescription.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDescription.ForeColor = Color.FromArgb(200, 230, 255);
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new Point(8, 128);

            this.rtbDescription.Location = new Point(8, 152);
            this.rtbDescription.Size = new Size(460, 160);
            this.rtbDescription.Font = new Font("Segoe UI", 10F);
            // Description placeholder handled at runtime (clear on focus if placeholder text)

            // add controls to pnlFields
            this.pnlFields.Controls.Add(this.lblLocation);
            this.pnlFields.Controls.Add(this.txtLocation);
            this.pnlFields.Controls.Add(this.lblCategory);
            this.pnlFields.Controls.Add(this.cmbCategory);
            this.pnlFields.Controls.Add(this.lblDescription);
            this.pnlFields.Controls.Add(this.rtbDescription);

            // assemble content
            this.pnlContent.Controls.Add(this.pnlFields);
            this.pnlContent.Controls.Add(this.pnlLeft);

            // assemble card
            this.pnlCard.Controls.Add(this.lblHeader);
            this.pnlCard.Controls.Add(this.pnlContent);
            this.pnlCard.Controls.Add(this.lblEngagement);
            this.pnlCard.Controls.Add(this.progressEngagement);

            // ensure controls use readable neon palette
            var fg = Color.FromArgb(220, 230, 240);
            this.lblHeader.ForeColor = fg;
            this.lblAttachedFile.ForeColor = Color.LightGray;
            this.txtLocation.BackColor = Color.FromArgb(24, 30, 36);
            this.txtLocation.ForeColor = fg;
            this.txtLocation.BorderStyle = BorderStyle.FixedSingle;
            this.cmbCategory.BackColor = Color.FromArgb(24, 30, 36);
            this.cmbCategory.ForeColor = fg;
            this.rtbDescription.BackColor = Color.FromArgb(20, 26, 32);
            this.rtbDescription.ForeColor = fg;
            this.picPreview.BackColor = Color.FromArgb(12, 18, 24);

            // add bottom panel and card to form (add bottom first so docking is stable)
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlCard);

            // form chrome and appearance
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Report an Issue";
            this.BackColor = Color.FromArgb(12, 18, 28);

            // final z-order and anchors
            this.btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnSubmit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.pnlBottom.BringToFront();
            this.pnlCard.BringToFront();

            // ensure action buttons are enabled and visible
            this.btnSubmit.Visible = true;
            this.btnSubmit.Enabled = true;
            this.btnBack.Visible = true;
            this.btnBack.Enabled = true;
        }
    }
}
