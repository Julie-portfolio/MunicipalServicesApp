using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Services
{
    public class RecommendationService
    {
        // maintain recent searches as a stack (most recent on top)
        private Stack<(string category, string query)> _searchHistory = new Stack<(string, string)>();
        private readonly EventManager _eventManager;

        public RecommendationService(EventManager eventManager)
        {
            _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
        }

        public void RecordSearch(string category, string query)
        {
            _searchHistory.Push((category ?? string.Empty, query ?? string.Empty));
            // cap history size to last 200
            while (_searchHistory.Count > 200)
            {
                // remove oldest items: reverse to get oldest at start, rebuild stack of most recent 200
                _searchHistory = new Stack<(string, string)>(_searchHistory.Take(200).Reverse());
                break;
            }
        }

        public IEnumerable<EventItem> Recommend(int max = 5)
        {
            var topCats = _searchHistory
                .Where(h => !string.IsNullOrWhiteSpace(h.category))
                .GroupBy(h => h.category, StringComparer.OrdinalIgnoreCase)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(3)
                .ToList();

            var bucket = new List<EventItem>();

            foreach (var c in topCats)
                bucket.AddRange(_eventManager.GetAllEvents()
                    .Where(ev => string.Equals(ev.Category, c, StringComparison.OrdinalIgnoreCase)));

            var recentQueries = _searchHistory.Select(h => h.query).Where(q => !string.IsNullOrWhiteSpace(q)).Take(5);
            foreach (var q in recentQueries)
            {
                var qL = q.ToLowerInvariant();
                bucket.AddRange(_eventManager.GetAllEvents()
                    .Where(ev => (!string.IsNullOrWhiteSpace(ev.Title) && ev.Title.ToLowerInvariant().Contains(qL)) || (!string.IsNullOrWhiteSpace(ev.Description) && ev.Description.ToLowerInvariant().Contains(qL))));
            }

            var recommended = bucket
                .GroupBy(e => e.Id)
                .Select(g => g.First())
                .OrderBy(e => e.EventDate)
                .ThenByDescending(e => e.Priority)
                .Take(max)
                .ToList();

            return recommended;
        }

        public DataTable ToDataTable(IEnumerable<EventItem> list)
        {
            var dt = new DataTable();
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("Priority", typeof(int));
            foreach (var e in list)
                dt.Rows.Add(e.Title, e.Category, e.EventDate, e.Location, e.Priority);
            return dt;
        }
    }
}
