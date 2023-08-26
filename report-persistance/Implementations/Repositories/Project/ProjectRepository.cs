using report_core.Domain.DTOs.Project;
using report_core.Domain.Entities;
using report_core.Domain.Interfaces.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_persistance.Implementations.Repositories.Project
{
    public class ProjectRepository : GenericRepository<ProjectDTO>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
