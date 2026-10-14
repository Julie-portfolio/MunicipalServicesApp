using System.Collections.Generic;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// In-memory store for reported issues, shared across the application
    /// for the lifetime of the running program. Uses a List&lt;Issue&gt;
    /// as required by the technical specification.
    /// </summary>
    public static class IssueRepository
    {
        private static readonly List<Issue> _issues = new List<Issue>();

        // Event raised when a new issue is added. Subscribers (UI) can refresh accordingly.
        public static event System.Action<Issue> IssueAdded;

        public static IReadOnlyList<Issue> GetAll() => _issues.AsReadOnly();

        public static void Add(Issue issue)
        {
            _issues.Add(issue);
            IssueAdded?.Invoke(issue);
        }
    }
}
