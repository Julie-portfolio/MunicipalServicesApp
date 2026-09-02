using System;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// Represents a single issue reported by a resident.
    /// </summary>
    public class Issue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public string Status { get; set; }
        public DateTime DateReported { get; set; }

        public Issue(string location, string category, string description, string attachmentPath)
        {
            Location = location;
            Category = category;
            Description = description;
            AttachmentPath = attachmentPath;
            Status = "Open";
            DateReported = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{DateReported:g}] {Category} - {Location}";
        }
    }
}
