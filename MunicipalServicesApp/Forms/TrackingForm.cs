using System;
using System.Linq;
using System.Windows.Forms;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Forms
{
    public class TrackingForm : Form
    {
        private DataGridView dgv;

        public TrackingForm()
        {
            InitializeComponent();
            LoadIssues();
            IssueRepository.IssueAdded += IssueRepository_IssueAdded;
        }

        public TrackingForm(Issue highlight) : this()
        {
            if (highlight != null)
            {
                // select after form shown
                this.Shown += (s, e) => SelectIssue(highlight);
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Reported Issues";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.Columns.Add("colDate", "Date");
            dgv.Columns.Add("colCategory", "Category");
            dgv.Columns.Add("colLocation", "Location");
            dgv.Columns.Add("colDescription", "Description");
            dgv.Columns.Add("colAttachment", "Attachment");
            dgv.Columns.Add("colStatus", "Status");

            this.Controls.Add(dgv);
        }

        private void LoadIssues()
        {
            dgv.Rows.Clear();
            var items = IssueRepository.GetAll();
            foreach (var it in items)
            {
                AddIssueRow(it);
            }
        }

        private void IssueRepository_IssueAdded(Issue obj)
        {
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new Action(() => AddIssueRow(obj)));
            }
        }

        private void AddIssueRow(Issue it)
        {
            var desc = it.Description?.Length > 120 ? it.Description.Substring(0, 117) + "..." : it.Description;
            var attach = string.IsNullOrEmpty(it.AttachmentPath) ? "No" : System.IO.Path.GetFileName(it.AttachmentPath);
            dgv.Rows.Add(it.DateReported.ToString("g"), it.Category, it.Location, desc, attach, it.Status);
            dgv.Rows[dgv.Rows.Count - 1].Tag = it;
        }

        public void SelectIssue(Issue issue)
        {
            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (r.Tag is Issue i && i.DateReported == issue.DateReported)
                {
                    r.Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = r.Index;
                    break;
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            IssueRepository.IssueAdded -= IssueRepository_IssueAdded;
        }
    }
}
