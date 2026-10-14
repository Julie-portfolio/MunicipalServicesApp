using System;
using System.Collections.Generic;
using System.Linq;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Services
{
    public class AnnouncementManager
    {
        private readonly PriorityQueue<EventItem> _priorityQueue = new PriorityQueue<EventItem>();
        private readonly EventManager _eventManager;

        public AnnouncementManager(EventManager eventManager)
        {
            _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
        }

        // Seed announcements from events with Priority > 0
        public void SeedAnnouncementsFromEvents()
        {
            foreach (var ev in _eventManager.GetAllEvents().Where(e => e.EventDate.Date >= DateTime.Today && e.EventDate.Date <= DateTime.Today.AddYears(1)))
            {
                if (ev.Priority > 0)
                {
                    _priorityQueue.Enqueue(ev, ev.Priority);
                }
            }
        }

        public void EnqueueAnnouncement(EventItem ev, int priority)
        {
            if (ev == null) return;
            _priorityQueue.Enqueue(ev, priority);
        }

        public EventItem DequeueTopAnnouncement()
        {
            if (_priorityQueue.Count == 0) return null;
            return _priorityQueue.Dequeue();
        }

        public IEnumerable<EventItem> PeekTopN(int n)
        {
            var list = new List<EventItem>();
            var temp = new List<(int, EventItem)>();
            while (_priorityQueue.Count > 0 && list.Count < n)
            {
                var item = _priorityQueue.Dequeue();
                list.Add(item);
                temp.Add((item.Priority, item));
            }
            // push back
            foreach (var t in temp) _priorityQueue.Enqueue(t.Item2, t.Item1);
            return list;
        }
    }
}
