using JobApplicationTracker.Server.Handlers;
using JobApplicationTracker.Server.Models.DTOs;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly JobApplicationHandler _jobApplicationHandler;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(JobApplicationsController));
        public JobApplicationsController(JobApplicationHandler jobApplicationHandler)
        {
            _jobApplicationHandler = jobApplicationHandler;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplicationResponse>>> GetAllJobApplications()
        {
            var jobApplications = await _jobApplicationHandler.GetAllJobApplicationsAsync();
            return Ok(jobApplications);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplicationResponse>> GetById(int id)
        {
            var jobApplication = await _jobApplicationHandler.GetJobApplicationByIdAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            return Ok(jobApplication);
        }
        [HttpPost]
        public async Task<ActionResult<JobApplicationResponse>> Add([FromBody] JobApplicationRequest jobApplicationRequest)
        {
            if (jobApplicationRequest == null)
            {
                return BadRequest("Job application request cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var jobApplication = await _jobApplicationHandler.AddJobApplicationAsync(jobApplicationRequest);
            if (jobApplication == null)
            {
                return Conflict(new { message = "A job application with the same details already exists." });
            }
            return CreatedAtAction(nameof(GetById), new { id = jobApplication.Id }, jobApplication);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JobApplicationRequest jobApplicationRequest)
        {
            if (jobApplicationRequest == null)
            {
                return BadRequest("Job application request cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedApplication = await _jobApplicationHandler.UpdateJobApplicationAsync(id, jobApplicationRequest);

            if (updatedApplication == null)
            {
                return Conflict(new { message = "Another job application with the same details already exists." });
            }

            return Ok(updatedApplication);
        }
    }
}
