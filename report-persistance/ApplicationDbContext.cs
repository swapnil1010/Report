using Microsoft.EntityFrameworkCore;
using report_core.Domain.DTOs.Login;
using report_core.Domain.DTOs.Project;
using report_core.Domain.Entities;

namespace report_persistance
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<LoginDTO> TBL_USER  { get; set; }
        public DbSet<ProjectDTO> TBL_PROJECT { get; set; }
        public DbSet<ProjectMasterDTO> TBL_PROJECT_MASTER { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}