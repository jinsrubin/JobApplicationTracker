using JobApplicationTracker.Server.Models.DTOs;

namespace JobApplicationTracker.Server.Services
{
    public interface IJobApplicationService
    {
        Task<IEnumerable<JobApplicationResponse>> GetJobApplicationsAsync();
        Task<JobApplicationResponse> GetJobApplicationByIdAsync(int id);
        Task<JobApplicationResponse> AddJobApplicationAsync(JobApplicationRequest jobApplicationrequest);
        Task<JobApplicationResponse> UpdateJobApplicationAsync(int id, JobApplicationRequest jobApplicationrequest);
        Task<bool> JobApplicationExistsAsync(JobApplicationRequest jobApplicationRequest, int? excluded = null);
    }
}
