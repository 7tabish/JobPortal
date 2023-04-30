using JobPortal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.DBContexts
{
    public class JobDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Job> Jobs { get; set; } = null!;
        public DbSet<Requirement> JobRequirements { get; set; } = null!;

        public JobDbContext(DbContextOptions<JobDbContext> options)
            : base(options)
        {

        }




    }

}