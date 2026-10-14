using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.Services;

namespace MunicipalServicesApp.Forms
{
    public partial class LocalEventsForm : Form
    {
        private readonly EventManager _eventManager = new EventManager();
        private readonly RecommendationService _rec;
        private readonly AnnouncementManager _announcements;

        public LocalEventsForm()
        {
            InitializeComponent();
            _rec = new RecommendationService(_eventManager);
            _announcements = new AnnouncementManager(_eventManager);

            // populate categories and defaults are set in LocalEventsForm_Load
        }

        private void LocalEventsForm_Load(object sender, EventArgs e)
        {
            try
            {
                _eventManager.SeedSample();
                _announcements.SeedAnnouncementsFromEvents();

                // populate categories
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("All categories");
                cmbCategory.Items.AddRange(_eventManager.GetCategories().ToArray());
                cmbCategory.SelectedIndex = 0;

                // sensible date defaults
                dtFrom.Value = DateTime.Today;
                dtTo.Value = DateTime.Today.AddDays(60);
                chkFrom.Checked = false;
                chkTo.Checked = false;

                // initial load
                dataGridViewEvents.DataSource = _rec.ToDataTable(_eventManager.GetAllEvents());
                RefreshAnnouncements();
                listBoxRecommendations.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing Local Events: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllEvents()
        {
            var all = _eventManager.GetAllEvents()
                .Where(e => e.EventDate.Date >= DateTime.Today.AddDays(-30) && e.EventDate.Date <= DateTime.Today.AddYears(1))
                .ToList();
            dataGridViewEvents.DataSource = _rec.ToDataTable(all);
            // hide no-results label
            if (this.Controls.ContainsKey("lblNoEvents"))
            {
                var lbl = this.Controls["lblNoEvents"] as Label;
                if (lbl != null) lbl.Visible = false;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string cat = null;
            if (cmbCategory.SelectedItem != null)
            {
                var s = cmbCategory.SelectedItem as string;
                if (!string.Equals(s, "All categories", StringComparison.OrdinalIgnoreCase)) cat = s;
            }
            DateTime? from = chkFrom.Checked ? dtFrom.Value.Date : (DateTime?)null;
            DateTime? to = chkTo.Checked ? dtTo.Value.Date : (DateTime?)null;

            // validate date range if both provided
            if (from.HasValue && to.HasValue && from.Value > to.Value)
            {
                MessageBox.Show("From date must be before To date", "Invalid dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var results = _eventManager.Search(cat, txtSearch.Text, from, to);

            try
            {
                _rec.RecordSearch(cat, txtSearch.Text);
            }
            catch { }

            dataGridViewEvents.DataSource = _rec.ToDataTable(results ?? new System.Collections.Generic.List<Models.EventItem>());

            // show no-results label when grid is empty
            var noEventsLabel = this.Controls.ContainsKey("lblNoEvents") ? this.Controls["lblNoEvents"] as Label : null;
            if (noEventsLabel != null)
            {
                noEventsLabel.Visible = (results == null || results.Count == 0);
            }

            // refresh recommendations excluding currently shown events
            try
            {
                var recs = _eventManager.GetRecommendations(results, 5);
                if (recs == null || recs.Count == 0)
                {
                    listBoxRecommendations.DataSource = new System.Collections.Generic.List<string> { "Search first to get recommendations" };
                }
                else
                {
                    var reason = _eventManager.GetRecommendationReason();
                    var recStrings = recs.Select(r => string.Format("{0}: {1} ({2:dd MMM})", reason, r.Title, r.EventDate)).ToList();
                    listBoxRecommendations.DataSource = recStrings;
                }
            }
            catch
            {
                listBoxRecommendations.DataSource = new System.Collections.Generic.List<string> { "Search first to get recommendations" };
            }
        }

        private void BtnRecommend_Click(object sender, EventArgs e)
        {
            // Recommend based on the most-searched category and exclude currently shown grid items
            var shown = new System.Collections.Generic.List<Models.EventItem>();
            var ds = dataGridViewEvents.DataSource as DataTable;
            if (ds != null)
            {
                foreach (DataRow row in ds.Rows)
                {
                    try
                    {
                        Guid id = Guid.Empty;
                        if (row.Table.Columns.Contains("Id") && row["Id"] != DBNull.Value)
                            id = (Guid)row["Id"];
                        if (id != Guid.Empty)
                        {
                            var evItem = _eventManager.GetAllEvents().FirstOrDefault(x => x.Id == id);
                            if (evItem != null) shown.Add(evItem);
                        }
                    }
                    catch { }
                }
            }

            var recs = _eventManager.GetRecommendations(shown, 10);
            dataGridViewEvents.DataSource = _rec.ToDataTable(recs);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            // clear search filters and reload recent/initial data
            txtSearch.Text = string.Empty;
            cmbCategory.SelectedIndex = 0;
            chkFrom.Checked = false;
            chkTo.Checked = false;
            dtFrom.Value = DateTime.Today;
            dtTo.Value = DateTime.Today.AddDays(60);
            // reload initial event list
            LoadAllEvents();
            // clear recommendations and announcements
            listBoxRecommendations.DataSource = null;
            listBoxAnnouncements.DataSource = null;
        }

        private void BtnRefreshAnnouncements_Click(object sender, EventArgs e)
        {
            RefreshAnnouncements();
        }

        private void BtnBackToMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Designer-generated handler name expected by InitializeComponent
        private void btnBackToMain_Click(object sender, EventArgs e)
        {
            BtnBackToMain_Click(sender, e);
        }

        private void RefreshAnnouncements()
        {
            var top = _announcements.PeekTopN(10).ToList();
            listBoxAnnouncements.DataSource = top;
            listBoxAnnouncements.DisplayMember = "Title";
        }
    }
}
