using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Forms
{
    public partial class ReportIssueForm : Form
    {
        private string _attachedFilePath = string.Empty;

        // P/Invoke for setting cue banner (placeholder) on TextBox
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public ReportIssueForm()
        {
            InitializeComponent();
            // Apply app theme and initialize dynamic engagement
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            // draw neon background using NeonPainter
            this.Paint += (s, e) => NeonPainter.PaintBackground(e.Graphics, this.ClientRectangle);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint, true);
            WireEngagementEvents();

            // Set placeholder for location textbox
            SetPlaceholderText(this.txtLocation, "Enter location...");

            // Setup RichTextBox placeholder behavior
            this.rtbDescription.Text = "Describe the issue...";
            this.rtbDescription.ForeColor = Color.Gray;
            this.rtbDescription.GotFocus += RtbDescription_GotFocus;
            this.rtbDescription.LostFocus += RtbDescription_LostFocus;

            // Apply rounded corners to submit button and hover effects
            ApplyRoundedStyle(this.btnSubmit, 10, Color.FromArgb(6, 193, 196));
            ApplyRoundedStyle(this.btnBack, 6, Color.FromArgb(40, 46, 54));

            // Ensure btnSubmit tooltip and accessibility
            var tt = new ToolTip();
            tt.SetToolTip(this.btnSubmit, "Submit the report");
            tt.SetToolTip(this.btnBack, "Return to main menu");

            // Initialize combo selection if not set
            if (this.cmbCategory.Items.Count > 0 && this.cmbCategory.SelectedIndex == -1)
            {
                this.cmbCategory.SelectedIndex = 0;
            }

            UpdateEngagement();
            UpdateProgress();
        }

        private void WireEngagementEvents()
        {
            txtLocation.TextChanged += (s, e) => UpdateEngagement();
            cmbCategory.SelectedIndexChanged += (s, e) => UpdateEngagement();
            rtbDescription.TextChanged += (s, e) => UpdateEngagement();
        }

        /// <summary>
        /// Dynamic engagement feature: a progress bar and encouraging label
        /// that fill in as the resident completes the report, rewarding
        /// participation and showing how close the report is to being ready.
        /// </summary>
        private void UpdateEngagement()
        {
            int filled = 0;
            int total = 4;

            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) filled++;
            if (cmbCategory.SelectedIndex != -1) filled++;
            if (!string.IsNullOrWhiteSpace(rtbDescription.Text)) filled++;
            if (!string.IsNullOrEmpty(_attachedFilePath)) filled++;

            int percent = (int)((filled / (double)total) * 100);
            progressEngagement.Value = percent;

            if (percent == 0)
                lblEngagement.Text = "Let's get started - tell us where the issue is.";
            else if (percent < 50)
                lblEngagement.Text = "Great start! Keep going, every detail helps.";
            else if (percent < 100)
                lblEngagement.Text = "Almost there - you're helping improve your community!";
            else
                lblEngagement.Text = "All set! Thank you for making your voice heard. Click Submit.";
        }

        private void SetPlaceholderText(TextBox tb, string placeholder)
        {
            if (tb == null) return;
            // EM_SETCUEBANNER wParam: show when focused? use false (0) to show always
            SendMessage(tb.Handle, EM_SETCUEBANNER, (IntPtr)0, placeholder);
        }

        private void RtbDescription_GotFocus(object sender, EventArgs e)
        {
            if (this.rtbDescription.Text == "Describe the issue...")
            {
                this.rtbDescription.Text = string.Empty;
                this.rtbDescription.ForeColor = Color.White;
            }
        }

        private void RtbDescription_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.rtbDescription.Text))
            {
                this.rtbDescription.Text = "Describe the issue...";
                this.rtbDescription.ForeColor = Color.Gray;
            }
        }

        private void UpdateProgress()
        {
            int score = 0;
            if (!string.IsNullOrWhiteSpace(this.txtLocation.Text)) score += 33;
            if (this.cmbCategory.SelectedIndex >= 0) score += 33;
            if (!string.IsNullOrWhiteSpace(this.rtbDescription.Text) && this.rtbDescription.Text != "Describe the issue...") score += 34;

            this.progressEngagement.Value = Math.Min(Math.Max(score, 0), 100);
            this.lblEngagement.Text = score >= 100 ? "Your report helps build the future!" : "Your report helps build the future!";
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Attach a photo or document related to the issue";
                dialog.Filter = "Images and Documents (*.jpg;*.jpeg;*.png;*.pdf;*.docx)|*.jpg;*.jpeg;*.png;*.pdf;*.docx|All files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _attachedFilePath = dialog.FileName;
                    lblAttachedFile.Text = "Attached: " + System.IO.Path.GetFileName(_attachedFilePath);
                    UpdateEngagement();
                    // Try to show image preview when an image is selected
                    try
                    {
                        if (!string.IsNullOrEmpty(_attachedFilePath) && System.IO.File.Exists(_attachedFilePath))
                        {
                            var ext = System.IO.Path.GetExtension(_attachedFilePath).ToLowerInvariant();
                            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".gif")
                            {
                                using (var img = System.Drawing.Image.FromFile(_attachedFilePath))
                                {
                                    picPreview.Image = new System.Drawing.Bitmap(img);
                                }
                            }
                        }
                    }
                    catch { /* ignore preview errors */ }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter the location of the issue.", "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Focus();
                return;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category for the issue.", "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                MessageBox.Show("Please provide a description of the issue.", "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtbDescription.Focus();
                return;
            }

            var issue = new Issue(
                txtLocation.Text.Trim(),
                cmbCategory.SelectedItem.ToString(),
                rtbDescription.Text.Trim(),
                _attachedFilePath
            );

            IssueRepository.Add(issue);

            MessageBox.Show(
                "Thank you! Your issue has been reported successfully and is now on record.\n\n" +
                "Reference: " + issue.DateReported.ToString("yyyyMMddHHmmss"),
                "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Show tracking so user can see where the report goes
            try
            {
                using (var tf = new TrackingForm(issue))
                {
                    tf.ShowDialog(this);
                }
            }
            catch { }

            ClearForm();
        }

        private void ClearForm()
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Clear();
            _attachedFilePath = string.Empty;
            lblAttachedFile.Text = "No file attached.";
            picPreview.Image = null;
            UpdateEngagement();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ApplyRoundedStyle(Button btn, int radius, Color baseColor)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = baseColor;
            btn.ForeColor = Color.White;

            // create rounded region
            var rect = new Rectangle(0, 0, btn.Width, btn.Height);
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int r = radius;
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseAllFigures();

            btn.Region = new Region(path);

            // hover effect
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(baseColor);
            btn.MouseLeave += (s, e) => btn.BackColor = baseColor;
        }
    }
}
