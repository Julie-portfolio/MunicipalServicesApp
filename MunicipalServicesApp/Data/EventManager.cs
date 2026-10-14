using System;
using System.Collections.Generic;
using System.Linq;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Data
{
    public class EventManager
    {
        private readonly SortedDictionary<DateTime, List<EventItem>> eventsByDate;
        private readonly Dictionary<string, List<EventItem>> eventsByCategory;
        private readonly HashSet<string> categories;
        private readonly Queue<EventItem> announcements;
        private readonly Stack<string> recentSearches;
        private readonly Dictionary<string, int> searchCounts;
        private readonly PriorityQueue<EventItem> urgentQueue;

        public EventManager()
        {
            eventsByDate = new SortedDictionary<DateTime, List<EventItem>>();
            eventsByCategory = new Dictionary<string, List<EventItem>>(StringComparer.OrdinalIgnoreCase);
            categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            announcements = new Queue<EventItem>();
            recentSearches = new Stack<string>();
            searchCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            urgentQueue = new PriorityQueue<EventItem>();
            SeedSample();
        }

        public void AddEvent(EventItem e)
        {
            if (e == null) return;
            var dateKey = e.EventDate.Date;
            if (!eventsByDate.TryGetValue(dateKey, out var list))
            {
                list = new List<EventItem>();
                eventsByDate[dateKey] = list;
            }
            list.Add(e);

            var cat = string.IsNullOrWhiteSpace(e.Category) ? "Uncategorized" : e.Category;
            if (!eventsByCategory.TryGetValue(cat, out var clist))
            {
                clist = new List<EventItem>();
                eventsByCategory[cat] = clist;
            }
            clist.Add(e);

            categories.Add(cat);

            if (e.Priority != 0)
                urgentQueue.Enqueue(e, e.Priority);

            // announcements queue - avoid duplicates by Id
            if (!announcements.Any(a => a.Id == e.Id))
                announcements.Enqueue(e);
        }

        public List<string> GetCategories()
        {
            return categories.OrderBy(x => x).ToList();
        }

        public List<EventItem> GetAllEvents()
        {
            var result = new List<EventItem>();
            foreach (var kv in eventsByDate)
            {
                result.AddRange(kv.Value.OrderBy(x => x.EventDate));
            }
            return result;
        }

        public List<EventItem> Search(string category, string text, DateTime? from, DateTime? to)
        {
            IEnumerable<EventItem> source;
            if (!string.IsNullOrWhiteSpace(category) && eventsByCategory.ContainsKey(category))
                source = eventsByCategory[category];
            else
                source = GetAllEvents();

            if (from.HasValue || to.HasValue)
            {
                var f = from ?? DateTime.MinValue;
                var t = to ?? DateTime.MaxValue;
                source = source.Where(ev => ev.EventDate.Date >= f.Date && ev.EventDate.Date <= t.Date);
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                var q = text.ToLowerInvariant();
                source = source.Where(ev => (!string.IsNullOrWhiteSpace(ev.Title) && ev.Title.ToLowerInvariant().Contains(q)) || (!string.IsNullOrWhiteSpace(ev.Description) && ev.Description.ToLowerInvariant().Contains(q)));
            }

            var results = source.OrderBy(ev => ev.EventDate).ToList();

            // Record search
            var desc = string.IsNullOrWhiteSpace(category) ? (string.IsNullOrWhiteSpace(text) ? "" : text) : category + (string.IsNullOrWhiteSpace(text) ? "" : ": " + text);
            recentSearches.Push(desc);
            if (!string.IsNullOrWhiteSpace(category))
            {
                if (!searchCounts.ContainsKey(category)) searchCounts[category] = 0;
                searchCounts[category]++;
            }
            else if (!string.IsNullOrWhiteSpace(text) && results.Any())
            {
                var topCat = results.First().Category;
                if (!string.IsNullOrWhiteSpace(topCat))
                {
                    if (!searchCounts.ContainsKey(topCat)) searchCounts[topCat] = 0;
                    searchCounts[topCat]++;
                }
            }

            return results;
        }

        public List<EventItem> GetRecommendations(List<EventItem> alreadyShown, int max = 5)
        {
            if (searchCounts.Count == 0) return new List<EventItem>();
            var topCategory = searchCounts.OrderByDescending(kv => kv.Value).First().Key;
            if (!eventsByCategory.ContainsKey(topCategory)) return new List<EventItem>();
            var upcoming = eventsByCategory[topCategory].Where(e => e.EventDate.Date >= DateTime.Today).OrderBy(e => e.EventDate);
            var list = upcoming.Where(e => alreadyShown == null || !alreadyShown.Any(s => s.Id == e.Id)).Take(max).ToList();
            return list;
        }

        public string GetRecommendationReason()
        {
            if (searchCounts.Count == 0) return string.Empty;
            var topCategory = searchCounts.OrderByDescending(kv => kv.Value).First().Key;
            return string.Format("Because you searched {0}", topCategory);
        }

        public List<EventItem> GetMostUrgent(int count)
        {
            var result = new List<EventItem>();
            // copy by dequeuing into temp list
            var temp = new List<EventItem>();
            while (urgentQueue.Count > 0 && result.Count < count)
            {
                var it = urgentQueue.Dequeue();
                if (it != null)
                {
                    result.Add(it);
                    temp.Add(it);
                }
            }
            // re-enqueue
            foreach (var t in temp) urgentQueue.Enqueue(t, t.Priority);
            return result;
        }

        public List<EventItem> GetAnnouncements()
        {
            var seen = new HashSet<Guid>();
            var list = new List<EventItem>();
            foreach (var a in announcements)
            {
                if (!seen.Contains(a.Id))
                {
                    seen.Add(a.Id);
                    list.Add(a);
                }
            }
            return list;
        }

        public List<string> GetRecentSearches()
        {
            return recentSearches.ToList();
        }

        // seed at least 8 sample events across 4 categories
        public void SeedSample()
        {
            if (eventsByDate.Count > 0) return;
            AddEvent(new EventItem("Community Clean-Up", "Parks", "Join neighbours to clean the park.", DateTime.Today.AddDays(3), "Central Park", 2));
            AddEvent(new EventItem("Food Drive", "Community", "Donate non-perishable food.", DateTime.Today.AddDays(7), "Town Hall", 3));
            AddEvent(new EventItem("Roadworks Notice", "Roads", "Temporary closure of 5th Ave.", DateTime.Today.AddDays(1), "5th Ave", 1));
            AddEvent(new EventItem("Park Concert", "Parks", "Outdoor music evening.", DateTime.Today.AddDays(14), "Riverside Park", 2));
            AddEvent(new EventItem("Water Shutdown", "Utilities", "Scheduled maintenance.", DateTime.Today.AddDays(5), "Various", 0));
            AddEvent(new EventItem("Community Meeting", "Community", "Discuss neighborhood initiatives.", DateTime.Today.AddDays(10), "Library Hall", 1));
            AddEvent(new EventItem("Bridge Repair", "Roads", "Lane closures expected.", DateTime.Today.AddDays(20), "Bridge St.", 0));
            AddEvent(new EventItem("Playground Opening", "Parks", "New equipment unveiling.", DateTime.Today.AddDays(30), "East Park", 3));
        }
    }
}
