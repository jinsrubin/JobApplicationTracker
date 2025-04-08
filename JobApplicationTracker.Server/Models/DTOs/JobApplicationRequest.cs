using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Server.Models.DTOs
{
    public class JobApplicationRequest
    {
        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters.")]
        public required string CompanyName { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [StringLength(100, ErrorMessage = "Position cannot exceed 100 characters.")]
        public required string Position { get; set; }

        [EnumDataType(typeof(JobStatus), ErrorMessage = "Invalid Job Status.")]
        public JobStatus Status { get; set; } = JobStatus.Applied;

        public DateTime DateApplied { get; set; } = DateTime.UtcNow;
    }
}
