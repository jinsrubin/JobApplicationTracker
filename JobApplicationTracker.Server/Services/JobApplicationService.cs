using AutoMapper;
using JobApplicationTracker.Server.Entities;
using JobApplicationTracker.Server.Models.DTOs;
using JobApplicationTracker.Server.Repositories;

namespace JobApplicationTracker.Server.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IRepository<JobApplication> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<JobApplicationService> _logger;
        public JobApplicationService(IRepository<JobApplication> repository,
            IMapper mapper,
            ILogger<JobApplicationService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<JobApplicationResponse>> GetJobApplicationsAsync()
        {
            var jobApplications = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobApplicationResponse>>(jobApplications);
        }
        public async Task<JobApplicationResponse> GetJobApplicationByIdAsync(int id)
        {
            var jobApplication = await _repository.GetByIdAsync(id);
            return  _mapper.Map<JobApplicationResponse>(jobApplication);
        }
        public async Task<JobApplicationResponse> AddJobApplicationAsync(JobApplicationRequest jobApplicationRequest)
        {
            var jobApplication = _mapper.Map<JobApplication>(jobApplicationRequest);
            var createdJobApplication = await _repository.AddAsync(jobApplication);
            _logger.LogInformation($"Job application with ID - {createdJobApplication.Id}, Company - {createdJobApplication.CompanyName} and Position - {createdJobApplication.Position} created successfully.");
            return _mapper.Map<JobApplicationResponse>(createdJobApplication);
        }
        public async Task<JobApplicationResponse> UpdateJobApplicationAsync(int id,JobApplicationRequest jobApplicationRequest)
        {
            var existingJobApplication = await _repository.GetByIdAsync(id);
            if (existingJobApplication == null)
            {
                throw new KeyNotFoundException($"Job application with ID {id} not found.");
            }
            existingJobApplication.CompanyName = jobApplicationRequest.CompanyName;
            existingJobApplication.Position = jobApplicationRequest.Position;
            existingJobApplication.Status = jobApplicationRequest.Status;
            existingJobApplication.DateApplied = jobApplicationRequest.DateApplied;

            var updatedJobApplication = await _repository.UpdateAsync(existingJobApplication);
            _logger.LogInformation($"Job application with ID - {updatedJobApplication.Id}, Company - {updatedJobApplication.CompanyName} and Position - {updatedJobApplication.Position} updated successfully.");

            return _mapper.Map<JobApplicationResponse>(updatedJobApplication);
        }
        public async Task<bool> JobApplicationExistsAsync(JobApplicationRequest jobApplicationRequest, int? excluded = null)
        {
            return await _repository.AnyAsync(j =>
            (excluded == null || j.Id != excluded.Value) &&
            j.CompanyName == jobApplicationRequest.CompanyName &&
            j.Position == jobApplicationRequest.Position &&
            j.Status == jobApplicationRequest.Status &&
            j.DateApplied.Date == jobApplicationRequest.DateApplied.Date
            );
        }
    }
}
