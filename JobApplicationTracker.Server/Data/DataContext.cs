using JobApplicationTracker.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.Server.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<JobApplication> JobApplications { get; set; }
    }
}
