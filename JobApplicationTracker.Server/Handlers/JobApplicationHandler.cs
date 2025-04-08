using JobApplicationTracker.Server.Models.DTOs;
using JobApplicationTracker.Server.Services;

namespace JobApplicationTracker.Server.Handlers
{
    public class JobApplicationHandler
    {
        private readonly IJobApplicationService _jobApplicationService;
        public JobApplicationHandler(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }
        public async Task<IEnumerable<JobApplicationResponse>> GetAllJobApplicationsAsync()
        {
            return await _jobApplicationService.GetJobApplicationsAsync();
        }
        public async Task<JobApplicationResponse> GetJobApplicationByIdAsync(int id)
        {
            return await _jobApplicationService.GetJobApplicationByIdAsync(id);
        }
        public async Task<JobApplicationResponse> AddJobApplicationAsync(JobApplicationRequest jobApplicationRequest)
        {
            return await _jobApplicationService.AddJobApplicationAsync(jobApplicationRequest);
        }
        public async Task<JobApplicationResponse> UpdateJobApplicationAsync(JobApplicationRequest jobApplicationRequest)
        {
            return await _jobApplicationService.UpdateJobApplicationAsync(jobApplicationRequest);
        }
        public async Task DeleteJobApplicationAsync(int id)
        {
            await _jobApplicationService.DeleteJobApplicationAsync(id);
        }
    }
}
