using JobApplicationTracker.Server.Models;

namespace JobApplicationTracker.Server.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }
        public required string CompanyName { get; set; }
        public required string Position { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Applied;
        public DateTime DateApplied { get; set; }
    }
}
