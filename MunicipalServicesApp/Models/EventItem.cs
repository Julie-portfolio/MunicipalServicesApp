using System;

namespace MunicipalServicesApp.Models
{
    public class EventItem
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public int Priority { get; set; } = 0; // higher value => higher priority
        public string Location { get; set; }

        public EventItem() { }

        public EventItem(string title, string category, string description, DateTime date, string location = null, int priority = 0)
        {
            Title = title;
            Category = category;
            Description = description;
            EventDate = date;
            Location = location;
            Priority = priority;
        }
    }
}
