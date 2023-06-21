using First_Step_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace First_Step_API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<JobPosting> JobPostings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}